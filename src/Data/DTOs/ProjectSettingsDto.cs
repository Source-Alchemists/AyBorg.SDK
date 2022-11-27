namespace AyBorg.SDK.Data.DTOs;

public sealed record ProjectSettingsDto
{
    public bool IsForceResultCommunicationEnabled { get; set; } = false;

    public bool IsForceWebUiCommunicationEnabled { get; set; } = false;
}
