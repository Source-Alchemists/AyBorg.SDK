namespace AyBorg.Hub.Connect.Models;

public sealed record NotifyParameters(string ServiceId, AgentNotifyType NotifyType, string Payload);
public sealed record RegisterParameters(string ServiceId, string Name, string Type, string Version);
