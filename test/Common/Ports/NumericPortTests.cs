namespace AyBorg.SDK.Common.Ports.Tests;

public class NumericPortTests
{
    [Fact]
    public void Test_Properties()
    {
        // Arrange / Act
        var port = new NumericPort("TestPort", PortDirection.Input, 1.0, 0.0, 2.0);

        // Assert
        Assert.Equal("TestPort", port.Name);
        Assert.Equal(PortDirection.Input, port.Direction);
        Assert.Equal(1.0, port.Value);
        Assert.Equal(0.0, port.Min);
        Assert.Equal(2.0, port.Max);
        Assert.Equal(PortBrand.Numeric, port.Brand);
        Assert.NotEqual(Guid.Empty, port.Id);
    }

    [Fact]
    public void Test_CopyConstructor()
    {
        // Arrange
        var port = new NumericPort("TestPort", PortDirection.Input, 1.0, 0.0, 2.0);

        // Act
        var copy = new NumericPort(port);

        // Assert
        Assert.Equal("TestPort", copy.Name);
        Assert.Equal(PortDirection.Input, copy.Direction);
        Assert.Equal(1.0, copy.Value);
        Assert.Equal(0.0, copy.Min);
        Assert.Equal(2.0, copy.Max);
        Assert.Equal(PortBrand.Numeric, copy.Brand);
        Assert.Equal(port.Id, copy.Id);
    }

    [Fact]
    public void Test_UpdateValue()
    {
        // Arrange
        var port = new NumericPort("TestPort", PortDirection.Input, 1.0, 0.0, 2.0);
        var sourcePort = new NumericPort("SourcePort", PortDirection.Output, 2.0, 0.0, 2.0);

        // Act
        port.UpdateValue(sourcePort);

        // Assert
        Assert.Equal(2.0, port.Value);

    }
}
