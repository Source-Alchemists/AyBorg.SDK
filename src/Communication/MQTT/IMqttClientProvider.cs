using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Communication.MQTT;

public interface IMqttClientProvider : IDisposable
{
    ValueTask ConnectAsync();

    ValueTask PublishAsync(string topic, string message, MqttPublishOptions options);

    ValueTask PublishAsync(string topic, byte[] message, MqttPublishOptions options);

    ValueTask PublishAsync(string topic, IPort port, MqttPublishOptions options);

    ValueTask<MqttSubscription> SubscribeAsync(string topic);

    ValueTask UnsubscribeAsync(MqttSubscription subscription);
}
