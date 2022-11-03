﻿using System.ComponentModel.DataAnnotations;
using Atomy.SDK.Common;

namespace Atomy.SDK.Data.DAL;

public record StepRecord
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
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the meta information.
    /// </summary>
    public PluginMetaInfo MetaInfo { get; set; } = new PluginMetaInfo();

    /// <summary>
    /// Gets or sets the x.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Gets or sets the y.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Gets or sets the ports.
    /// </summary>
    public List<PortRecord> Ports { get; set; } = new List<PortRecord>();

    /// <summary>
    /// Gets or sets the project record identifier.
    /// </summary>
    /// <remarks>Used by entity.</remarks>
    public Guid ProjectRecordId { get; set; }

    /// <summary>
    /// Gets or sets the project record.
    /// </summary>
    /// <remarks>Used by entity.</remarks>
    public ProjectRecord ProjectRecord { get; set; } = new ProjectRecord();
}