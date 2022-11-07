using System.ComponentModel.DataAnnotations;
using Autodroid.SDK.Projects;

namespace Autodroid.SDK.Data.DTOs;

public sealed record ProjectMetaDto
{

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [Editable(true)]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Editable(true)]
    [StringLength(100, MinimumLength = 1)]
    public string VersionName { get; set; } = string.Empty;

    [Editable(true)]
    [StringLength(200)]
    public string Comment { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    public DateTime UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    public ProjectState State { get; set; } = ProjectState.Draft;
}