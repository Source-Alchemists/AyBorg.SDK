using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Common.Communication;

public interface ICommunicationDevice : IDevice
{
    ValueTask<bool> TrySendAsync(string messageId, IPort port);

    ValueTask<IMessageSubscription> SubscribeAsync(string messageId);

    ValueTask UnsubscribeAsync(IMessageSubscription subscription);
}
