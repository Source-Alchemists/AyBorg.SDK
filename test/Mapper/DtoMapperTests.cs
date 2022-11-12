using AyBorg.SDK.Data.DAL;
using AyBorg.SDK.Data.DTOs;
using AyBorg.SDK.ImageProcessing;
using AyBorg.SDK.Data.Mapper;
using AyBorg.SDK.Common;
using AyBorg.SDK.Common.Ports;
using Moq;

namespace AyBorg.SDK.Tests.Mapper;

#nullable disable

public class DtoMapperTests
{
    [Fact]
    public void MapEmptyProjectRecordToDto()
    {
        // Arrange
        var mapper = new DtoMapper();
        var projectRecord = new ProjectRecord
        {
            DbId = Guid.NewGuid(),
            Meta = new ProjectMetaRecord {
                DbId = Guid.NewGuid(),
                Name = "Test Project",
            },
            Steps = new List<StepRecord>()
            
        };

        // Act
        var dto = mapper.Map(projectRecord);

        // Assert
        Assert.Equal(projectRecord.Meta.DbId, dto.Meta.Id);
        Assert.Equal(projectRecord.Meta.Name, dto.Meta.Name);
        Assert.Equal(projectRecord.Meta.IsActive, dto.Meta.IsActive);
    }

    [Fact]
    public void MapProjectRecordWithStepsToDto()
    {
        // Arrange
        var mapper = new DtoMapper();
        var projectRecord = new ProjectRecord
        {
            DbId = Guid.NewGuid(),
            Meta = new ProjectMetaRecord {
                DbId = Guid.NewGuid(),
                Name = "Test Project",
            },
            Steps = new List<StepRecord> {
                new StepRecord {
                    Id = Guid.NewGuid(),
                    Name = "Test Step",
                    X = 100,
                    Y = 200,
                    MetaInfo = new PluginMetaInfo
                    {
                        Id = Guid.NewGuid(),
                        AssemblyName = "TestAssembly",
                        AssemblyVersion = "1.0.0.0",
                        TypeName = "TestType"
                    },
                    Ports = new List<PortRecord> {
                        new PortRecord {
                            Name = "Port1",
                            Direction = PortDirection.Output,
                            Value = "123"
                        },
                        new PortRecord {
                            Name = "Port2",
                            Direction = PortDirection.Input,
                            Value = "Test"
                        }
                    }
                }
            }
        };

        var expectedDto = new ProjectDto
        {
            Meta = new ProjectMetaDto {
                Id = projectRecord.Meta.DbId,
                Name = projectRecord.Meta.Name,
                IsActive = projectRecord.Meta.IsActive
            }
        };
        var stepDto = new StepDto
        {
            Id = projectRecord.Steps.First().Id,
            Name = projectRecord.Steps.First().Name,
            X = projectRecord.Steps.First().X,
            Y = projectRecord.Steps.First().Y,
            MetaInfo = projectRecord.Steps.First().MetaInfo,
            Ports = new List<PortDto>()
        };
        var portDto1 = new PortDto
        {
            Name = projectRecord.Steps.First().Ports.First().Name,
            Direction = projectRecord.Steps.First().Ports.First().Direction,
            Value = projectRecord.Steps.First().Ports.First().Value
        };
        var portDto2 = new PortDto
        {
            Name = projectRecord.Steps.First().Ports.Last().Name,
            Direction = projectRecord.Steps.First().Ports.Last().Direction,
            Value = projectRecord.Steps.First().Ports.Last().Value
        };
        var ports = new List<PortDto> {
            portDto1,
            portDto2
        };

        // Act
        var dto = mapper.Map(projectRecord);

        // Assert
        Assert.Equal(expectedDto.Meta.Id, dto.Meta.Id);
        Assert.Equal(expectedDto.Meta.Name, dto.Meta.Name);
        Assert.Equal(expectedDto.Meta.IsActive, dto.Meta.IsActive);
        Assert.Equal(stepDto.Id, dto.Steps.First().Id);
        Assert.Equal(stepDto.Name, dto.Steps.First().Name);
        Assert.Equal(stepDto.X, dto.Steps.First().X);
        Assert.Equal(stepDto.Y, dto.Steps.First().Y);
        Assert.Equal(stepDto.MetaInfo.Id, dto.Steps.First().MetaInfo.Id);
        Assert.Equal(stepDto.MetaInfo.AssemblyName, dto.Steps.First().MetaInfo.AssemblyName);
        Assert.Equal(stepDto.MetaInfo.AssemblyVersion, dto.Steps.First().MetaInfo.AssemblyVersion);
        Assert.Equal(stepDto.MetaInfo.TypeName, dto.Steps.First().MetaInfo.TypeName);
        Assert.Equal(ports.First().Name, dto.Steps.First().Ports.First().Name);
        Assert.Equal(ports.First().Direction, dto.Steps.First().Ports.First().Direction);
        Assert.Equal(ports.First().Value, dto.Steps.First().Ports.First().Value);
        Assert.Equal(ports.Last().Name, dto.Steps.First().Ports.Last().Name);
        Assert.Equal(ports.Last().Direction, dto.Steps.First().Ports.Last().Direction);
        Assert.Equal(ports.Last().Value, dto.Steps.First().Ports.Last().Value);
    }

    [Fact]
    public void MapPortStringRecordToDto()
    {
        // Arrange
        var mapper = new DtoMapper();
        var portRecord = new PortRecord
        {
            Name = "Test Port",
            Direction = PortDirection.Output,
            Brand = PortBrand.String,
            Value = "Test string"
        };
        var expectedDto = new PortDto
        {
            Name = portRecord.Name,
            Direction = portRecord.Direction,
            Brand = PortBrand.String,
            Value = portRecord.Value
        };

        // Act
        var dto = mapper.Map(portRecord);

        // Assert
        Assert.Equal(expectedDto.Name, dto.Name);
        Assert.Equal(expectedDto.Direction, dto.Direction);
        Assert.Equal(expectedDto.Brand, dto.Brand);
        Assert.Equal(expectedDto.Value, dto.Value);
    }

    [Fact]
    public void MapPortDoubleRecordToDto()
    {
        // Arrange
        var mapper = new DtoMapper();
        var portRecord = new PortRecord
        {
            Name = "Test Port",
            Direction = PortDirection.Output,
            Brand = PortBrand.Numeric,
            Value = "123.456"
        };
        var expectedDto = new PortDto
        {
            Name = portRecord.Name,
            Direction = portRecord.Direction,
            Brand = PortBrand.Numeric,
            Value = portRecord.Value
        };

        // Act
        var dto = mapper.Map(portRecord);

        // Assert
        Assert.Equal(expectedDto.Name, dto.Name);
        Assert.Equal(expectedDto.Direction, dto.Direction);
        Assert.Equal(expectedDto.Brand, dto.Brand);
        Assert.Equal(expectedDto.Value, dto.Value);
    }

    [Fact]
    public void MapStepProxyToDto()
    {
        // Arrange
        var mapper = new DtoMapper();
        var stepBodyMock = new Mock<IStepBody>();
        var stepProxyMock = new Mock<IStepProxy>();
        stepProxyMock.Setup(x => x.StepBody).Returns(stepBodyMock.Object);
        stepProxyMock.Setup(x => x.Id).Returns(Guid.NewGuid());
        stepProxyMock.Setup(x => x.MetaInfo).Returns(new PluginMetaInfo
        {
            Id = Guid.NewGuid(),
            AssemblyName = "TestAssembly",
            AssemblyVersion = "1.0.0.0",
            TypeName = "TestType"
        });

        // Act
        var dto = mapper.Map(stepProxyMock.Object);

        // Assert
        Assert.Equal(stepProxyMock.Object.Id, dto.Id);
        Assert.Equal(stepProxyMock.Object.Name, dto.Name);
        Assert.Equal(stepProxyMock.Object.X, dto.X);
        Assert.Equal(stepProxyMock.Object.Y, dto.Y);
        Assert.Equal(stepProxyMock.Object.MetaInfo.Id, dto.MetaInfo.Id);
        Assert.Equal(stepProxyMock.Object.MetaInfo.AssemblyName, dto.MetaInfo.AssemblyName);
        Assert.Equal(stepProxyMock.Object.MetaInfo.AssemblyVersion, dto.MetaInfo.AssemblyVersion);
        Assert.Equal(stepProxyMock.Object.MetaInfo.TypeName, dto.MetaInfo.TypeName);
        Assert.Equal(stepProxyMock.Object.Ports.Count(), dto.Ports.Count());
    }

    [Fact]
    public void TestMapEnumPortToDto()
    {
        // Arrange
        var mode = BinaryThresholdMode.MaxChroma;
        var names = BinaryThresholdMode.GetNames(typeof(BinaryThresholdMode));
        var mapper = new DtoMapper();
        var port = new EnumPort("Test Port", PortDirection.Input, mode);
        var expectedDto = new PortDto
        {
            Name = port.Name,
            Direction = port.Direction,
            Brand = PortBrand.Enum,
            Value = new EnumDto { Name = mode.ToString(), Names = names }
        };

        // Act
        var dto = mapper.Map(port);

        // Assert
        Assert.Equal(expectedDto.Name, dto.Name);
        Assert.Equal(expectedDto.Direction, dto.Direction);
        Assert.Equal(expectedDto.Brand, dto.Brand);
        Assert.Equal(expectedDto.Value, dto.Value);
    }
}