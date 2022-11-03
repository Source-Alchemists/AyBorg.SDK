using AutoMapper;
using Atomy.SDK.DTOs;

namespace Atomy.SDK.Mapper.Converter;

internal class EnumToDtoConverter : IValueConverter<Enum, EnumDto>
{
    public EnumDto Convert(Enum sourceMember, ResolutionContext context)
    {
        var names = Enum.GetNames(sourceMember.GetType());
        var dto = new EnumDto
        {
            Name = sourceMember.ToString(),
            Names = names
        };
        return dto;
    }
}