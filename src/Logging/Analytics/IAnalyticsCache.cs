using Ayborg.Gateway.Analytics.V1;

namespace AyBorg.SDK.Logging.Analytics;

public interface IAnalyticsCache
{
    void Enqueue(EventEntry eventRequest);
    bool TryDequeue(out EventEntry eventRequest);
}
