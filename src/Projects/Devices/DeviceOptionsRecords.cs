using AyBorg.SDK.Common.Models;

namespace AyBorg.SDK.Projects;

public sealed record AddDeviceOptions(string ProviderName, string DeviceId, bool IsActive = false);

public sealed record ChangeDeviceStateOptions(string DeviceId, bool Activate);

public sealed record UpdateDeviceOptions(string DeviceId, IEnumerable<Port> Ports);
