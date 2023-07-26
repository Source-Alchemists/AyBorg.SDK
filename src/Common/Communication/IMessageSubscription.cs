namespace AyBorg.SDK.Common.Communication;

public interface IMessageSubscription {
    string Id { get; init; }
    event EventHandler<MessageEventArgs> Received;
}
