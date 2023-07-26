namespace AyBorg.SDK.Common.Ports.Tests;

public class SelectPortTests
{
    [Fact]
    public void Test_Properties()
    {
        // Arrange / Act
        var port = new SelectPort("TestPort", PortDirection.Input, new SelectPort.ValueContainer("Value", new List<string> { "Value" }));

        // Assert
        Assert.Equal("TestPort", port.Name);
        Assert.Equal(PortDirection.Input, port.Direction);
        Assert.Equal("Value", port.Value.SelectedValue);
        Assert.Equal(PortBrand.Select, port.Brand);
        Assert.NotEqual(Guid.Empty, port.Id);
    }

    [Fact]
    public void Test_CopyConstructor()
    {
        // Arrange
        var port = new SelectPort("TestPort", PortDirection.Input, new SelectPort.ValueContainer("Value", new List<string> { "Value" }));

        // Act
        var copy = new SelectPort(port);

        // Assert
        Assert.Equal("TestPort", copy.Name);
        Assert.Equal(PortDirection.Input, copy.Direction);
        Assert.Equal("Value", copy.Value.SelectedValue);
        Assert.Equal(PortBrand.Select, copy.Brand);
        Assert.Equal(port.Id, copy.Id);
    }

    [Fact]
    public void Test_UpdateValue()
    {
        // Arrange
        var port = new SelectPort("TestPort", PortDirection.Input, new SelectPort.ValueContainer("Value", new List<string> { "Value" }));
        var sourcePort = new SelectPort("SourcePort", PortDirection.Output, new SelectPort.ValueContainer("Value2", new List<string> { "Value2" }));

        // Act
        port.UpdateValue(sourcePort);

        // Assert
        Assert.Equal("Value2", port.Value.SelectedValue);
    }

}
