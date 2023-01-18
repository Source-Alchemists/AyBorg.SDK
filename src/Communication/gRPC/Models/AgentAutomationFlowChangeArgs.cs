namespace AyBorg.SDK.Communication.gRPC.Models;

public record AgentAutomationFlowChangeArgs
{
    public string[] AddedSteps { get; set; } = Array.Empty<string>();
    public string[] RemovedSteps { get; set; } = Array.Empty<string>();
    public string[] AddedLinks { get; set; } = Array.Empty<string>();
    public string[] RemovedLinks { get; set; } = Array.Empty<string>();
    public string[] ChangedSteps { get; set; } = Array.Empty<string>();
    public string[] ChangedPorts { get; set; } = Array.Empty<string>();
}
