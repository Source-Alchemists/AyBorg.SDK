namespace AyBorg.Hub.Connect;

public record HubClientOptions
{
    public const string AyBorgHubClient = nameof(AyBorgHubClient);
    public Uri HubAddress { get; init; } = null!;
    public bool AllowInsecureChannel { get; init; } = false;
    public string ServiceName { get; init; } = string.Empty;
    public string ServiceUniqueName { get; init; } = Guid.NewGuid().ToString("n");
    public string ServiceType { get; init; } = string.Empty;
    public HubAuditOptions Audit { get; init; } = new HubAuditOptions();
}

public record HubAuditOptions
{
    public const string Audit = nameof(Audit);
    public bool Required { get; init; } = false;
}
