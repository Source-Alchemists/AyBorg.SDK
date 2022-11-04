using AutoMapper;
using Atomy.SDK.Data.DTOs;

namespace Atomy.SDK.Data.Mapper.Converter;

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