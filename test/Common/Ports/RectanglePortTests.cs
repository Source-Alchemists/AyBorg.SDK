using ImageTorque;

namespace AyBorg.SDK.Common.Ports.Tests;

public class RectanglePortTests
{
    [Fact]
    public void Test_Properties()
    {
        // Arrange / Act
        var port = new RectanglePort("TestPort", PortDirection.Input, new Rectangle(1, 2, 3, 4));

        // Assert
        Assert.Equal("TestPort", port.Name);
        Assert.Equal(PortDirection.Input, port.Direction);
        Assert.Equal(new Rectangle(1, 2, 3, 4), port.Value);
        Assert.Equal(PortBrand.Rectangle, port.Brand);
        Assert.NotEqual(Guid.Empty, port.Id);
    }

    [Fact]
    public void Test_CopyConstructor()
    {
        // Arrange
        var port = new RectanglePort("TestPort", PortDirection.Input, new Rectangle(1, 2, 3, 4));

        // Act
        var copy = new RectanglePort(port);

        // Assert
        Assert.Equal("TestPort", copy.Name);
        Assert.Equal(PortDirection.Input, copy.Direction);
        Assert.Equal(new Rectangle(1, 2, 3, 4), copy.Value);
        Assert.Equal(PortBrand.Rectangle, copy.Brand);
        Assert.Equal(port.Id, copy.Id);
    }

    [Fact]
    public void Test_UpdateValue()
    {
        // Arrange
        var port = new RectanglePort("TestPort", PortDirection.Input, new Rectangle(1, 2, 3, 4));
        var sourcePort = new RectanglePort("SourcePort", PortDirection.Output, new Rectangle(5, 6, 7, 8));

        // Act
        port.UpdateValue(sourcePort);

        // Assert
        Assert.Equal(new Rectangle(5, 6, 7, 8), port.Value);
    }

}
