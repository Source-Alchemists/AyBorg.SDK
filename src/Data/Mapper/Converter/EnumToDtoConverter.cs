using AutoMapper;
using AyBorg.SDK.Data.DTOs;

namespace AyBorg.SDK.Data.Mapper.Converter;

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