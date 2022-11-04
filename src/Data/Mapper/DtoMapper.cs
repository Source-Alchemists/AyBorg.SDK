using AutoMapper;
using Atomy.SDK.Data.DAL;
using Atomy.SDK.Data.DTOs;
using Atomy.SDK.Data.Mapper.Converter;
using Atomy.SDK.Common.Ports;
using Atomy.SDK.Common;

namespace Atomy.SDK.Data.Mapper;

public class DtoMapper : IDtoMapper
{
    private readonly AutoMapper.Mapper _mapper;

    /// <summary>
    /// Gets the mapper, use with caution.
    /// </summary>
    public AutoMapper.Mapper Mapper => _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="DtoMapper"/> class.
    /// </summary>
    public DtoMapper()
    {
        var config = new MapperConfiguration(config =>
        {
            // Records
            config.CreateMap<ProjectMetaRecord, ProjectMetaDto>().ForMember(d => d.Id, opt => opt.MapFrom(s => s.DbId)).ReverseMap();
            config.CreateMap<ProjectRecord, ProjectDto>().ReverseMap();
            config.CreateMap<StepRecord, StepDto>();
            config.CreateMap<LinkRecord, LinkDto>();
            config.CreateMap<PortRecord, PortDto>();

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

        _mapper = new AutoMapper.Mapper(config);
    }

    /// <summary>
    /// Maps the specified project meta record.
    /// </summary>
    /// <param name="projectMetaRecord">The project meta record.</param>
    /// <returns></returns>
    public ProjectMetaDto Map(ProjectMetaRecord projectMetaRecord) => _mapper.Map<ProjectMetaDto>(projectMetaRecord);
    
    /// <summary>
    /// Maps the specified project record.
    /// </summary>
    /// <param name="projectRecord">The project record.</param>
    /// <returns>Project dto.</returns>
    public ProjectDto Map(ProjectRecord projectRecord) => _mapper.Map<ProjectDto>(projectRecord);

    /// <summary>
    /// Maps the specified port to record.
    /// </summary>
    /// <param name="portRecord">The port record.</param>
    /// <returns>Port dto.</returns>
    public PortDto Map(PortRecord portRecord) => _mapper.Map<PortDto>(portRecord);

    /// <summary>
    /// Maps the specified project dto.
    /// </summary>
    /// <param name="projectDto">The project dto.</param>
    /// <returns></returns>
    public ProjectRecord Map(ProjectDto projectDto) => _mapper.Map<ProjectRecord>(projectDto);

    /// <summary>
    /// Maps the specified step body.
    /// </summary>
    /// <param name="step">The step.</param>
    /// <returns></returns>
    public StepDto Map(IStepProxy step) =>_mapper.Map<StepDto>(step);

    /// <summary>
    /// Maps the specified port.
    /// </summary>
    /// <param name="port">The port.</param>
    /// <returns></returns>
    public PortDto Map(IPort port) => _mapper.Map<PortDto>(port);

    /// <summary>
    /// Maps the specified link.
    /// </summary>
    /// <param name="link">The link.</param>
    /// <returns></returns>
    public LinkDto Map(PortLink link) => _mapper.Map<LinkDto>(link);
}
