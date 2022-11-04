using System.ComponentModel.DataAnnotations;

namespace Atomy.SDK.Data.DTOs;

/// <summary>
/// Service registry entry.
/// </summary>
public sealed record ServiceRegistryEntryDto
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public Guid Id { get; set; }

    /// <summary>
    /// The service name.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The unique service name.
    /// </summary>
    [Required]
    public string UniqueName { get; set; } = string.Empty;

    /// <summary>
    /// The service type.
    /// </summary>
    [Required]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// The service address (e.g. https://myservice).
    /// </summary>
    [Required]
    [Url]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the version.
    /// </summary>
    /// <value>
    /// The version.
    /// </value>
    [Required]
    public string Version { get; set; } = string.Empty;
}