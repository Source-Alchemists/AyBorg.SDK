namespace AyBorg.SDK.Common.Ports.Tests;

public class BooleanPortTests
{
    [Fact]
    public void Test_Properties()
    {
        // Arrange / Act
        var port = new BooleanPort("TestPort", PortDirection.Input, true);

        // Assert
        Assert.Equal("TestPort", port.Name);
        Assert.Equal(PortDirection.Input, port.Direction);
        Assert.True(port.Value);
        Assert.Equal(PortBrand.Boolean, port.Brand);
        Assert.NotEqual(Guid.Empty, port.Id);
    }

    [Fact]
    public void Test_CopyConstructor()
    {
        // Arrange
        var port = new BooleanPort("TestPort", PortDirection.Input, true);

        // Act
        var copy = new BooleanPort(port);

        // Assert
        Assert.Equal("TestPort", copy.Name);
        Assert.Equal(PortDirection.Input, copy.Direction);
        Assert.True(copy.Value);
        Assert.Equal(PortBrand.Boolean, copy.Brand);
        Assert.Equal(port.Id, copy.Id);
    }

    [Fact]
    public void Test_UpdateValue()
    {
        // Arrange
        var port = new BooleanPort("TestPort", PortDirection.Input, true);
        var sourcePort = new BooleanPort("SourcePort", PortDirection.Output, false);

        // Act
        port.UpdateValue(sourcePort);

        // Assert
        Assert.False(port.Value);
    }
}
