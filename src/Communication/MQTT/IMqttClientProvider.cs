using Atomy.SDK.Common.Ports;

namespace Atomy.SDK.Communication.MQTT;

public interface IMqttClientProvider : IDisposable
{
    string ServiceUniqueName { get; }

    Task ConnectAsync();
    
    Task PublishAsync(string topic, string message, MqttPublishOptions options);
    
    Task PublishAsync(string topic, byte[] message, MqttPublishOptions options);
    
    Task PublishAsync(string topic, IPort port, MqttPublishOptions options);

    Task<MqttSubscription> SubscribeAsync(string topic);

    Task UnsubscribeAsync(MqttSubscription subscription);
}