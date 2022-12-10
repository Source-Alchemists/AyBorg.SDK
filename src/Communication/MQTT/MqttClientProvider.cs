using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;
using AyBorg.SDK.Common.Ports;
using AyBorg.SDK.Data.DTOs;
using AyBorg.SDK.ImageProcessing;
using AyBorg.SDK.System.Configuration;
using Microsoft.Extensions.Configuration;
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
    private readonly ILogger<MqttClientProvider> _logger;
    private readonly IConfiguration _configuration;
    private readonly string _serviceTypeName;
    private readonly string _serviceVersion;
    private readonly MqttClientOptions _mqttClientOptions;
    private readonly ManagedMqttClientOptions _managedMqttClientOptions;
    private readonly IManagedMqttClient _mqttClient;
    private bool _disposedValue;
    private Task? _statusTask;
    private ConcurrentBag<MqttSubscription> _subscriptions = new();

    public string ServiceUniqueName { get; }

    public MqttClientProvider(ILogger<MqttClientProvider> logger, IConfiguration configuration, IGatewayConfiguration serviceConfiguration)
    {
        _logger = logger;
        _configuration = configuration;

        ServiceUniqueName = serviceConfiguration.UniqueName;
        _serviceTypeName = serviceConfiguration.TypeName;
        _serviceVersion = serviceConfiguration.Version;

        var factory = new MqttFactory();
        _mqttClientOptions = new MqttClientOptionsBuilder()
            .WithClientId(ServiceUniqueName)
            .WithTcpServer(_configuration.GetValue<string>("MQTT:Host"), _configuration.GetValue<int>("MQTT:Port"))
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
        while (!_mqttClient.IsConnected)
        {
            await Task.Delay(100);
        }
        string baseTopic = $"AyBorg/sys/services/{ServiceUniqueName}";
        var options = new MqttPublishOptions { Retain = true };
        await PublishAsync($"{baseTopic}/version", _serviceVersion, options);
        await PublishAsync($"{baseTopic}/type", _serviceTypeName, options);

        _statusTask = Task.Run(async () =>
        {
            DateTime startUtc = DateTime.UtcNow;
            while (!_disposedValue)
            {
                if (_mqttClient.IsConnected)
                {
                    TimeSpan uptime = DateTime.UtcNow - startUtc;
                    await PublishAsync($"{baseTopic}/uptime", $"{((long)uptime.TotalSeconds)} seconds", new MqttPublishOptions());
                }
                await Task.Delay(10000);
            }
        });
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
                await PublishAsync($"{topic}/value", stringPort.Value, options).ConfigureAwait(false);
                break;
            case NumericPort numericPort:
                await PublishAsync($"{topic}/value", numericPort.Value.ToString(CultureInfo.InvariantCulture), options).ConfigureAwait(false);
                break;
            case BooleanPort booleanPort:
                await PublishAsync($"{topic}/value", booleanPort.Value.ToString(CultureInfo.InvariantCulture), options).ConfigureAwait(false);
                break;
            case EnumPort enumPort:
                await PublishAsync($"{topic}/value", enumPort.Value.ToString(), options).ConfigureAwait(false);
                break;
            case RectanglePort rectanglePort:
                await PublishAsync($"{topic}/value", JsonSerializer.Serialize(rectanglePort.Value), options).ConfigureAwait(false);
                break;
            case ImagePort imagePort:
                await SendImageAsync($"{topic}/value", imagePort.Value, options).ConfigureAwait(false);
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
        _subscriptions.Add(subscription);
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
            var tmp = new List<MqttSubscription>(_subscriptions);
            tmp.Remove(subscription);
            _subscriptions = new ConcurrentBag<MqttSubscription>(tmp);
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
            Image.CalculateClampSize(image, maxSize, out int w, out int h);
            resizedImage = image.Resize(w, h, ResizeMode.NearestNeighbor);
            targetImage = resizedImage;
        }

        try
        {
            await PublishAsync($"{topic}/meta", JsonSerializer.Serialize(new ImageMetaDto
            {
                Width = image.Width,
                Height = image.Height,
                PixelFormat = image.PixelFormat,
                EncoderType = options.EncoderType
            }), options);

            using MemoryStream stream = s_memoryManager.GetStream();
            Image.Save(targetImage, stream, options.EncoderType);
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

    public async void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _mqttClient?.Dispose();
                if (_statusTask != null)
                {
                    await _statusTask;
                    _statusTask.Dispose();
                }
            }

            _disposedValue = true;
        }
    }

    private async Task OnApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        var subscriptions = _subscriptions.Where(x => MqttTopicFilterComparer.Compare(e.ApplicationMessage.Topic, x.TopicFilter) == MqttTopicFilterCompareResult.IsMatch).ToList();
        CancellationToken token = CancellationToken.None;
        await Parallel.ForEachAsync(subscriptions, async (subscription, token) =>
        {
            subscription.MessageReceived?.Invoke(e.ApplicationMessage);
            await ValueTask.CompletedTask;
        });
    }
}
