namespace AyBorg.SDK.Common.Ports.Tests;

public class EnumPortTests
{
    [Fact]
    public void Test_Properties()
    {
        // Arrange / Act
        var port = new EnumPort("TestPort", PortDirection.Input, TestEnum.Value1);

        // Assert
        Assert.Equal("TestPort", port.Name);
        Assert.Equal(PortDirection.Input, port.Direction);
        Assert.Equal(TestEnum.Value1, port.Value);
        Assert.Equal(PortBrand.Enum, port.Brand);
        Assert.NotEqual(Guid.Empty, port.Id);
    }

    [Fact]
    public void Test_CopyConstructor()
    {
        // Arrange
        var port = new EnumPort("TestPort", PortDirection.Input, TestEnum.Value1);

        // Act
        var copy = new EnumPort(port);

        // Assert
        Assert.Equal("TestPort", copy.Name);
        Assert.Equal(PortDirection.Input, copy.Direction);
        Assert.Equal(TestEnum.Value1, copy.Value);
        Assert.Equal(PortBrand.Enum, copy.Brand);
        Assert.Equal(port.Id, copy.Id);
    }

    [Fact]
    public void Test_UpdateValue()
    {
        // Arrange
        var port = new EnumPort("TestPort", PortDirection.Input, TestEnum.Value1);
        var sourcePort = new EnumPort("SourcePort", PortDirection.Output, TestEnum.Value2);

        // Act
        port.UpdateValue(sourcePort);

        // Assert
        Assert.Equal(TestEnum.Value2, port.Value);
    }

    public enum TestEnum
    {
        Value1,
        Value2
    }

}
