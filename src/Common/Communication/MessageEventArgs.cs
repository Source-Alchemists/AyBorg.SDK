namespace AyBorg.SDK.Common.Communication;

public sealed class MessageEventArgs : EventArgs
{
    public IMessage Message { get; }

    public MessageEventArgs(IMessage message) : base()
    {
        Message = message;
    }
}
