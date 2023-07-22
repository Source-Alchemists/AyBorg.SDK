namespace AyBorg.SDK.Common;

public interface IAfterInitialized
{
    /// <summary>
    /// Called [after initializing].
    /// </summary>
    ValueTask AfterInitializedAsync();
}
