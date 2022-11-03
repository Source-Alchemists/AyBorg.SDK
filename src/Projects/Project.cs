using Atomy.SDK.Common;
using Atomy.SDK.Common.Ports;

namespace Atomy.SDK.Projects;

public class Project
{
    /// <summary>
    /// Gets or sets the meta informations.
    /// </summary>
    public ProjectMeta Meta { get; set; } = new ProjectMeta();

    /// <summary>
    /// Gets or sets the steps.
    /// </summary>
    public ICollection<IStepProxy> Steps { get; set; } = new List<IStepProxy>();

    /// <summary>
    /// Gets or sets the links.
    /// </summary>
    public ICollection<PortLink> Links { get; set; } = new List<PortLink>();
}
