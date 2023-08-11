using AyBorg.SDK.Common.Models;

namespace AyBorg.SDK.Common.Result;

public record PortResult
{
    public string Id { get; init; } = string.Empty;
    public Port Port { get; init; } = new Port();
}
