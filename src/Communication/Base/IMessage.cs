namespace AyBorg.SDK.Communication;

public interface IMessage {
    string ContentType { get; init; }
    byte[] Payload { get; init; }
}
