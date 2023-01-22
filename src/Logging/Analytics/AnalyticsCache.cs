using System.Collections.Concurrent;
using Ayborg.Gateway.Analytics.V1;

namespace AyBorg.SDK.Logging.Analytics;

public sealed class AnalyticsCache
{
    private readonly ConcurrentQueue<EventEntry> _queue = new();

    public void Enqueue(EventEntry eventRequest)
    {
        _queue.Enqueue(eventRequest);
    }

    public bool TryDequeue(out EventEntry eventRequest)
    {
        bool result = _queue.TryDequeue(out EventEntry? er);
        eventRequest = er!;
        return result;
    }
}
