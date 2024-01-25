namespace AyBorg.SDK.System.Configuration;

public interface IGatewayConfiguration
{
    string DisplayName { get; }
    string UniqueName { get; }
    string TypeName { get; }
    string Version { get; }
    bool IsAuditRequired { get; }
}
