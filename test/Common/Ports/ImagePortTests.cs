using ImageTorque;

namespace AyBorg.SDK.Common.Ports.Tests;

public class ImagePortTests
{
    [Fact]
    public void Test_Properties()
    {
        // Arrange / Act
        using var image = Image.Load("./resources/luna.jpg");
        var port = new ImagePort("TestPort", PortDirection.Input, image);

        // Assert
        Assert.Equal("TestPort", port.Name);
        Assert.Equal(PortDirection.Input, port.Direction);
        Assert.Equal(image.Width, port.Value.Width);
        Assert.Equal(PortBrand.Image, port.Brand);
        Assert.NotEqual(Guid.Empty, port.Id);
    }

    [Fact]
    public void Test_CopyConstructor()
    {
        // Arrange
        using var image = Image.Load("./resources/luna.jpg");
        var port = new ImagePort("TestPort", PortDirection.Input, image);

        // Act
        var copy = new ImagePort(port);

        // Assert
        Assert.Equal("TestPort", copy.Name);
        Assert.Equal(PortDirection.Input, copy.Direction);
        Assert.Equal(image.Width, copy.Value.Width);
        Assert.Equal(PortBrand.Image, copy.Brand);
        Assert.Equal(port.Id, copy.Id);
    }

    [Fact]
    public void Test_UpdateValue()
    {
        // Arrange
        using var image = Image.Load("./resources/luna.jpg");
        var port = new ImagePort("TestPort", PortDirection.Input, null!);
        var sourcePort = new ImagePort("SourcePort", PortDirection.Output, image);

        // Act
        port.UpdateValue(sourcePort);

        // Assert
        Assert.Equal(image.Width, port.Value.Width);
    }

}
