namespace Autodroid.SDK.System.Configuration;

public interface IRegistryConfiguration
{
    string DisplayName { get; }
    string UniqueName { get; }
    string TypeName { get; }
    string Version { get; }
    string Url { get; }
}