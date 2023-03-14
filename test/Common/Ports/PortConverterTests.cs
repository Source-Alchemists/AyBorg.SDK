using System.Collections.ObjectModel;

namespace AyBorg.SDK.Common.Ports.Tests;

public class PortConverterTests
{
    [Fact]
    public void Test_StringCollection_ConvertTo_String()
    {
        // Arrange
        var collection = new List<string> {
            "Test1",
            "Test2",
            "Test3"
        };
        var sourcePort = new StringCollectionPort("SourcePort", PortDirection.Output, new ReadOnlyCollection<string>(collection));
        var targetPort = new StringPort("TargetPort", PortDirection.Input, string.Empty);

        // Act
        bool isConvertableResult = PortConverter.IsConvertable(sourcePort, targetPort);
        string convertResult = PortConverter.Convert<string>(sourcePort, targetPort.Value);

        // Assert
        Assert.True(isConvertableResult);
        Assert.Equal("[\"Test1\",\"Test2\",\"Test3\"]", convertResult);
    }

    [Fact]
    public void Test_Empty_StringCollection_ConvertTo_String()
    {
        // Arrange
        var collection = new List<string>();
        var sourcePort = new StringCollectionPort("SourcePort", PortDirection.Output, new ReadOnlyCollection<string>(collection));
        var targetPort = new StringPort("TargetPort", PortDirection.Input, string.Empty);

        // Act
        bool isConvertableResult = PortConverter.IsConvertable(sourcePort, targetPort);
        string convertResult = PortConverter.Convert<string>(sourcePort, targetPort.Value);

        // Assert
        Assert.True(isConvertableResult);
        Assert.Equal("[]", convertResult);
    }

    [Theory]
    [InlineData("TestString")]
    [InlineData("[]")]
    [InlineData("\"[]\"")]
    public void Test_String_ConvertTo_StringCollection(string expectedString){
        // Arrange
        var targetPort = new StringCollectionPort("SourcePort", PortDirection.Output, new ReadOnlyCollection<string>(new List<string>()));
        var sourcePort = new StringPort("TargetPort", PortDirection.Input, expectedString);

        // Act
        bool isConvertableResult = PortConverter.IsConvertable(sourcePort, targetPort);
        ReadOnlyCollection<string> convertResult = PortConverter.Convert<ReadOnlyCollection<string>>(sourcePort, targetPort.Value);

        // Assert
        Assert.True(isConvertableResult);
        Assert.Single(convertResult);
        Assert.Contains(expectedString, convertResult);
    }

    [Fact]
    public void Test_StringCollection_IsNotConvertableToInvalidType()
    {
        // Arrange
        var collection = new List<string> {
            "Test1",
            "Test2",
            "Test3"
        };
        var sourcePort = new StringCollectionPort("SourcePort", PortDirection.Output, new ReadOnlyCollection<string>(collection));

        // Act / Assert
        Assert.False(PortConverter.IsConvertable(sourcePort, new FolderPort("Test", PortDirection.Input, string.Empty)));
        Assert.False(PortConverter.IsConvertable(sourcePort, new BooleanPort("Test", PortDirection.Input, false)));
        Assert.False(PortConverter.IsConvertable(sourcePort, new RectanglePort("Test", PortDirection.Input, new ImageTorque.Rectangle())));
        Assert.False(PortConverter.IsConvertable(sourcePort, new EnumPort("Test", PortDirection.Input, TestEnum.A)));
    }

    private enum TestEnum {
        A,
        B
    }
}
