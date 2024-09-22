using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AyBorg.SDK.Common;

public class Environment : IEnvironment
{
    public Environment(ILogger<Environment> logger, IConfiguration configuration)
    {
        StorageLocation = configuration.GetValue("Storage:Folder", Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "AyBorg", "Storage"))!;
        logger.LogInformation("Environment>Storage>Folder: {StorageLocation}", StorageLocation);
    }

    /// <summary>
    /// Gets the storage location.
    /// </summary>
    public string StorageLocation { get; }
}
