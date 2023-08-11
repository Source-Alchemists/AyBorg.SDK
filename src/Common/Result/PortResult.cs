using AyBorg.SDK.Common.Models;

namespace AyBorg.SDK.Common.Result;

public record PortResult
{
    public string Id { get; } = string.Empty;
    public Port Port { get; } = new Port();

    public PortResult(string id, Port port)
    {
        Id = id;
        Port = port;
    }
}
