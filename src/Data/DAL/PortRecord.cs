using System.ComponentModel.DataAnnotations;
using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Data.DAL;

public record PortRecord
{
    /// <summary>
    /// Gets or sets the database identifier.
    /// </summary>
    [Key]
    public Guid DbId { get; set; }

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
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the brand.
    /// </summary>
    public PortBrand Brand { get; set; }

    /// <summary>
    /// Gets or sets the step record identifier.
    /// </summary>
    /// <remarks>Used by entity.</remarks>
    public Guid StepRecordId { get; set; }

    /// <summary>
    /// Gets or sets the step record.
    /// </summary>
    /// <remarks>Used by entity.</remarks>
    public StepRecord StepRecord { get; set; } = new StepRecord();
}