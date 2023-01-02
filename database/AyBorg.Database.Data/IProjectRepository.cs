using AyBorg.SDK.Data.DAL;

namespace AyBorg.Database.Data;

public interface IProjectRepository
{
    ValueTask<IEnumerable<ProjectMetaRecord>> GetAllMetasAsync();
    ValueTask<ProjectSettingsRecord> GetSettingsRecordAsync(Guid projectMetaDbId);
    ValueTask<ProjectMetaRecord> GetMetaRecordAsync(Guid projectMetaDbId);
}
