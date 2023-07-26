namespace AyBorg.SDK.Common.Communication;

public interface IMessage {
    string ContentType { get; init; }
    byte[] Payload { get; init; }
}
