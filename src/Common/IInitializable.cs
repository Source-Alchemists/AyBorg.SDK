namespace AyBorg.SDK.Common;

public interface IInitializable
{
    /// <summary>
    /// Called when [initialize].
    /// Called once on single run or continuous run before TryRunAsync.
    /// </summary>
    ValueTask OnInitializeAsync();
}
