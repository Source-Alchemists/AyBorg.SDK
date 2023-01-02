using AyBorg.SDK.Data.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AyBorg.Database.Data;

public sealed class ProjectRepository : IProjectRepository
{
    public ILogger<ProjectRepository> _logger;
    private readonly IDbContextFactory<ProjectContext> _contextFactory;

    public ProjectRepository(ILogger<ProjectRepository> logger, IDbContextFactory<ProjectContext> contextFactory)
    {
        _logger = logger;
        _contextFactory = contextFactory;
    }

    public async ValueTask<IEnumerable<ProjectMetaRecord>> GetAllMetasAsync()
    {
        using ProjectContext context = await _contextFactory.CreateDbContextAsync();
        return await context.AyBorgProjectMetas!.ToListAsync();
    }

    public async ValueTask<ProjectSettingsRecord> GetSettingsRecordAsync(Guid projectMetaDbId)
    {
        using ProjectContext context = await _contextFactory.CreateDbContextAsync();
        ProjectRecord? projectRecord = await context.AyBorgProjects!.Include(x => x.Settings).FirstOrDefaultAsync(x => x.Meta.DbId.Equals(projectMetaDbId));
        if (projectRecord == null)
        {
            _logger.LogWarning("No project found with id [{projectDatabaseId}].", projectMetaDbId);
            return null!;
        }

        return projectRecord.Settings;
    }

    public async ValueTask<ProjectMetaRecord> GetMetaRecordAsync(Guid projectMetaDbId)
    {
        IEnumerable<ProjectMetaRecord> projectMetas = await GetAllMetasAsync();
        ProjectMetaRecord? projectMeta = projectMetas.FirstOrDefault(p => p.DbId == projectMetaDbId);
        if(projectMeta == null)
        {
            _logger.LogWarning("No settings found for project {projectMetaDbId}.", projectMetaDbId);

        }

        return projectMeta!;
    }
}
