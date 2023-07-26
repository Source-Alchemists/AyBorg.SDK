namespace AyBorg.SDK.Common;

public interface IStepBody : IPortProviderPlugin
{
    /// <summary>
    /// Tries to run asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if successful.</returns>
    ValueTask<bool> TryRunAsync(CancellationToken cancellationToken);
}
