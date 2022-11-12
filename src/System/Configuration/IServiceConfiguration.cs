namespace AyBorg.SDK.System.Configuration;

public interface IServiceConfiguration : IRegistryConfiguration
{
    string RegistryUrl { get; }
}