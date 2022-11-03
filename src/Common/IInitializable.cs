namespace Atomy.SDK;

public interface IInitializable
{
    /// <summary>
    /// Called when [initialize].
    /// Called once on single run or continuous run before TryRunAsync.
    /// </summary>
    Task OnInitializeAsync();
}
