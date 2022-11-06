using System.ComponentModel.DataAnnotations;
using Autodroid.SDK.Projects;

namespace Autodroid.SDK.Data.DAL;

public record ProjectMetaRecord
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    [Key]
    public Guid DbId { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [Editable(true)]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    /// <value>
    /// The created date.
    /// </value>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    /// <value>
    /// The updated date.
    /// </value>
    public DateTime UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is active.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
    /// </value>
    public bool IsActive { get; set; } = false;

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    public ProjectState State { get; set; } = ProjectState.Draft;

    /// <summary>
    /// Gets or sets the unique name of the service. As hint wich service is responsible for this project.
    /// </summary>
    public string ServiceUniqueName {get; set;} = string.Empty;

    /// <summary>
    /// Gets or sets the project record identifier.
    /// </summary>
    /// <remarks>Used for navigation.</remarks>
    public Guid ProjectRecordId { get; set; }
}