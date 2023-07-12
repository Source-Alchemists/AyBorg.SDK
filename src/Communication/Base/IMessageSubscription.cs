namespace AyBorg.SDK.Communication;

public interface IMessageSubscription {
    string Id { get; init; }
    Action<IMessage> Received { get; init; }
}
