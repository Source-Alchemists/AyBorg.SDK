using Sys = System;
using Atomy.SDK.Data.DAL;
using Atomy.SDK.ImageProcessing;
using Atomy.SDK.ImageProcessing.Buffers;
using Atomy.SDK.ImageProcessing.Pixels;
using AutoMapper;

namespace Atomy.SDK.Data.Mapper.Converter;

internal class ImageToRecordConverter : IValueConverter<Image, string>
{
    public string Convert(Image sourceMember, ResolutionContext context)
    {
        if(sourceMember == null)
        {
            return string.Empty;
        }

        var record = new ImageRecord
        {
            Width = sourceMember.Width,
            Height = sourceMember.Height,
            Size = sourceMember.Size,
            PixelFormat = sourceMember.PixelFormat,
            Value = string.Empty // No need to save images to the database as port.
        };
        
        return Sys.Text.Json.JsonSerializer.Serialize(record);
    }
}