namespace AyBorg.SDK.Data.DTOs;

public sealed record ProjectSettingsDto
{
    public bool IsResultCommunicationForced { get; set; } = false;

    public bool IsWebUiCommunicationForced { get; set; } = false;
}
