using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using ImageTorque;

namespace AyBorg.SDK.Common.Ports;

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
        return sourcePort.Brand switch
        {
            PortBrand.String => targetPort.Brand == PortBrand.Numeric
                                    || targetPort.Brand == PortBrand.Boolean
                                    || targetPort.Brand == PortBrand.Rectangle
                                    || targetPort.Brand == PortBrand.Enum
                                    || targetPort.Brand == PortBrand.StringCollection,
            PortBrand.Folder => targetPort.Brand == PortBrand.String,
            PortBrand.Numeric => targetPort.Brand == PortBrand.String
                                    || targetPort.Brand == PortBrand.Boolean
                                    || targetPort.Brand == PortBrand.Enum,
            PortBrand.Boolean => targetPort.Brand == PortBrand.String || targetPort.Brand == PortBrand.Numeric,
            PortBrand.Rectangle => targetPort.Brand == PortBrand.String,
            PortBrand.Enum => targetPort.Brand == PortBrand.String || targetPort.Brand == PortBrand.Numeric,
            // Collections
            PortBrand.StringCollection => targetPort.Brand == PortBrand.String
                                    || targetPort.Brand == PortBrand.Numeric
                                    || targetPort.Brand == PortBrand.Boolean,
            PortBrand.NumericCollection => targetPort.Brand == PortBrand.String
                                    || targetPort.Brand == PortBrand.Numeric
                                    || targetPort.Brand == PortBrand.Boolean
                                    || targetPort.Brand == PortBrand.StringCollection,
            PortBrand.RectangleCollection => targetPort.Brand == PortBrand.Rectangle
                                    || targetPort.Brand == PortBrand.String
                                    || targetPort.Brand == PortBrand.Numeric
                                    || targetPort.Brand == PortBrand.Boolean,
            _ => false,
        };
    }

    /// <summary>
    /// Connvert the port value to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to convert to.</typeparam>
    /// <param name="sourcePort">The port to convert.</param>
    /// <param name="targetPreviousValue">The original value.</param>
    /// <returns>The converted value.</returns>
    public static T Convert<T>(IPort sourcePort, object targetPreviousValue)
    {
        try
        {
            return sourcePort.Brand switch
            {
                PortBrand.String => ConvertStringPort<T>(sourcePort, targetPreviousValue),
                PortBrand.Folder => (T)System.Convert.ChangeType(((FolderPort)sourcePort).Value, typeof(T)),
                PortBrand.Numeric => ConvertNumericPort<T>(sourcePort, targetPreviousValue),
                PortBrand.Boolean => (T)System.Convert.ChangeType(((BooleanPort)sourcePort).Value, typeof(T)),
                PortBrand.Rectangle => (T)System.Convert.ChangeType(JsonSerializer.Serialize(((RectanglePort)sourcePort).Value), typeof(T)),
                PortBrand.Enum => (T)System.Convert.ChangeType(((EnumPort)sourcePort).Value, typeof(T)),
                // Collections
                PortBrand.StringCollection => ConvertStringCollectionPort<T>(sourcePort),
                PortBrand.NumericCollection => ConvertNumericCollectionPort<T>(sourcePort),
                PortBrand.RectangleCollection => ConvertRectangleCollectionPort<T>(sourcePort),
                // Unsupported
                _ => throw new ArgumentException("The port brand is not supported."),
            };
        }
        catch (Exception ex)
        {
            throw new ArgumentException("The port value cannot be converted to the specified type.", ex);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static T ConvertNumericPort<T>(IPort sourcePort, object orgValue)
    {
        Type targetType = typeof(T);
        if (targetType == typeof(bool))
        {
            return (T)System.Convert.ChangeType(((NumericPort)sourcePort).Value > 0, targetType);
        }

        if (targetType == typeof(Enum))
        {
            int intValue = System.Convert.ToInt32(((NumericPort)sourcePort).Value);
            Type enumType = orgValue.GetType();
            return (T)System.Convert.ChangeType(Enum.Parse(enumType, Enum.GetNames(enumType)[intValue]), targetType);
        }
        return (T)System.Convert.ChangeType(((NumericPort)sourcePort).Value, targetType);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static T ConvertStringPort<T>(IPort sourcePort, object orgValue)
    {
        Type targetType = typeof(T);
        if (targetType == typeof(Rectangle))
        {
            return (T)System.Convert.ChangeType(JsonSerializer.Deserialize<Rectangle>(((StringPort)sourcePort).Value), targetType);
        }

        if (targetType == typeof(Enum))
        {
            Type enumType = orgValue.GetType();
            return (T)System.Convert.ChangeType(Enum.Parse(enumType, ((StringPort)sourcePort).Value), targetType);
        }

        if (targetType == typeof(double) && double.TryParse(((StringPort)sourcePort).Value, out double numericValue))
        {
            return (T)System.Convert.ChangeType(numericValue, targetType);
        }

        if (targetType == typeof(ImmutableList<string>))
        {
            var collection = new List<string> { ((StringPort)sourcePort).Value };
            return (T)System.Convert.ChangeType(collection.ToImmutableList(), targetType);
        }

        return (T)System.Convert.ChangeType(((StringPort)sourcePort).Value, targetType);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static T ConvertStringCollectionPort<T>(IPort sourcePort)
    {
        Type targetType = typeof(T);
        if (targetType == typeof(double))
        {
            return (T)System.Convert.ChangeType(((StringCollectionPort)sourcePort).Value.Count, targetType);
        }
        else if (targetType == typeof(bool))
        {
            return (T)System.Convert.ChangeType(((StringCollectionPort)sourcePort).Value.Any(), targetType);
        }

        return (T)System.Convert.ChangeType(JsonSerializer.Serialize(((StringCollectionPort)sourcePort).Value), targetType);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static T ConvertNumericCollectionPort<T>(IPort sourcePort)
    {
        Type targetType = typeof(T);
        if (targetType == typeof(double))
        {
            return (T)System.Convert.ChangeType(((NumericCollectionPort)sourcePort).Value.Count, targetType);
        }
        else if (targetType == typeof(bool))
        {
            return (T)System.Convert.ChangeType(((NumericCollectionPort)sourcePort).Value.Any(), targetType);
        }

        if (targetType == typeof(ImmutableList<string>))
        {
            var list = new List<string>();
            foreach (double value in ((NumericCollectionPort)sourcePort).Value)
            {
                list.Add(System.Convert.ToString(value, CultureInfo.InstalledUICulture));
            }
            return (T)System.Convert.ChangeType(list.ToImmutableList(), targetType);
        }

        return (T)System.Convert.ChangeType(JsonSerializer.Serialize(((NumericCollectionPort)sourcePort).Value), targetType);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static T ConvertRectangleCollectionPort<T>(IPort sourcePort)
    {
        Type targetType = typeof(T);
        if (targetType == typeof(Rectangle))
        {
            return (T)System.Convert.ChangeType(((RectangleCollectionPort)sourcePort).Value.FirstOrDefault(), targetType);
        }

        if (targetType == typeof(double))
        {
            return (T)System.Convert.ChangeType(((RectangleCollectionPort)sourcePort).Value.Count, targetType);
        }

        if (targetType == typeof(bool))
        {
            return (T)System.Convert.ChangeType(((RectangleCollectionPort)sourcePort).Value.Any(), targetType);
        }

        return (T)System.Convert.ChangeType(JsonSerializer.Serialize(((RectangleCollectionPort)sourcePort).Value), targetType);
    }
}
