using MQTTnet.Protocol;

namespace AyBorg.SDK.Communication.MQTT;

public sealed record MqttPublishOptions
{
    public MqttQualityOfServiceLevel QualityOfServiceLevel { get; init; } = MqttQualityOfServiceLevel.AtLeastOnce;
    public bool Retain { get; init; } = false;
    public bool Resize { get; init; } = false;
    public string EncoderType { get; init; } = "jpeg";
    public int Quality { get; init; } = 80;
}
