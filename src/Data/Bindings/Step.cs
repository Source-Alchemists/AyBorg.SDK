using AyBorg.SDK.Common;

namespace AyBorg.SDK.Data.Bindings;

public sealed record Step
{
    // <summary>
    /// Gets or sets the default name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier.
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the x position.
    /// </summary>
    public int X { get; set; } = 0;

    /// <summary>
    /// Gets or sets the y position.
    /// </summary>
    public int Y { get; set; } = 0;

    /// <summary>
    /// Gets or sets the meta information.
    /// </summary>
    public PluginMetaInfo MetaInfo { get; set; } = new PluginMetaInfo();

    /// <summary>
    /// Gets or sets the ports.
    /// </summary>
    public IEnumerable<Port>? Ports { get; set; } = new List<Port>();

    /// <summary>
    /// Gets or sets the execution time in milliseconds.
    /// </summary>
    public long ExecutionTimeMs { get; set; }
}
