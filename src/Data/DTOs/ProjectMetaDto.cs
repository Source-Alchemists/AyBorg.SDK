using System.ComponentModel.DataAnnotations;
using AyBorg.SDK.Projects;

namespace AyBorg.SDK.Data.DTOs;

public sealed record ProjectMetaDto
{

    /// <summary>
    /// Gets or sets the database identifier.
    /// </summary>
    [Key]
    [Required]
    public Guid DbId { get; set; }

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [Editable(true)]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the version name.
    /// </summary>
    [Editable(true)]
    [StringLength(100, MinimumLength = 1)]
    public string VersionName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the comment.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the approvers.
    /// </summary>
    public string? ApprovedBy { get; set; }
}
