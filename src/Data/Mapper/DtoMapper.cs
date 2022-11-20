using AutoMapper;
using AyBorg.SDK.Data.DAL;
using AyBorg.SDK.Data.DTOs;
using AyBorg.SDK.Data.Mapper.Converter;
using AyBorg.SDK.Common.Ports;
using AyBorg.SDK.Common;

namespace AyBorg.SDK.Data.Mapper;

public sealed class DtoMapper : IDtoMapper
{
    /// <summary>
    /// Gets the mapper, use with caution.
    /// </summary>
    public AutoMapper.Mapper Mapper { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DtoMapper"/> class.
    /// </summary>
    public DtoMapper()
    {
        var config = new MapperConfiguration(config =>
        {
            // Records
            config.CreateMap<ProjectMetaRecord, ProjectMetaDto>().ReverseMap();
            config.CreateMap<ProjectRecord, ProjectDto>().ReverseMap();
            config.CreateMap<StepRecord, StepDto>();
            config.CreateMap<LinkRecord, LinkDto>();
            config.CreateMap<PortRecord, PortDto>();
            config.CreateMap<PluginMetaInfoRecord, PluginMetaInfo>().ReverseMap();

            // Runtimes
            config.CreateMap<IStepProxy, StepDto>();
            config.CreateMap<PortLink, LinkDto>();
            config.CreateMap<StringPort, PortDto>();
            config.CreateMap<FolderPort, PortDto>();
            config.CreateMap<BooleanPort, PortDto>();
            config.CreateMap<NumericPort, PortDto>();
            config.CreateMap<ImagePort, PortDto>().ForMember(d => d.Value, opt => opt.ConvertUsing(new ImageToDtoConverter()));
            config.CreateMap<RectanglePort, PortDto>().ForMember(d => d.Value, opt => opt.MapFrom(s => new RectangleDto { X = s.Value.X, Y = s.Value.Y, Width = s.Value.Width, Height = s.Value.Height }));
            config.CreateMap<EnumPort, PortDto>().ForMember(d => d.Value, opt => opt.ConvertUsing(new EnumToDtoConverter()));
        });

        Mapper = new AutoMapper.Mapper(config);
    }

    /// <summary>
    /// Maps the specified project meta record.
    /// </summary>
    /// <param name="projectMetaRecord">The project meta record.</param>
    /// <returns></returns>
    public ProjectMetaDto Map(ProjectMetaRecord projectMetaRecord) => Mapper.Map<ProjectMetaDto>(projectMetaRecord);

    /// <summary>
    /// Maps the specified project record.
    /// </summary>
    /// <param name="projectRecord">The project record.</param>
    /// <returns>Project dto.</returns>
    public ProjectDto Map(ProjectRecord projectRecord) => Mapper.Map<ProjectDto>(projectRecord);

    /// <summary>
    /// Maps the specified port to record.
    /// </summary>
    /// <param name="portRecord">The port record.</param>
    /// <returns>Port dto.</returns>
    public PortDto Map(PortRecord portRecord) => Mapper.Map<PortDto>(portRecord);

    /// <summary>
    /// Maps the specified project dto.
    /// </summary>
    /// <param name="projectDto">The project dto.</param>
    /// <returns></returns>
    public ProjectRecord Map(ProjectDto projectDto) => Mapper.Map<ProjectRecord>(projectDto);

    /// <summary>
    /// Maps the specified step body.
    /// </summary>
    /// <param name="step">The step.</param>
    /// <returns></returns>
    public StepDto Map(IStepProxy step) =>Mapper.Map<StepDto>(step);

    /// <summary>
    /// Maps the specified port.
    /// </summary>
    /// <param name="port">The port.</param>
    /// <returns></returns>
    public PortDto Map(IPort port) => Mapper.Map<PortDto>(port);

    /// <summary>
    /// Maps the specified link.
    /// </summary>
    /// <param name="link">The link.</param>
    /// <returns></returns>
    public LinkDto Map(PortLink link) => Mapper.Map<LinkDto>(link);
}
