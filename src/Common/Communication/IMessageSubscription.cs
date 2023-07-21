namespace AyBorg.SDK.Common.Communication;

public interface IMessageSubscription {
    string Id { get; init; }
    Action<IMessage> Received { get; init; }
}
