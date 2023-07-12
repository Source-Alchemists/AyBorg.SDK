namespace AyBorg.SDK.Communication;

public record MessageSubscription : IMessageSubscription {
    public string Id { get; init; } = string.Empty;

    public Action<IMessage> Received { get; init; } = null!;
}
