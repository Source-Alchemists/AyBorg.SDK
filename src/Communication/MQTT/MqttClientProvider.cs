using System.Globalization;
using System.Text.Json;
using AyBorg.SDK.Common;
using AyBorg.SDK.Common.Models;
using AyBorg.SDK.Common.Ports;
using ImageTorque;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Server;

namespace AyBorg.SDK.Communication.MQTT;

public sealed class MqttClientProvider : IMqttClientProvider
{
    private static readonly RecyclableMemoryStreamManager s_memoryManager = new();
    private readonly ILogger _logger;
    private readonly MqttClientOptions _mqttClientOptions;
    private readonly ManagedMqttClientOptions _managedMqttClientOptions;
    private readonly IManagedMqttClient _mqttClient;
    private readonly List<MqttSubscription> _subscriptions = new();
    private bool _disposedValue;

    public MqttClientProvider(ILogger logger, string clientId, string host, int port)
    {
        _logger = logger;

        var factory = new MqttFactory();
        _mqttClientOptions = new MqttClientOptionsBuilder()
            .WithClientId(clientId)
            .WithTcpServer(host, port)
            .WithCleanSession()
            .Build();
        _managedMqttClientOptions = new ManagedMqttClientOptionsBuilder()
            .WithClientOptions(_mqttClientOptions)
            .Build();

        _mqttClient = factory.CreateManagedMqttClient();
        _mqttClient.ApplicationMessageReceivedAsync += OnApplicationMessageReceivedAsync;
    }

    public async ValueTask ConnectAsync()
    {
        await _mqttClient.StartAsync(_managedMqttClientOptions);
        int count = 0;
        while (!_mqttClient.IsConnected && count < 10)
        {
            await Task.Delay(1000);
            _logger.LogTrace(new EventId((int)EventLogType.Disconnect), "MQTT client is not connected, retrying...");
            count++;
        }

        if (!_mqttClient.IsConnected)
        {
            throw new InvalidOperationException("MQTT client is not connected");
        }
    }

    public async ValueTask PublishAsync(string topic, string message, MqttPublishOptions options)
    {
        try
        {
            await _mqttClient.InternalClient.PublishAsync(new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .WithQualityOfServiceLevel(options.QualityOfServiceLevel)
                .WithRetainFlag(options.Retain)
                .Build());
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogTrace(ex, "PublishAsync cancelled");
        }
    }

    public async ValueTask PublishAsync(string topic, byte[] message, MqttPublishOptions options)
    {
        try
        {
            await _mqttClient.InternalClient.PublishAsync(new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .WithQualityOfServiceLevel(options.QualityOfServiceLevel)
                .WithRetainFlag(options.Retain)
                .Build()).ConfigureAwait(false);
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogTrace(ex, "PublishAsync cancelled");
        }
    }

    public async ValueTask PublishAsync(string topic, IPort port, MqttPublishOptions options)
    {
        switch (port)
        {
            case StringPort stringPort: // Contains also FolderPort
                await PublishAsync($"{topic}", stringPort.Value, options).ConfigureAwait(false);
                break;
            case NumericPort numericPort:
                await PublishAsync($"{topic}", numericPort.Value.ToString(CultureInfo.InvariantCulture), options).ConfigureAwait(false);
                break;
            case BooleanPort booleanPort:
                await PublishAsync($"{topic}", booleanPort.Value.ToString(CultureInfo.InvariantCulture), options).ConfigureAwait(false);
                break;
            case EnumPort enumPort:
                await PublishAsync($"{topic}", enumPort.Value.ToString(), options).ConfigureAwait(false);
                break;
            case RectanglePort rectanglePort:
                await PublishAsync($"{topic}", JsonSerializer.Serialize(rectanglePort.Value), options).ConfigureAwait(false);
                break;
            case ImagePort imagePort:
                await SendImageAsync($"{topic}", imagePort.Value, options).ConfigureAwait(false);
                break;
            default:
                throw new NotSupportedException($"Port type {port.GetType().Name} is not supported.");

        }
    }

    public async ValueTask<MqttSubscription> SubscribeAsync(string topic)
    {
        if (!_mqttClient.IsConnected)
        {
            _logger.LogWarning("Cannot subscribe to {topic} because MQTT client is not connected.", topic);
            await ConnectAsync().ConfigureAwait(false);
        }
        await _mqttClient.SubscribeAsync(topic).ConfigureAwait(false);
        var subscription = new MqttSubscription { TopicFilter = topic };
        lock (_subscriptions)
        {
            _subscriptions.Add(subscription);
        }
        return subscription;
    }

    public async ValueTask UnsubscribeAsync(MqttSubscription subscription)
    {
        if (!_mqttClient.IsConnected)
        {
            _logger.LogWarning("UnsubscribeAsync called while not connected");
            return;
        };
        await _mqttClient.UnsubscribeAsync(subscription.TopicFilter).ConfigureAwait(false);
        lock (_subscriptions)
        {
            _subscriptions.Remove(subscription);
        }
    }

    private async ValueTask SendImageAsync(string topic, Image image, MqttPublishOptions options)
    {
        if (image == null) return;

        const int maxSize = 250;
        Image resizedImage = null!;
        Image targetImage;
        if (image.Width <= maxSize && image.Height <= maxSize || options.Resize == false)
        {
            targetImage = image;
        }
        else
        {
            image.CalculateClampSize(maxSize, out int w, out int h);
            resizedImage = image.Resize(w, h, ResizeMode.NearestNeighbor);
            targetImage = resizedImage;
        }

        try
        {
            await PublishAsync($"{topic}/meta", JsonSerializer.Serialize(new ImageMeta
            {
                Width = image.Width,
                Height = image.Height,
                PixelFormat = image.PixelFormat
            }), options);

            using MemoryStream stream = s_memoryManager.GetStream();
            targetImage.Save(stream, options.EncoderType);
            stream.Position = 0;
            await _mqttClient.InternalClient.PublishAsync(new MqttApplicationMessageBuilder()
                .WithTopic($"{topic}/data")
                .WithPayload(stream, stream.Length)
                .WithQualityOfServiceLevel(options.QualityOfServiceLevel)
                .WithRetainFlag(options.Retain)
                .Build()).ConfigureAwait(false);
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogTrace(ex, "SendImageAsync cancelled");
        }
        finally
        {
            resizedImage?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _mqttClient?.Dispose();
            }

            _disposedValue = true;
        }
    }

    private async Task OnApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        var subscriptions = _subscriptions.Where(x => MqttTopicFilterComparer.Compare(e.ApplicationMessage.Topic, x.TopicFilter) == MqttTopicFilterCompareResult.IsMatch).ToList();
        await Parallel.ForEachAsync(subscriptions, async (subscription, token) =>
        {
            subscription.MessageReceived?.Invoke(e.ApplicationMessage);
            await ValueTask.CompletedTask;
        });
    }
}
