using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace AyBorg.SDK.Logging.Analytics;

public static class AnalyticsLoggerMiddleware
{
    public static ILoggingBuilder AddAyBorgAnalyticsLogger(this ILoggingBuilder builder)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, AnalyticsLoggerProvider>());
        return builder;
    }

    public static WebApplicationBuilder AddAyBorgAnalyticsLogger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<AnalyticsCache>();
        builder.Services.AddHostedService<AnalyticsBackgroundService>();
        builder.Logging.AddAyBorgAnalyticsLogger();
        return builder;
    }
}
