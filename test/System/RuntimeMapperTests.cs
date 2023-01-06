using AyBorg.SDK.Common;
using AyBorg.SDK.Common.Models;
using AyBorg.SDK.Common.Ports;
using AyBorg.SDK.ImageProcessing;
using AyBorg.SDK.ImageProcessing.Buffers;
using AyBorg.SDK.ImageProcessing.Pixels;
using AyBorg.SDK.System.Agent;
using Moq;

namespace System;

public class RuntimeMapperTests
{
    private readonly RuntimeMapper _service = new();

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Test_FromRuntime_StepProxy(bool skipPorts)
    {
        // Arrange
        var mockStepProxy = new Mock<IStepProxy>();
        mockStepProxy.Setup(m => m.ExecutionTimeMs).Returns(1);
        mockStepProxy.Setup(m => m.Id).Returns(Guid.NewGuid());
        mockStepProxy.Setup(m => m.MetaInfo).Returns(new PluginMetaInfo
        {
            Id = Guid.NewGuid(),
            AssemblyName = "Test_Assembly",
            AssemblyVersion = "123",
            TypeName = "Test_Type"
        });
        mockStepProxy.Setup(m => m.Name).Returns("Test_Step");
        mockStepProxy.Setup(m => m.X).Returns(2);
        mockStepProxy.Setup(m => m.Y).Returns(3);
        mockStepProxy.Setup(m => m.Ports).Returns(new List<IPort> {
            new StringPort("P1", PortDirection.Input, "Test_Value"),
            new NumericPort("P2", PortDirection.Output, 42),
            new NumericPort("P3", PortDirection.Output, 0),
        });

        IStepProxy stepProxy = mockStepProxy.Object;

        // Act
        Step result = _service.FromRuntime(stepProxy, skipPorts);

        // Assert
        Assert.Equal(stepProxy.Id, result.Id);
        Assert.Equal(stepProxy.Name, result.Name);
        Assert.Equal(stepProxy.X, result.X);
        Assert.Equal(stepProxy.Y, result.Y);
        Assert.Equal(stepProxy.ExecutionTimeMs, result.ExecutionTimeMs);
        Assert.Equal(stepProxy.MetaInfo, result.MetaInfo);

        if (skipPorts)
        {
            Assert.Empty(result.Ports!);
        }
        else
        {
            Assert.Equal(stepProxy.Ports.Count(), result.Ports!.Count());
            Assert.All(result.Ports!, Assert.NotNull);
        }
    }

    [Theory]
    [InlineData(PortBrand.Boolean, true)]
    [InlineData(PortBrand.Boolean, false)]
    [InlineData(PortBrand.Numeric, 42)]
    [InlineData(PortBrand.String, "test")]
    [InlineData(PortBrand.Folder, "/test")]
    [InlineData(PortBrand.Enum, PortDirection.Output)]
    [InlineData(PortBrand.Rectangle, null)]
    [InlineData(PortBrand.Image, null)]
    public void Test_FromRuntime_Port(PortBrand portBrand, object value)
    {
        // Arrange
        if (portBrand == PortBrand.Rectangle)
        {
            value = new AyBorg.SDK.ImageProcessing.Shapes.Rectangle(1, 2, 3, 4);
        }

        if (portBrand == PortBrand.Image)
        {
            value = new Image(new PackedPixelBuffer<Mono8>(2, 2));
        }

        IPort runtimePort = portBrand switch
        {
            PortBrand.Boolean => new BooleanPort("P1", PortDirection.Input, (bool)value),
            PortBrand.Numeric => new NumericPort("P2", PortDirection.Output, Convert.ToDouble(value)),
            PortBrand.String => new StringPort("P3", PortDirection.Input, (string)value),
            PortBrand.Folder => new FolderPort("P4", PortDirection.Output, (string)value),
            PortBrand.Enum => new EnumPort("P5", PortDirection.Input, (PortDirection)value),
            PortBrand.Rectangle => new RectanglePort("P6", PortDirection.Input, (AyBorg.SDK.ImageProcessing.Shapes.Rectangle)value),
            PortBrand.Image => new ImagePort("P7", PortDirection.Input, (Image)value),
            _ => throw new NotImplementedException(),
        };

        // Act
        Port result = _service.FromRuntime(runtimePort);

        // Assert
        Assert.Equal(runtimePort.Id, result.Id);
        Assert.Equal(runtimePort.Name, result.Name);
        Assert.Equal(runtimePort.Direction, result.Direction);
        Assert.Equal(runtimePort.Brand, result.Brand);
        Assert.Equal(runtimePort.IsConnected, result.IsConnected);

        switch (portBrand)
        {
            case PortBrand.Boolean:
                Assert.Equal(((BooleanPort)runtimePort).Value, result.Value);
                break;
            case PortBrand.Numeric:
                Assert.Equal(((NumericPort)runtimePort).Value, Convert.ToDouble(result.Value));
                break;
            case PortBrand.String:
                Assert.Equal(((StringPort)runtimePort).Value, result.Value);
                break;
            case PortBrand.Folder:
                Assert.Equal(((FolderPort)runtimePort).Value, result.Value);
                break;
            case PortBrand.Enum:
                Assert.Equal(((EnumPort)runtimePort).Value, result.Value);
                break;
            case PortBrand.Rectangle:
                Assert.Equal(((RectanglePort)runtimePort).Value, result.Value);
                break;
            case PortBrand.Image:
                Assert.Equal(new CacheImage
                {
                    Width = 2,
                    Height = 2,
                    PixelFormat = PixelFormat.Mono8,
                    OriginalImage = new Image((Image)value)
                }, result.Value);
                break;
        }

    }
}
