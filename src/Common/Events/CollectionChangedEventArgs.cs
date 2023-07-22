namespace AyBorg.SDK.Common;

public sealed class CollectionChangedEventArgs : EventArgs
{
    public IEnumerable<object> NewCollection { get; }

    public CollectionChangedEventArgs(IEnumerable<object> newCollection) : base()
    {
        NewCollection = newCollection;
    }
}
