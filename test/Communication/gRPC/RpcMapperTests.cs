using Ayborg.Gateway.Agent.V1;
using AyBorg.SDK.Common.Models;
using AyBorg.SDK.Common.Ports;
using Moq;

namespace AyBorg.SDK.Communication.gRPC.Tests;

public class RpcMapperTests
{
    private readonly RpcMapper _service = new();

    [Fact]
    public void Test_FromRpc_StepDto()
    {
        // Arrange
        var dto = new StepDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Test_Dto",
            X = 1,
            Y = 2,
            ExecutionTimeMs = 3,
            MetaInfo = new PluginMetaDto
            {
                Id = Guid.NewGuid().ToString(),
                AssemblyName = "Test_Assembly",
                AssemblyVersion = "123",
                TypeName = "Test_Type"
            }
        };
        dto.Ports.Add(new PortDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Test_Port_Dto",
            Direction = (int)PortDirection.Input,
            Brand = (int)PortBrand.Numeric,
            IsConnected = true,
            IsLinkConvertable = true,
            Value = "42"
        });

        // Act
        Step result = _service.FromRpc(dto);

        // Assert
        Assert.Equal(dto.Id, result.Id.ToString());
        Assert.Equal(dto.Name, result.Name);
        Assert.Equal(dto.X, result.X);
        Assert.Equal(dto.Y, result.Y);
        Assert.Equal(dto.ExecutionTimeMs, result.ExecutionTimeMs);
        Assert.Equal(dto.MetaInfo.Id, result.MetaInfo.Id.ToString());
        Assert.Equal(dto.MetaInfo.AssemblyName, result.MetaInfo.AssemblyName);
        Assert.Equal(dto.MetaInfo.AssemblyVersion, result.MetaInfo.AssemblyVersion);
        Assert.Equal(dto.MetaInfo.TypeName, result.MetaInfo.TypeName);
        Assert.Equal(dto.Ports[0].Id, result.Ports!.First().Id.ToString());
        Assert.Equal(dto.Ports[0].Name, result.Ports!.First().Name);
        Assert.Equal(dto.Ports[0].Direction, (int)result.Ports!.First().Direction);
        Assert.Equal(dto.Ports[0].Brand, (int)result.Ports!.First().Brand);
        Assert.Equal(dto.Ports[0].IsConnected, result.Ports!.First().IsConnected);
        Assert.Equal(dto.Ports[0].IsLinkConvertable, result.Ports!.First().IsLinkConvertable);
        Assert.Equal(Convert.ToDouble(dto.Ports[0].Value), (double)result.Ports!.First().Value!);
    }

    [Fact]
    public void Test_ToRpc_Step()
    {
        // Arrange
        var step = new Step
        {
            Id = Guid.NewGuid(),
            Name = "Test_Step",
            X = 1,
            Y = 2,
            ExecutionTimeMs = 3,
            MetaInfo = new AyBorg.SDK.Common.PluginMetaInfo
            {
                Id = Guid.NewGuid(),
                AssemblyName = "Test_Assembly",
                AssemblyVersion = "123",
                TypeName = "Test_Type"
            }
        };
        step.Ports = step.Ports!.Append(new Port
        {
            Id = Guid.NewGuid(),
            Name = "Test_Port",
            Direction = PortDirection.Input,
            Brand = PortBrand.Numeric,
            IsConnected = true,
            IsLinkConvertable = true,
            Value = 42
        });

        // Act
        StepDto result = _service.ToRpc(step);

        // Assert
        Assert.Equal(step.Id.ToString(), result.Id);
        Assert.Equal(step.Name, result.Name);
        Assert.Equal(step.X, result.X);
        Assert.Equal(step.Y, result.Y);
        Assert.Equal(step.ExecutionTimeMs, result.ExecutionTimeMs);
        Assert.Equal(step.MetaInfo.Id.ToString(), result.MetaInfo.Id);
        Assert.Equal(step.MetaInfo.AssemblyName, result.MetaInfo.AssemblyName);
        Assert.Equal(step.MetaInfo.AssemblyVersion, result.MetaInfo.AssemblyVersion);
        Assert.Equal(step.MetaInfo.TypeName, result.MetaInfo.TypeName);
        Assert.Equal(step.Ports!.First().Id.ToString(), result.Ports[0].Id);
        Assert.Equal(step.Ports!.First().Name, result.Ports[0].Name);
        Assert.Equal((int)step.Ports!.First().Direction, result.Ports[0].Direction);
        Assert.Equal((int)step.Ports!.First().Brand, result.Ports[0].Brand);
        Assert.Equal(step.Ports!.First().IsConnected, result.Ports[0].IsConnected);
        Assert.Equal(step.Ports!.First().IsLinkConvertable, result.Ports[0].IsLinkConvertable);
        Assert.Equal(step.Ports!.First().Value!.ToString(), result.Ports[0].Value);
    }

    [Fact]
    public void Test_FromRpc_PluginMetaDto()
    {
        // Arrange
        var dto = new PluginMetaDto
        {
            Id = Guid.NewGuid().ToString(),
            AssemblyName = "Test_Assembly",
            AssemblyVersion = "123",
            TypeName = "Test_Type"
        };

        // Act
        AyBorg.SDK.Common.PluginMetaInfo result = _service.FromRpc(dto);

        // Assert
        Assert.Equal(dto.Id, result.Id.ToString());
        Assert.Equal(dto.AssemblyName, result.AssemblyName);
        Assert.Equal(dto.AssemblyVersion, result.AssemblyVersion);
        Assert.Equal(dto.TypeName, result.TypeName);
    }

    [Fact]
    public void Test_ToRpc_PluginMetaInfo()
    {
        // Arrange
        var meta = new AyBorg.SDK.Common.PluginMetaInfo
        {
            Id = Guid.NewGuid(),
            AssemblyName = "Test_Assembly",
            AssemblyVersion = "123",
            TypeName = "Test_Type"
        };

        // Act
        PluginMetaDto result = _service.ToRpc(meta);

        // Assert
        Assert.Equal(meta.Id.ToString(), result.Id);
        Assert.Equal(meta.AssemblyName, result.AssemblyName);
        Assert.Equal(meta.AssemblyVersion, result.AssemblyVersion);
        Assert.Equal(meta.TypeName, result.TypeName);
    }

    [Fact]
    public void Test_FromRpc_LinkDto()
    {
        // Arrange
        var dto = new LinkDto
        {
            Id = Guid.NewGuid().ToString(),
            SourceId = Guid.NewGuid().ToString(),
            TargetId = Guid.NewGuid().ToString()
        };

        // Act
        Link result = _service.FromRpc(dto);

        // Assert
        Assert.Equal(dto.Id, result.Id.ToString());
        Assert.Equal(dto.SourceId, result.SourceId.ToString());
        Assert.Equal(dto.TargetId, result.TargetId.ToString());
    }

    [Fact]
    public void Test_ToRpc_Link()
    {
        // Arrange
        var link = new Link
        {
            Id = Guid.NewGuid(),
            SourceId = Guid.NewGuid(),
            TargetId = Guid.NewGuid()
        };

        // Act
        LinkDto result = _service.ToRpc(link);

        // Assert
        Assert.Equal(link.Id.ToString(), result.Id);
        Assert.Equal(link.SourceId.ToString(), result.SourceId);
        Assert.Equal(link.TargetId.ToString(), result.TargetId);
    }

    [Fact]
    public void Test_ToRpc_PortLink()
    {
        // Arrange
        var sourcePortMock = new Mock<IPort>();
        sourcePortMock.Setup(m => m.Id).Returns(Guid.NewGuid());
        var targetPortMock = new Mock<IPort>();
        targetPortMock.Setup(m => m.Id).Returns(Guid.NewGuid());
        var link = new PortLink(sourcePortMock.Object, targetPortMock.Object);

        // Act
        LinkDto result = _service.ToRpc(link);

        // Assert
        Assert.Equal(link.Id.ToString(), result.Id);
        Assert.Equal(link.SourceId.ToString(), result.SourceId);
        Assert.Equal(link.TargetId.ToString(), result.TargetId);
    }

    [Theory]
    [InlineData(PortBrand.Boolean, "true")]
    [InlineData(PortBrand.Boolean, "false")]
    [InlineData(PortBrand.Folder, "/test")]
    [InlineData(PortBrand.String, "test")]
    [InlineData(PortBrand.Numeric, "42")]
    [InlineData(PortBrand.Enum, "{\"name\":\"Output\",\"names\":[\"Input\",\"Output\"]}")]
    [InlineData(PortBrand.Image, "{\"OriginalImage\":null,\"Meta\":{\"Width\":2,\"Height\":4,\"PixelFormat\":5}}")]
    [InlineData(PortBrand.Rectangle, "{\"x\":2,\"y\":4,\"width\":10,\"height\":20}")]
    public void Test_FromRpc_PortDto(PortBrand portBrand, string value)
    {
        // Arrange
        var dto = new PortDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Test_Port",
            Direction = (int)PortDirection.Input,
            Brand = (int)portBrand,
            IsConnected = true,
            IsLinkConvertable = true,
            Value = value
        };

        // Act
        Port result = _service.FromRpc(dto);

        // Assert
        Assert.Equal(dto.Id, result.Id.ToString());
        Assert.Equal(dto.Name, result.Name);
        Assert.Equal((PortDirection)dto.Direction, result.Direction);
        Assert.Equal(portBrand, result.Brand);
        Assert.Equal(dto.IsConnected, result.IsConnected);
        Assert.Equal(dto.IsLinkConvertable, result.IsLinkConvertable);
        if (portBrand == PortBrand.Enum)
        {
            Assert.Equal("Output", ((AyBorg.SDK.Common.Models.Enum)result.Value!).Name);
            return;
        }

        if (portBrand == PortBrand.Image)
        {
            Assert.Equal(2, ((CacheImage)result.Value!).Meta.Width);
            Assert.Equal(4, ((CacheImage)result.Value!).Meta.Height);
            Assert.Equal(ImageTorque.PixelFormat.Rgb24Packed, ((CacheImage)result.Value!).Meta.PixelFormat);
            return;
        }

        if (portBrand == PortBrand.Rectangle)
        {
            Assert.Equal(2, ((Rectangle)result.Value!).X);
            Assert.Equal(4, ((Rectangle)result.Value!).Y);
            Assert.Equal(10, ((Rectangle)result.Value!).Width);
            Assert.Equal(20, ((Rectangle)result.Value!).Height);
            return;
        }

        Assert.Equal(value, result.Value!.ToString(), true);
    }

    [Theory]
    [InlineData(PortBrand.Boolean, true)]
    [InlineData(PortBrand.Boolean, false)]
    [InlineData(PortBrand.Folder, "/test")]
    [InlineData(PortBrand.String, "test")]
    [InlineData(PortBrand.Numeric, 42)]
    [InlineData(PortBrand.Enum, PortDirection.Output)]
    [InlineData(PortBrand.Image, null)]
    [InlineData(PortBrand.Rectangle, null)]
    public void Test_ToRpc_Port(PortBrand portBrand, object? value)
    {
        // Arrange
        if (portBrand == PortBrand.Image)
        {
            value = new CacheImage
            {
                Meta = new ImageMeta
                {
                    Width = 2,
                    Height = 4,
                    PixelFormat = ImageTorque.PixelFormat.Rgb24Packed
                }
            };
        }

        if (portBrand == PortBrand.Rectangle)
        {
            value = new Rectangle
            {
                X = 2,
                Y = 4,
                Width = 10,
                Height = 20
            };
        }

        var port = new Port
        {
            Id = Guid.NewGuid(),
            Name = "Test_Port",
            Direction = PortDirection.Input,
            Brand = portBrand,
            IsConnected = true,
            IsLinkConvertable = true,
            Value = value
        };

        // Act
        PortDto result = _service.ToRpc(port);

        // Assert
        Assert.Equal(port.Id.ToString(), result.Id);
        Assert.Equal(port.Name, result.Name);
        Assert.Equal((int)port.Direction, result.Direction);
        Assert.Equal((int)portBrand, result.Brand);
        Assert.Equal(port.IsConnected, result.IsConnected);
        Assert.Equal(port.IsLinkConvertable, result.IsLinkConvertable);
        if (portBrand == PortBrand.Enum)
        {
            Assert.Equal("{\"name\":\"Output\",\"names\":[\"Input\",\"Output\"]}", result.Value);
            return;
        }

        if (portBrand == PortBrand.Image)
        {
            Assert.Equal("{\"width\":2,\"height\":4,\"pixelFormat\":5}", result.Value);
            return;
        }

        if (portBrand == PortBrand.Rectangle)
        {
            Assert.Equal("{\"x\":2,\"y\":4,\"width\":10,\"height\":20}", result.Value);
            return;
        }

        if (value != null)
        {
            Assert.Equal(value.ToString(), result.Value);
        }
    }

    [Theory]
    [InlineData(0.4, "0.4")]
    [InlineData(0.4, "0,4")]
    [InlineData(1000.4, "1000.4")]
    [InlineData(1000.4, "1000,4")]
    public void Test_DoublePortFromRpc(double expectedValue, string value)
    {
        // Arrange
        var dto = new PortDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Test_Port",
            Direction = (int)PortDirection.Input,
            Brand = (int)PortBrand.Numeric,
            Value = value
        };

        // Act
        Port port = _service.FromRpc(dto);

        // Assert
        Assert.Equal(double.Round(expectedValue, 1), double.Round((double)port.Value!, 1));
    }

    [Theory]
    [InlineData("0.4", 0.4)]
    [InlineData("1000.4", 1000.4)]
    public void Test_DoublePortToRpc(string expectedValue, double value)
    {
        // Arrange
        var port = new Port
        {
            Id = Guid.NewGuid(),
            Name = "Test_Port",
            Direction = PortDirection.Input,
            Brand = PortBrand.Numeric,
            Value = value
        };

        // Act
        PortDto dto = _service.ToRpc(port);

        // Assert
        Assert.Equal(expectedValue, dto.Value);
    }
}
