namespace AyBorg.SDK.Common;

public sealed class CollectionChangedEventArgs : EventArgs
{
    public IEnumerable<object> NewCollection { get; }
    public IEnumerable<object> AddedItems { get; }
    public IEnumerable<object> RemovedItems { get; }

    public CollectionChangedEventArgs(IEnumerable<object> newCollection, IEnumerable<object> addedItems, IEnumerable<object> removedItems) : base()
    {
        NewCollection = newCollection;
        AddedItems = addedItems;
        RemovedItems = removedItems;
    }
}
