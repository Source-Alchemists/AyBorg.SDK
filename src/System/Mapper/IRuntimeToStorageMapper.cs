using Atomy.SDK.Common;
using Atomy.SDK.Data.DAL;
using Atomy.SDK.Projects;

namespace Atomy.SDK.System.Mapper;

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