namespace AyBorg.SDK.Authorization;

public record SecurityConfiguration
{
    /// <summary>
    /// Gets or sets the primary shared key.
    /// </summary>
    public SharedKeyConfiguration PrimarySharedKey { get; init; } = new();
}
