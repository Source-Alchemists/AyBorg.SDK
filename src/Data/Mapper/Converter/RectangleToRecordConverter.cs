using AutoMapper;
using AyBorg.SDK.Data.DAL;
using ImageTorque;
using Sys = System;

namespace AyBorg.SDK.Data.Mapper.Converter;

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
        return Sys.Text.Json.JsonSerializer.Serialize(record);
    }
}
