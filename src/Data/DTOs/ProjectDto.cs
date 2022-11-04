namespace Atomy.SDK.Data.DTOs;

public sealed record ProjectDto
{
    
    /// <summary>
    /// Gets or sets the meta informations.
    /// </summary>
    public ProjectMetaDto Meta { get; set; } = new ProjectMetaDto();

    /// <summary>
    /// Gets or sets the steps.
    /// </summary>
    public List<StepDto> Steps { get; set; } = new List<StepDto>();

    /// <summary>
    /// Gets or sets the links.
    /// </summary>
    public List<LinkDto> Links { get; set; } = new List<LinkDto>();
}
