namespace AyBorg.SDK.System.Runtime;

public interface ICommunicationStateProvider
{
    /// <summary>
    /// Gets a value indicating whether the result communication is enabled.
    /// </summary>
    bool IsResultCommunicationEnabled { get; }
}
