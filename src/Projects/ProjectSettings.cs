namespace AyBorg.SDK.Projects;

public sealed record ProjectSettings
{
    public bool IsResultCommunicationForced { get; set; } = false;

    public bool IsWebUiCommunicationForced { get; set; } = false;
}
