namespace AyBorg.SDK.Projects;

public sealed record ProjectSettings
{
    public bool IsForceResultCommunicationEnabled { get; set; } = false;

    public bool IsForceWebUiCommunicationEnabled { get; set; } = false;
}
