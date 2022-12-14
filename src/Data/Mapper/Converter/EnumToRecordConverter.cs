using Sys = System;
using AutoMapper;
using AyBorg.SDK.Data.DAL;

namespace AyBorg.SDK.Data.Mapper.Converter;

internal class EnumToRecordConverter : IValueConverter<Enum, string>
{
    public string Convert(Enum sourceMember, ResolutionContext context)
    {
        var names = Enum.GetNames(sourceMember.GetType());
        var record = new EnumRecord
        {
            Name = sourceMember.ToString(),
            Names = names
        };

        return Sys.Text.Json.JsonSerializer.Serialize(record);
    }
}