using Microsoft.Extensions.Configuration;

namespace Atomy.SDK.Common;

public class Environment : IEnvironment
{
    public Environment(IConfiguration configuration)
    {
        StorageLocation = configuration.GetValue("Storage:Folder", Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Atomy", "Storage"));
    }

    /// <summary>
    /// Gets the storage location.
    /// </summary>
    public string StorageLocation { get; }
}