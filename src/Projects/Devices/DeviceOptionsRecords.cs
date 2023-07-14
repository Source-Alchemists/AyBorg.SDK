namespace AyBorg.SDK.Projects;

public sealed record AddDeviceOptions(string ProviderName, string DeviceId);

public sealed record ChangeDeviceStateOptions(string DeviceId, bool Activate);
