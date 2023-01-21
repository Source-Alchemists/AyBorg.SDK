using System.Collections.Concurrent;
using Ayborg.Gateway.Analytics.V1;

namespace AyBorg.SDK.Logging.Analytics;

public sealed class AnalyticsCache
{
    private readonly ConcurrentQueue<EventRequest> _queue = new();

    public void Enqueue(EventRequest eventRequest)
    {
        _queue.Enqueue(eventRequest);
    }

    public bool TryDequeue(out EventRequest eventRequest)
    {
        bool result = _queue.TryDequeue(out EventRequest? er);
        eventRequest = er!;
        return result;
    }
}
