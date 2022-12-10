namespace AyBorg.SDK.System.Configuration;

public interface IGatewayConfiguration : IRegistryConfiguration
{
    string GatewayUrl { get; }
}
