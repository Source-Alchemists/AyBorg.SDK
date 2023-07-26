namespace AyBorg.SDK.Common;

public interface IPlugin {

    /// <summary>
    /// Gets the default name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the categories.
    /// </summary>
    IReadOnlyCollection<string> Categories { get; }
}
