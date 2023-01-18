namespace AyBorg.SDK.System.Configuration;

public interface IServiceConfiguration : IGatewayConfiguration
{
    string GatewayUrl { get; }
}
