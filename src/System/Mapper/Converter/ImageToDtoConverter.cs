using Microsoft.IO;
using Microsoft.Toolkit.HighPerformance;
using AutoMapper;
using Atomy.SDK.DTOs;
using Atomy.SDK.ImageProcessing;
using Atomy.SDK.ImageProcessing.Encoding;

namespace Atomy.SDK.Mapper.Converter;

internal class ImageToDtoConverter : IValueConverter<Image, ImageDto>
{
    private static readonly RecyclableMemoryStreamManager _memoryManager = new RecyclableMemoryStreamManager();
    public ImageDto Convert(Image sourceMember, ResolutionContext context)
    {
        const int maxSize = 250;
        if (sourceMember == null)
        {
            return new ImageDto();
        }
        Image targetImage = null!;
        Image resizedImage = null!;
        if (sourceMember.Width <= maxSize && sourceMember.Height <= maxSize)
        {
            targetImage = sourceMember;
        }
        else
        {
            Image.CalculateClampSize(sourceMember, maxSize, out var w, out var h);
            resizedImage = sourceMember.Resize(w, h, ResizeMode.NearestNeighbor);
            targetImage = resizedImage;
        }

        var encoderType = EncoderType.Png;
        using var stream = _memoryManager.GetStream();
        Image.Save(targetImage, stream, encoderType);
        stream.Position = 0;
        var rmStream = (RecyclableMemoryStream)stream;
        var buffer = rmStream.GetBuffer();
        var dto = new ImageDto
        {
            Base64 = System.Convert.ToBase64String(buffer, 0, (int)stream.Length),
            Meta = new ImageMetaDto
            {
                Width = sourceMember.Width,
                Height = sourceMember.Height,
                PixelFormat = sourceMember.PixelFormat,
                EncoderType = encoderType
            }
        };
        resizedImage?.Dispose();
        return dto;
    }
}