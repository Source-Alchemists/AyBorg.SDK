using Autodroid.SDK.Common;
using Autodroid.SDK.Data.DAL;
using Autodroid.SDK.Projects;

namespace Autodroid.SDK.Data.Mapper;

public interface IRuntimeToStorageMapper
{
    /// <summary>
    /// Maps the specified step proxy.
    /// </summary>
    /// <param name="stepProxy">The step proxy.</param>
    /// <returns></returns>
    StepRecord Map(IStepProxy stepProxy);
    
    /// <summary>
    /// Maps the specified project.
    /// </summary>
    /// <param name="project">The project.</param>
    /// <returns></returns>
    ProjectRecord Map(Project project);
}