namespace AyBorg.SDK.Common.Communication;

public interface IMessage {
    string ContentType { get; init; }
    ArraySegment<byte> Payload { get; init; }
}
