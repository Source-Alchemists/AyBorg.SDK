using AyBorg.SDK.Data.DAL;
using AyBorg.SDK.Projects;

namespace AyBorg.Database.Data;

public interface IProjectRepository
{
    ValueTask<IEnumerable<ProjectMetaRecord>> GetAllMetasAsync();
    ValueTask<IEnumerable<ProjectMetaRecord>> GetMetasByProjectIdAsync(Guid projectId);
    ValueTask<ProjectRecord> AddAsync(ProjectRecord project);
    ValueTask<ProjectMetaRecord> AddAsync(ProjectMetaRecord projectMeta);
    ValueTask<ProjectSettingsRecord> AddAsync(ProjectSettingsRecord projectSettings);
    ValueTask<bool> ContainsActiveProjectForServiceAsync(string serviceUniqueName);
    ValueTask<ProjectSettingsRecord> GetSettingAsync(Guid projectMetaDbId);
    ValueTask<ProjectMetaRecord> FindMetaAsync(Guid projectMetaDbId);
    ValueTask<IEnumerable<ProjectMetaRecord>> GetProjectMetasAsync(Guid projectMetaId, ProjectState projectState, long versionIteration);
    ValueTask<IEnumerable<ProjectRecord>> GetProjectsAsync(Guid projectId);
    ValueTask RemoveRangeAsync(IEnumerable<ProjectRecord> projects);
    ValueTask RemoveRangeAsync(IEnumerable<ProjectMetaRecord> projectMetas);
    ValueTask<ProjectRecord> GetProjectAsync(Guid projectMetaId);
    ValueTask SaveChangesAsync();
}
