using AyBorg.SDK.Common;

namespace AyBorg.SDK.Data.DTOs;

public sealed record StepDto
{
    /// <summary>
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
    public IEnumerable<PortDto>? Ports { get; set; } = new List<PortDto>();

    /// <summary>
    /// Gets or sets the execution time in milliseconds.
    /// </summary>
    public long ExecutionTimeMs { get; set; }
}
