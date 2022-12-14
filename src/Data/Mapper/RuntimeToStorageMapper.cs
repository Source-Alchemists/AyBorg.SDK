using System.Globalization;
using AutoMapper;
using AyBorg.SDK.Data.DAL;
using AyBorg.SDK.Common.Ports;
using AyBorg.SDK.Data.Mapper.Converter;
using AyBorg.SDK.Common;
using AyBorg.SDK.Projects;

namespace AyBorg.SDK.Data.Mapper;

public sealed class RuntimeToStorageMapper : IRuntimeToStorageMapper
{
    private readonly AutoMapper.Mapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="RuntimeToStorageMapper"/> class.
    /// </summary>
    public RuntimeToStorageMapper()
    {
        var config = new MapperConfiguration(config => {
            config.CreateMap<ProjectMeta, ProjectMetaRecord>().ReverseMap();
            config.CreateMap<PluginMetaInfo, PluginMetaInfoRecord>().ReverseMap();
            config.CreateMap<Project, ProjectRecord>();
            config.CreateMap<IStepProxy, StepRecord>();
            config.CreateMap<PortLink, LinkRecord>();

            // Ports
            config.CreateMap<NumericPort, PortRecord>().ForMember(d => d.Value, opt => opt.MapFrom(s => Convert.ToString(s.Value, CultureInfo.InvariantCulture)));
            config.CreateMap<StringPort, PortRecord>();
            config.CreateMap<FolderPort, PortRecord>();
            config.CreateMap<BooleanPort, PortRecord>();
            config.CreateMap<ImagePort, PortRecord>().ForMember(d => d.Value, opt => opt.ConvertUsing(new ImageToRecordConverter()));
            config.CreateMap<RectanglePort, PortRecord>().ForMember(d => d.Value, opt => opt.ConvertUsing(new RectangleToRecordConverter()));
            config.CreateMap<EnumPort, PortRecord>().ForMember(d => d.Value, opt => opt.ConvertUsing(new EnumToRecordConverter()));
        });

        _mapper = new AutoMapper.Mapper(config);
    }

    /// <summary>
    /// Maps the specified step proxy.
    /// </summary>
    /// <param name="stepProxy">The step proxy.</param>
    /// <returns></returns>
    public StepRecord Map(IStepProxy stepProxy)
    {
        return _mapper.Map<StepRecord>(stepProxy);
    }

    /// <summary>
    /// Maps the specified project.
    /// </summary>
    /// <param name="project">The project.</param>
    /// <returns></returns>
    public ProjectRecord Map(Project project)
    {
        return _mapper.Map<ProjectRecord>(project);
    }
}
