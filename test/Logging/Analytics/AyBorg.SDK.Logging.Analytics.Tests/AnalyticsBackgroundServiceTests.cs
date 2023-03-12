using Ayborg.Gateway.Analytics.V1;
using AyBorg.SDK.System.Configuration;
using Moq;

namespace AyBorg.SDK.Logging.Analytics.Tests;

public class AnalyticsBackgroundServiceTests
{
    private readonly Mock<IServiceConfiguration> _mockConfiguration = new();
    private readonly Mock<IAnalyticsCache> _mockCache = new();
    private readonly Mock<EventLog.EventLogClient> _mockClient = new();
    private readonly AnalyticsBackgroundService _service;

    public AnalyticsBackgroundServiceTests()
    {
        _service = new AnalyticsBackgroundService(_mockConfiguration.Object, _mockCache.Object, _mockClient.Object);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Test_BackgroundDequeue(bool canDequeue)
    {
        // Arrange
        CancellationTokenSource tokenSource = new();
        EventEntry entry;
        _mockCache.Setup(m => m.TryDequeue(out entry)).Returns(canDequeue);

        // Act
        await _service.StartAsync(tokenSource.Token);
        await Task.Delay(100);
        await _service.StopAsync(tokenSource.Token);
        tokenSource.Cancel();

        // Assert
        _mockCache.Verify(m => m.TryDequeue(out entry), Times.AtLeastOnce);
    }
}
