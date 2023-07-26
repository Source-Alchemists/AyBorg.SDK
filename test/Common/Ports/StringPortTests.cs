namespace AyBorg.SDK.Common.Ports.Tests;

public class StringPortTests
{
    [Fact]
    public void Test_Properties()
    {
        // Arrange / Act
        var port = new StringPort("TestPort", PortDirection.Input, "Value");

        // Assert
        Assert.Equal("TestPort", port.Name);
        Assert.Equal(PortDirection.Input, port.Direction);
        Assert.Equal("Value", port.Value);
        Assert.Equal(PortBrand.String, port.Brand);
        Assert.NotEqual(Guid.Empty, port.Id);
    }

    [Fact]
    public void Test_CopyConstructor()
    {
        // Arrange
        var port = new StringPort("TestPort", PortDirection.Input, "Value");

        // Act
        var copy = new StringPort(port);

        // Assert
        Assert.Equal("TestPort", copy.Name);
        Assert.Equal(PortDirection.Input, copy.Direction);
        Assert.Equal("Value", copy.Value);
        Assert.Equal(PortBrand.String, copy.Brand);
        Assert.Equal(port.Id, copy.Id);
    }

    [Fact]
    public void Test_UpdateValue()
    {
        // Arrange
        var port = new StringPort("TestPort", PortDirection.Input, "Value");
        var sourcePort = new StringPort("SourcePort", PortDirection.Output, "Value2");

        // Act
        port.UpdateValue(sourcePort);

        // Assert
        Assert.Equal("Value2", port.Value);
    }


}
