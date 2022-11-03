using Atomy.SDK.ImageProcessing.Encoding;
using MQTTnet.Protocol;

namespace Atomy.SDK.Communication.MQTT;

public record MqttPublishOptions
{
    public MqttQualityOfServiceLevel QualityOfServiceLevel { get; init; } = MqttQualityOfServiceLevel.AtLeastOnce;
    public bool Retain { get; init; } = false;
    public bool Resize { get; init; } = false;
    public EncoderType EncoderType { get; init; } = EncoderType.Jpeg;
    public int Quality { get; init; } = 80;
}