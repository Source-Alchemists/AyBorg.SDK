using Atomy.SDK.DAL;
using Atomy.SDK.ImageProcessing.Shapes;
using AutoMapper;

namespace Atomy.SDK.Mapper.Converter;

internal class RectangleToRecordConverter : IValueConverter<Rectangle, string>
{
    public string Convert(Rectangle sourceMember, ResolutionContext context)
    {
        var record = new RectangleRecord
        {
            X = sourceMember.X,
            Y = sourceMember.Y,
            Width = sourceMember.Width,
            Height = sourceMember.Height
        };
        return System.Text.Json.JsonSerializer.Serialize(record);
    }
}