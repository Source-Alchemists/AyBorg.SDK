namespace AyBorg.SDK.Common.Communication;

public record Message : IMessage
{
    public string ContentType { get; init; } = string.Empty;

    public byte[] Payload { get; init; } = Array.Empty<byte>();
}
