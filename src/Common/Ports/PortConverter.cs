using System.Text.Json;
using Atomy.SDK.ImageProcessing.Shapes;

namespace Atomy.SDK.Common.Ports;

public static class PortConverter
{
    /// <summary>
    /// Determines whether the source port value is convertible to the target port value.
    /// </summary>
    /// <param name="sourcePort">The source port.</param>
    /// <param name="targetPort">The target port.</param>
    /// <returns></returns>
    public static bool IsConvertable(IPort sourcePort, IPort targetPort)
    {
        if (sourcePort.Brand == targetPort.Brand)
        {
            return true;
        }
        switch (sourcePort.Brand)
        {
            case PortBrand.String:
                return targetPort.Brand == PortBrand.Numeric
                        || targetPort.Brand == PortBrand.Boolean
                        || targetPort.Brand == PortBrand.Rectangle
                        || targetPort.Brand == PortBrand.Enum;
            case PortBrand.Folder:
                return targetPort.Brand == PortBrand.String;
            case PortBrand.Numeric:
                return targetPort.Brand == PortBrand.String
                        || targetPort.Brand == PortBrand.Boolean
                        || targetPort.Brand == PortBrand.Enum;
            case PortBrand.Boolean:
                return targetPort.Brand == PortBrand.String || targetPort.Brand == PortBrand.Numeric;
            case PortBrand.Rectangle:
                return targetPort.Brand == PortBrand.String;
            case PortBrand.Enum:
                return targetPort.Brand == PortBrand.String || targetPort.Brand == PortBrand.Numeric;
            default:
                return false;
        }
    }

    /// <summary>
    /// Connvert the port value to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to convert to.</typeparam>	
    /// <param name="sourcePort">The port to convert.</param>
    /// <param name="orgtValue">The original value.</param>
    /// <returns>The converted value.</returns>
    public static T Convert<T>(IPort sourcePort, object orgValue)
    {
        try
        {
            switch (sourcePort.Brand)
            {
                case PortBrand.String:
                    if (typeof(T) == typeof(Rectangle))
                    {
                        return (T)System.Convert.ChangeType(JsonSerializer.Deserialize<Rectangle>(((StringPort)sourcePort).Value), typeof(T));
                    }
                    if (typeof(T) == typeof(Enum))
                    {
                        var enumType = orgValue.GetType();
                        return (T)System.Convert.ChangeType(Enum.Parse(enumType, ((StringPort)sourcePort).Value), typeof(T));
                    }
                    if(typeof(T) == typeof(Double) && Double.TryParse(((StringPort)sourcePort).Value, out var numericValue))
                    {
                        return (T)System.Convert.ChangeType(numericValue, typeof(T));
                    }
                    return (T)System.Convert.ChangeType(((StringPort)sourcePort).Value, typeof(T));                    
                case PortBrand.Folder:
                    return (T)System.Convert.ChangeType(((FolderPort)sourcePort).Value, typeof(T));
                case PortBrand.Numeric:
                    if (typeof(T) == typeof(bool))
                    {
                        return (T)System.Convert.ChangeType(((NumericPort)sourcePort).Value > 0, typeof(T));
                    }
                    if (typeof(T) == typeof(Enum))
                    {
                        var intValue = System.Convert.ToInt32((((NumericPort)sourcePort).Value));
                        var enumType = orgValue.GetType();
                        return (T)System.Convert.ChangeType(Enum.Parse(enumType, Enum.GetNames(enumType).ElementAt(intValue)), typeof(T));
                    }
                    return (T)System.Convert.ChangeType(((NumericPort)sourcePort).Value, typeof(T));
                case PortBrand.Boolean:
                    return (T)System.Convert.ChangeType(((BooleanPort)sourcePort).Value, typeof(T));
                case PortBrand.Rectangle:
                    return (T)System.Convert.ChangeType(JsonSerializer.Serialize(((RectanglePort)sourcePort).Value), typeof(T));
                case PortBrand.Enum:
                    return (T)System.Convert.ChangeType(((EnumPort)sourcePort).Value, typeof(T));
                default:
                    throw new ArgumentException("The port brand is not supported.");
            }
        }
        catch (Exception ex)
        {
            throw new ArgumentException("The port value cannot be converted to the specified type.", ex);
        }
    }
}