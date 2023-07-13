namespace AyBorg.SDK.Projects;

public interface IDeviceProxy
{
    string Id { get; }
    string Name { get; }
    IReadOnlyCollection<string> Categories { get; }
}
