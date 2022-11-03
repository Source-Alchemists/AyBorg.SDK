using System.ComponentModel.DataAnnotations;
using Atomy.SDK.Projects;

namespace Atomy.SDK.Data.DTOs;

public record ProjectMetaDto
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