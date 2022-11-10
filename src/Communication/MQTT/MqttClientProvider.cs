using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using MQTTnet.Extensions.ManagedClient;
using Autodroid.SDK.Data.DTOs;
using Autodroid.SDK.ImageProcessing;
using Autodroid.SDK.Common.Ports;
using Autodroid.SDK.System.Configuration;

namespace Autodroid.SDK.Communication.MQTT;

public sealed class MqttClientProvider : IMqttClientProvider
{
    private static readonly RecyclableMemoryStreamManager _memoryManager = new();
    private readonly ILogger<MqttClientProvider> _logger;
    private readonly IConfiguration _configuration;
    private readonly string _serviceTypeName;
    private readonly string _serviceUniqueName;
    private readonly string _serviceVersion;
    private readonly MqttClientOptions _mqttClientOptions;
    private readonly ManagedMqttClientOptions _managedMqttClientOptions;
    private readonly IManagedMqttClient _mqttClient;
    private bool _disposedValue;
    private Task? _statusTask;
    private ConcurrentBag<MqttSubscription> _subscriptions = new();

    public string ServiceUniqueName => _serviceUniqueName;

    public MqttClientProvider(ILogger<MqttClientProvider> logger, IConfiguration configuration, IServiceConfiguration serviceConfiguration)
    {
        _logger = logger;
        _configuration = configuration;

        _serviceUniqueName = serviceConfiguration.UniqueName;
        _serviceTypeName = serviceConfiguration.TypeName;
        _serviceVersion = serviceConfiguration.Version;

        var factory = new MqttFactory();
        _mqttClientOptions = new MqttClientOptionsBuilder()
            .WithClientId(_serviceUniqueName)
            .WithTcpServer(_configuration.GetValue<string>("MQTT:Host"), _configuration.GetValue<int>("MQTT:Port"))
            .WithCleanSession()
            .Build();
        _managedMqttClientOptions = new ManagedMqttClientOptionsBuilder()
            .WithClientOptions(_mqttClientOptions)
            .Build();
        
        _mqttClient = factory.CreateManagedMqttClient();
        _mqttClient.ApplicationMessageReceivedAsync += OnApplicationMessageReceivedAsync;
    }

    public async Task ConnectAsync()
    {
        await _mqttClient.StartAsync(_managedMqttClientOptions);
        while(!_mqttClient.IsConnected)
        {
            await Task.Delay(100);
        }
        var baseTopic = $"Autodroid/sys/services/{_serviceUniqueName}";
        var options = new MqttPublishOptions { Retain = true };
        await PublishAsync($"{baseTopic}/version", _serviceVersion, options);
        await PublishAsync($"{baseTopic}/type", _serviceTypeName, options);

        _statusTask = Task.Run(async () =>
        {
            var startUtc = DateTime.UtcNow;
            while (!_disposedValue)
            {
                if (_mqttClient.IsConnected)
                {
                    var uptime = DateTime.UtcNow - startUtc;
                    await PublishAsync($"{baseTopic}/uptime", $"{((long)uptime.TotalSeconds)} seconds", new MqttPublishOptions());
                }
                await Task.Delay(10000);
            }
        });
    }

    public Task PublishAsync(string topic, string message, MqttPublishOptions options)
    {
        try
        {
            return _mqttClient.InternalClient.PublishAsync(new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .WithQualityOfServiceLevel(options.QualityOfServiceLevel)
                .WithRetainFlag(options.Retain)
                .Build());
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogTrace(ex, "PublishAsync cancelled");
            return Task.CompletedTask;
        }
    }

    public async Task PublishAsync(string topic, byte[] message, MqttPublishOptions options)
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

    public async Task PublishAsync(string baseTopic, IPort port, MqttPublishOptions options)
    {
        var topic = baseTopic;
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

    public async Task<MqttSubscription> SubscribeAsync(string topic)
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

    public async Task UnsubscribeAsync(MqttSubscription subscription)
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

    private async Task SendImageAsync(string topic, Image image, MqttPublishOptions options)
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
            Image.CalculateClampSize(image, maxSize, out var w, out var h);
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

            using var stream = _memoryManager.GetStream();
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
        var token = CancellationToken.None;
        await Parallel.ForEachAsync(subscriptions, async (subscription, token) =>
        {
            subscription.MessageReceived?.Invoke(e.ApplicationMessage);
            await Task.CompletedTask;
        });
    }
}