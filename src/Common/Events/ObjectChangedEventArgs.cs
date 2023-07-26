namespace AyBorg.SDK.Common;

public sealed class ObjectChangedEventArgs : EventArgs
{
    public object Object { get; }

    public ObjectChangedEventArgs(object obj) : base()
    {
        Object = obj;
    }
}
