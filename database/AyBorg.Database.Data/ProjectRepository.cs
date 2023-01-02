using AyBorg.SDK.Data.DAL;
using AyBorg.SDK.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AyBorg.Database.Data;

public sealed class ProjectRepository : IProjectRepository, IDisposable
{
    public ILogger<ProjectRepository> _logger;
    private readonly IDbContextFactory<ProjectContext> _contextFactory;
    private ProjectContext _context = null!;
    private bool _disposedValue;

    public ProjectRepository(ILogger<ProjectRepository> logger, IDbContextFactory<ProjectContext> contextFactory)
    {
        _logger = logger;
        _contextFactory = contextFactory;
    }

    public async ValueTask<IEnumerable<ProjectMetaRecord>> GetAllMetasAsync()
    {
        ProjectContext context = await GetProjectContextAsync();
        return await context.AyBorgProjectMetas!.ToListAsync();
    }

    public async ValueTask<ProjectMetaRecord> FindMetaAsync(Guid projectMetaDbId)
    {
        ProjectContext context = await GetProjectContextAsync();
        ProjectMetaRecord? projectMeta = await context.AyBorgProjectMetas!.FindAsync(projectMetaDbId);
        if (projectMeta == null)
        {
            _logger.LogWarning("No settings found for project {projectMetaDbId}.", projectMetaDbId);

        }

        return projectMeta!;
    }

    public async ValueTask<IEnumerable<ProjectMetaRecord>> GetProjectMetasAsync(Guid projectMetaId, ProjectState projectState, long versionIteration)
    {
        ProjectContext context = await GetProjectContextAsync();
        return await context.AyBorgProjectMetas!.Where(pm => pm.Id.Equals(projectMetaId)
                                                                && pm.State == projectState
                                                                && pm.VersionIteration.Equals(versionIteration)).ToListAsync();
    }

    public async ValueTask<IEnumerable<ProjectMetaRecord>> GetMetasByProjectIdAsync(Guid projectId)
    {
        ProjectContext context = await GetProjectContextAsync();
        return await context.AyBorgProjectMetas!.Where(pm => pm.Id.Equals(projectId)).ToListAsync();
    }

    public async ValueTask<IEnumerable<ProjectRecord>> GetProjectsAsync(Guid projectId)
    {
        ProjectContext context = await GetProjectContextAsync();
        return await context.AyBorgProjects!.Where(p => p.Meta.Id.Equals(projectId)).ToListAsync();
    }

    public async ValueTask<ProjectRecord> GetProjectAsync(Guid projectMetaId)
    {
        ProjectContext context = await GetProjectContextAsync();
        IQueryable<ProjectRecord> queryProject = CreateFullProjectQuery(context);
        ProjectRecord orgProjectRecord = await queryProject.FirstAsync(x => x.Meta.DbId.Equals(projectMetaId));
        return orgProjectRecord;
    }

    public async ValueTask<ProjectRecord> AddAsync(ProjectRecord project)
    {
        ProjectContext context = await GetProjectContextAsync();
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<ProjectRecord> result = await context.AyBorgProjects!.AddAsync(project);
        return result.Entity;
    }

    public async ValueTask<ProjectMetaRecord> AddAsync(ProjectMetaRecord projectMeta)
    {
        ProjectContext context = await GetProjectContextAsync();
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<ProjectMetaRecord> result = await context.AyBorgProjectMetas!.AddAsync(projectMeta);
        return result.Entity;
    }

    public async ValueTask<ProjectSettingsRecord> AddAsync(ProjectSettingsRecord projectSettings)
    {
        ProjectContext context = await GetProjectContextAsync();
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<ProjectSettingsRecord> result = await context.AyBorgProjectSettings!.AddAsync(projectSettings);
        return result.Entity;
    }

    public async ValueTask<bool> ContainsActiveProjectForServiceAsync(string serviceUniqueName)
    {
        using ProjectContext context = await _contextFactory.CreateDbContextAsync();
        return context.AyBorgProjects!.Any(p => p.Meta.IsActive && p.Meta.ServiceUniqueName == serviceUniqueName);
    }

    public async ValueTask<ProjectSettingsRecord> GetSettingAsync(Guid projectMetaDbId)
    {
        ProjectContext context = await GetProjectContextAsync();
        ProjectRecord? projectRecord = await context.AyBorgProjects!.Include(x => x.Settings).FirstOrDefaultAsync(x => x.Meta.DbId.Equals(projectMetaDbId));
        if (projectRecord == null)
        {
            _logger.LogWarning("No project found with id [{projectDatabaseId}].", projectMetaDbId);
            return null!;
        }

        return projectRecord.Settings;
    }

    public async ValueTask RemoveRangeAsync(IEnumerable<ProjectRecord> projects)
    {
        ProjectContext context = await GetProjectContextAsync();
        context.AyBorgProjects!.RemoveRange(projects);
    }

    public async ValueTask RemoveRangeAsync(IEnumerable<ProjectMetaRecord> projectMetas)
    {
        ProjectContext context = await GetProjectContextAsync();
        context.AyBorgProjectMetas!.RemoveRange(projectMetas);
    }

    public async ValueTask SaveChangesAsync()
    {
        ProjectContext context = await GetProjectContextAsync();
        await context.SaveChangesAsync();
    }

    private async ValueTask<ProjectContext> GetProjectContextAsync()
    {
        _context ??= await _contextFactory.CreateDbContextAsync();
        return _context;
    }

    private static IQueryable<ProjectRecord> CreateFullProjectQuery(ProjectContext context)
    {
        return context.AyBorgProjects!.Include(x => x.Meta)
                                        .Include(x => x.Settings)
                                        .Include(x => x.Steps)
                                        .ThenInclude(x => x.MetaInfo)
                                        .Include(x => x.Steps)
                                        .ThenInclude(x => x.Ports)
                                        .Include(x => x.Links)
                                        .AsSplitQuery();
    }

    private void Dispose(bool disposing)
    {
        if (!_disposedValue && disposing)
        {
            _context?.Dispose();
            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
