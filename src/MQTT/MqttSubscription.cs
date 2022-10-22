using MQTTnet;

namespace Atomy.SDK.MQTT;

public class MqttSubscription
{
    /// <summary>
    /// Callback to be called when a message is received.
    /// </summary>
    public Action<MqttApplicationMessage>? MessageReceived { get; set; }

    /// <summary>
    /// The topic filter.
    /// </summary>
    public string TopicFilter { get; init; } = null!;
}