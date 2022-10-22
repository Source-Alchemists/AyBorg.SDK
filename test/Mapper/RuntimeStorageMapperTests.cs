using Atomy.SDK.DAL;
using Atomy.SDK.ImageProcessing;
using Atomy.SDK.ImageProcessing.Buffers;
using Atomy.SDK.ImageProcessing.Pixels;
using Atomy.SDK.Mapper;
using Atomy.SDK.Ports;
using AutoMapper;
using Moq;
using Newtonsoft.Json;

namespace Atomy.SDK.Tests.Mapper;

public class RuntimeStorageMapperTests
{
    [Fact]
    public void MapStepProxyWithInvalidPort()
    {
        // Arrange
        var mapper = new RuntimeToStorageMapper();
        var mockedStepProxy = CreateStepProxyMock();

        var mockedPort1 = new Mock<IPort>();
        mockedPort1.Setup(x => x.Direction).Returns(PortDirection.Output);
        mockedPort1.Setup(x => x.Name).Returns("Port1");

        var ports = new List<IPort>
        {
            mockedPort1.Object
        }; // No specific port. This is unsupported!

        mockedStepProxy.Setup(x => x.Ports).Returns(ports);

        // Act / Assert
        Assert.Throws<AutoMapperMappingException>(() => mapper.Map(mockedStepProxy.Object));
    }

    [Fact]
    public void MapStepProxyWithMultiplePorts()
    {
        // Arrange
        var mapper = new RuntimeToStorageMapper();
        var mockedStepProxy = CreateStepProxyMock();

        var ports = new List<IPort>
        {
            new NumericPort("Port1", PortDirection.Output, 123, 100, 200),
            new StringPort("Port2", PortDirection.Input, "Test")
        };

        mockedStepProxy.Setup(x => x.Ports).Returns(ports);

        // Act
        var result = mapper.Map(mockedStepProxy.Object);

        // Assert
        Assert.Equal(100, result.X);
        Assert.Equal(200, result.Y);
        Assert.Equal(mockedStepProxy.Object.MetaInfo, result.MetaInfo);
        Assert.Equal(2, result.Ports.Count());
        Assert.Equal("Port1", result.Ports.First().Name);
        Assert.Equal(PortDirection.Output, result.Ports.First().Direction);
        Assert.Equal(123, int.Parse(result.Ports.First().Value));
        Assert.Equal("Port2", result.Ports.Last().Name);
        Assert.Equal(PortDirection.Input, result.Ports.Last().Direction);
        Assert.Equal("Test", result.Ports.Last().Value);
    }

    [Fact]
    public void MapStepProxyWithOneIntegerPort()
    {
        // Arrange
        var mapper = new RuntimeToStorageMapper();
        var mockedStepProxy = CreateStepProxyMock();

        var ports = new List<IPort>
        {
            new NumericPort("Port1", PortDirection.Output, 123, 100, 200)
        };

        mockedStepProxy.Setup(x => x.Ports).Returns(ports);

        // Act
        var result = mapper.Map(mockedStepProxy.Object);

        // Assert
        Assert.Equal(100, result.X);
        Assert.Equal(200, result.Y);
        Assert.Equal(mockedStepProxy.Object.MetaInfo, result.MetaInfo);
        Assert.Single(result.Ports);
        Assert.Equal("Port1", result.Ports.First().Name);
        Assert.Equal(PortDirection.Output, result.Ports.First().Direction);
        Assert.Equal(123, int.Parse(result.Ports.First().Value));
    }

    [Fact]
    public void MapStepProxyWithOneStringPort()
    {
        // Arrange
        var mapper = new RuntimeToStorageMapper();
        var mockedStepProxy = CreateStepProxyMock();

        var ports = new List<IPort>
        {
            new StringPort("Port1", PortDirection.Input, "Test")
        };

        mockedStepProxy.Setup(x => x.Ports).Returns(ports);

        // Act
        var result = mapper.Map(mockedStepProxy.Object);

        // Assert
        Assert.Equal(100, result.X);
        Assert.Equal(200, result.Y);
        Assert.Equal(mockedStepProxy.Object.MetaInfo, result.MetaInfo);
        Assert.Single(result.Ports);
        Assert.Equal("Port1", result.Ports.First().Name);
        Assert.Equal(PortDirection.Input, result.Ports.First().Direction);
        Assert.Equal("Test", result.Ports.First().Value);
    }

    [Fact]
    public void MapProjectWithLinkedStepProxy()
    {
        // Arrange
        var mapper = new RuntimeToStorageMapper();

        var mockedStepProxy1 = CreateStepProxyMock();
        var step1outputPort = new NumericPort("Output", PortDirection.Output, 123);
        mockedStepProxy1.Setup(x => x.Ports).Returns(new List<IPort> { step1outputPort });

        var mockedStepProxy2 = CreateStepProxyMock();
        var step2inputPort = new NumericPort("Input", PortDirection.Input, 0);
        mockedStepProxy2.Setup(x => x.Ports).Returns(new List<IPort> { step2inputPort });
        var link = new PortLink(step1outputPort, step2inputPort);

        step1outputPort.Connect(link);
        step2inputPort.Connect(link);
        mockedStepProxy1.Setup(x => x.Links).Returns(new List<PortLink> { link });
        mockedStepProxy2.Setup(x => x.Links).Returns(new List<PortLink> { link });

        var project = new Project
        {
            Steps = new List<IStepProxy> {
                mockedStepProxy1.Object,
                mockedStepProxy2.Object
            },
            Links = new List<PortLink> { link }
        };

        // Act
        var result = mapper.Map(project);

        // Assert
        Assert.Equal(2, result.Steps.Count());
        Assert.Single(result.Links);
        Assert.Equal(link.Id, result.Links.First().Id);
        Assert.Equal(link.SourceId, result.Links.First().SourceId);
        Assert.Equal(link.TargetId, result.Links.First().TargetId);
    }

    [Fact]
    public void MapProject()
    {
        // Arrange
        var mapper = new RuntimeToStorageMapper();
        var mockedStepProxy1 = CreateStepProxyMock("Step1", 1, 2);
        var mockedStepProxy2 = CreateStepProxyMock("Step2", 3, 4);
        var project = new Project
        {
            Meta = new ProjectMeta { Name = "Testproject" },
            Steps = new List<IStepProxy> { mockedStepProxy1.Object, mockedStepProxy2.Object }
        };

        // Act
        var result = mapper.Map(project);

        // Assert
        Assert.Equal("Testproject", result.Meta.Name);
        Assert.Equal(2, result.Steps.Count);
    }

    [Fact]
    public void TestMapStepWithFolderPort()
    {
        // Arrange
        var mapper = new RuntimeToStorageMapper();
        var folderPort = new FolderPort("FolderPort", PortDirection.Input, "/testFolder");
        var mockedStepProxy = CreateStepProxyMock();
        mockedStepProxy.Setup(x => x.Ports).Returns(new List<IPort> { folderPort });

        // Act
        var result = mapper.Map(mockedStepProxy.Object);

        // Assert
        Assert.Single(result.Ports);
        Assert.Equal("FolderPort", result.Ports.First().Name);
        Assert.Equal(PortDirection.Input, result.Ports.First().Direction);
        Assert.Equal("/testFolder", result.Ports.First().Value);
    }

    [Fact]
    public void TestMapStepWithEmptyImagePort()
    {
        // Arrange
        var mapper = new RuntimeToStorageMapper();
        var imagePort = new ImagePort("ImagePort", PortDirection.Input, null!);
        var mockedStepProxy = CreateStepProxyMock();
        mockedStepProxy.Setup(x => x.Ports).Returns(new List<IPort> { imagePort });

        // Act
        var result = mapper.Map(mockedStepProxy.Object);

        // Assert
        Assert.Single(result.Ports);
        Assert.Equal("ImagePort", result.Ports.First().Name);
        Assert.Equal(PortDirection.Input, result.Ports.First().Direction);
        Assert.Equal(string.Empty, result.Ports.First().Value);
    }

    [Fact]
    public void TestMapStepWithFilledImagePort()
    {
        // Arrange
        var mapper = new RuntimeToStorageMapper();
        var pixelBuffer = new PackedPixelBuffer<Mono8>(2, 2, new Mono8[] { 0x00, 0x01, 0x80, 0xFF });
        var image = new Image(pixelBuffer);
        using var imagePort = new ImagePort("ImagePort", PortDirection.Input, image);
        var mockedStepProxy = CreateStepProxyMock();
        mockedStepProxy.Setup(x => x.Ports).Returns(new List<IPort> { imagePort });

        // Act
        var result = mapper.Map(mockedStepProxy.Object);

        // Assert
        Assert.Single(result.Ports);
        Assert.Equal("ImagePort", result.Ports.First().Name);
        Assert.Equal(PortDirection.Input, result.Ports.First().Direction);
        var imageRecord = JsonConvert.DeserializeObject<ImageRecord>(result.Ports.First().Value);
        Assert.Equal(image.Width, imageRecord.Width);
        Assert.Equal(image.Height, imageRecord.Height);
        Assert.Equal(image.Size, imageRecord.Size);
        Assert.Equal(image.PixelFormat, imageRecord.PixelFormat);
        Assert.Equal(string.Empty, imageRecord.Value);
    }

    private static Mock<IStepProxy> CreateStepProxyMock(string typeName = "MockedStep", int x = 100, int y = 200)
    {
        var mockedStepProxy = new Mock<IStepProxy>();
        mockedStepProxy.Setup(x => x.MetaInfo).Returns(new PluginMetaInfo
        {
            TypeName = typeName,
            AssemblyName = string.Empty,
            AssemblyVersion = string.Empty
        });
        mockedStepProxy.Setup(x => x.X).Returns(x);
        mockedStepProxy.Setup(x => x.Y).Returns(y);
        return mockedStepProxy;
    }
}
