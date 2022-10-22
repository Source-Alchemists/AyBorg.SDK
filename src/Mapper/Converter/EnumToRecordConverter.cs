using AutoMapper;
using Atomy.SDK.DAL;

namespace Atomy.SDK.Mapper.Converter;

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

        return System.Text.Json.JsonSerializer.Serialize(record);
    }
}