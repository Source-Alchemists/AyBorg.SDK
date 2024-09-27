namespace AyBorg.SDK.Common.Communication;

public record Message : IMessage
{
    public string ContentType { get; init; } = string.Empty;

    public ArraySegment<byte> Payload { get; init; } = ArraySegment<byte>.Empty;
}
