using System.Collections.Immutable;

namespace AyBorg.SDK.Common.Ports.Tests;

public class NumericCollectionPortConverterTests
{
    [Fact]
    public void Test_NumericCollection_ConvertTo_String()
    {
        // Arrange
        var collection = new List<double> { 1, 2, 3 };
        var sourcePort = new NumericCollectionPort("SourcePort", PortDirection.Output, collection.ToImmutableList());
        var targetPort = new StringPort("TargetPort", PortDirection.Input, string.Empty);

        // Act
        bool isConvertableResult = PortConverter.IsConvertable(sourcePort, targetPort);
        string convertResult = PortConverter.Convert<string>(sourcePort, targetPort.Value);

        // Assert
        Assert.True(isConvertableResult);
        Assert.Equal("[1,2,3]", convertResult);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Test_NumericCollection_ConvertTo_Boolean(bool isEmpty)
    {
        // Arrange
        var collection = new List<double>();
        if (!isEmpty)
        {
            collection.Add(1);
        }

        var sourcePort = new NumericCollectionPort("SourcePort", PortDirection.Output, collection.ToImmutableList());
        var targetPort = new StringPort("TargetPort", PortDirection.Input, string.Empty);

        // Act
        bool isConvertableResult = PortConverter.IsConvertable(sourcePort, targetPort);
        bool convertResult = PortConverter.Convert<bool>(sourcePort, targetPort.Value);

        // Assert
        Assert.True(isConvertableResult);
        Assert.Equal(!isEmpty, convertResult);
    }

    [Fact]
    public void Test_NumericCollection_ConvertTo_StringCollection()
    {
        // Arrange
        var collection = new List<double> { 1, 2, 3 };
        var sourcePort = new NumericCollectionPort("SourcePort", PortDirection.Output, collection.ToImmutableList());
        var targetPort = new StringCollectionPort("TargetPort", PortDirection.Input, ImmutableList<string>.Empty);

        // Act
        bool isConvertableResult = PortConverter.IsConvertable(sourcePort, targetPort);
        ImmutableList<string> convertResult = PortConverter.Convert<ImmutableList<string>>(sourcePort, targetPort.Value);

        // Assert
        Assert.True(isConvertableResult);
        Assert.Equal(3, convertResult.Count);
        Assert.Equal(new List<string> { "1", "2", "3" }, convertResult);
    }

    [Fact]
    public void Test_NumericCollection_IsNotConvertableToInvalidType()
    {
        // Arrange
        var collection = new List<double> { 1, 2, 3 };
        var sourcePort = new NumericCollectionPort("SourcePort", PortDirection.Output, collection.ToImmutableList());

        // Act / Assert
        using var image = new ImageTorque.Image(new ImageTorque.Buffers.PixelBuffer<ImageTorque.Pixels.L8>(2, 2));
        Assert.False(PortConverter.IsConvertable(sourcePort, new FolderPort("Test", PortDirection.Input, string.Empty)));
        Assert.False(PortConverter.IsConvertable(sourcePort, new RectanglePort("Test", PortDirection.Input, new ImageTorque.Rectangle())));
        Assert.False(PortConverter.IsConvertable(sourcePort, new EnumPort("Test", PortDirection.Input, TestEnum.A)));
        Assert.False(PortConverter.IsConvertable(sourcePort, new ImagePort("Test", PortDirection.Input, image)));
    }

    private enum TestEnum
    {
        A,
        B
    }
}
