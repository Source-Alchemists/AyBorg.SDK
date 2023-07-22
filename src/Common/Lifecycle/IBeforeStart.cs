namespace AyBorg.SDK.Common;

public interface IBeforeStart
{
    /// <summary>
    /// Called [before start].
    /// Called once on single run or continuous run before TryRunAsync.
    /// </summary>
    ValueTask BeforeStartAsync();
}
