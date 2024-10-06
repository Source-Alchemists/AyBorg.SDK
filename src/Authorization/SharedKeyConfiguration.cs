namespace AyBorg.SDK.Authorization;

public record SharedKeyConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="SharedKeyConfiguration"/> is enabled.
    /// </summary>
    public bool Enabled { get; init; } = false;

    /// <summary>
    /// Gets or sets the key value.
    /// </summary>
    public string KeyValue { get; init; } = string.Empty;
}
