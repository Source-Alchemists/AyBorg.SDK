namespace AyBorg.SDK.Common.Communication;

public record MessageSubscription : IMessageSubscription {
    public string Id { get; init; } = string.Empty;

    public event EventHandler<MessageEventArgs>? Received;

    public void Next(IMessage message)
    {
        Received?.Invoke(this, new MessageEventArgs(message));
    }
}
