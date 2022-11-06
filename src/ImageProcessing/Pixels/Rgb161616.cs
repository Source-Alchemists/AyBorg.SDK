using System.Runtime.CompilerServices;

namespace Autodroid.SDK.ImageProcessing.Pixels;

public record struct Rgb161616 : IPlanarPixel<Rgb161616>
{
    public ushort Value;

    public Rgb161616(ushort value)
    {
        Value = value;
    }

    public PixelInfo PixelInfo
    {
        get
        {
            return new PixelInfo(PixelType.Rgb161616, 6, 1, 3, true);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ushort(Rgb161616 value)
    {
        return value.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Rgb161616(ushort value)
    {
        return new Rgb161616(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rgb888 ToRgb888()
    {
        return new Rgb888(Convert.ToByte(Value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RgbFFF ToRgbFFF()
    {
        return new RgbFFF(Convert.ToSingle(Value));
    }
}