using Atomy.SDK.Common;
using Atomy.SDK.Common.Ports;

namespace Atomy.SDK.Data.DTOs;

public record PortDto
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the direction.
    /// </summary>
    public PortDirection Direction { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public object? Value { get; set; }

    /// <summary>
    /// Gets or sets the brand.
    /// </summary>
    public PortBrand Brand { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is connected.
    /// </summary>
    public bool IsConnected { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is link convertable.
    /// </summary>
    public bool IsLinkConvertable { get; set; }
}
