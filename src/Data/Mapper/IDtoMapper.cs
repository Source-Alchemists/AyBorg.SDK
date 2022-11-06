using Autodroid.SDK.Data.DAL;
using Autodroid.SDK.Data.DTOs;
using Autodroid.SDK.Common.Ports;
using Autodroid.SDK.Common;

namespace Autodroid.SDK.Data.Mapper;

public interface IDtoMapper
{
    /// <summary>
    /// Gets the mapper, use with caution.
    /// </summary>
    AutoMapper.Mapper Mapper { get; }
    
    /// <summary>
    /// Maps the specified project meta record.
    /// </summary>
    /// <param name="projectMetaRecord">The project meta record.</param>
    /// <returns></returns>
    ProjectMetaDto Map(ProjectMetaRecord projectMetaRecord);
    
    /// <summary>
    /// Maps the specified project to record.
    /// </summary>
    /// <param name="projectRecord">The project record.</param>
    /// <returns></returns>
    ProjectDto Map(ProjectRecord projectRecord);

    /// <summary>
    /// Maps the specified port to dto.
    /// </summary>
    PortDto Map(PortRecord portRecord);

    /// <summary>
    /// Maps the specified project dto.
    /// </summary>
    /// <param name="projectDto">The project dto.</param>
    /// <returns></returns>
    ProjectRecord Map(ProjectDto projectDto);

    /// <summary>
    /// Maps the specified step body.
    /// </summary>
    /// <param name="step">The step.</param>
    /// <returns></returns>
    StepDto Map(IStepProxy step);

    /// <summary>
    /// Maps the specified port.
    /// </summary>
    /// <param name="port">The port.</param>
    /// <returns></returns>
    PortDto Map(IPort port);

    /// <summary>
    /// Maps the specified link.
    /// </summary>
    /// <param name="link">The link.</param>
    /// <returns></returns>
    LinkDto Map(PortLink link);
}
