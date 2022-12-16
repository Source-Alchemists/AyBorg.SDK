using System.Runtime.CompilerServices;
using AyBorg.SDK.Common;
using AyBorg.SDK.Common.Ports;
using AyBorg.SDK.Data.Bindings;

namespace AyBorg.SDK.System.Agent;

public static class RuntimeMapper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Step FromRuntime(IStepProxy stepProxy)
    {
        var ports = new List<Port>();
        foreach (IPort port in stepProxy.Ports)
        {
            ports.Add(FromRuntime(port));
        }

        return new Step
        {
            Id = stepProxy.Id,
            Name = stepProxy.Name,
            X = stepProxy.X,
            Y = stepProxy.Y,
            ExecutionTimeMs = stepProxy.ExecutionTimeMs,
            MetaInfo = stepProxy.MetaInfo,
            Ports = ports
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Port FromRuntime(IPort runtimePort)
    {
        var port = new Port
        {
            Id = runtimePort.Id,
            Name = runtimePort.Name,
            Direction = runtimePort.Direction,
            Brand = runtimePort.Brand,
            IsConnected = runtimePort.IsConnected,
        };

        switch (runtimePort.Brand)
        {
            case PortBrand.String:
            case PortBrand.Folder:
                var stringPort = (StringPort)runtimePort;
                port.IsLinkConvertable = stringPort.IsLinkConvertable;
                port.Value = stringPort.Value;
                break;
            case PortBrand.Numeric:
                var numericPort = (NumericPort)runtimePort;
                port.IsLinkConvertable = numericPort.IsLinkConvertable;
                port.Value = numericPort.Value;
                break;
            case PortBrand.Boolean:
                var booleanPort = (BooleanPort)runtimePort;
                port.IsLinkConvertable = booleanPort.IsLinkConvertable;
                port.Value = booleanPort.Value;
                break;
            case PortBrand.Enum:
                var enumPort = (EnumPort)runtimePort;
                port.IsLinkConvertable = enumPort.IsLinkConvertable;
                port.Value = enumPort.Value;
                break;
            case PortBrand.Rectangle:
                var rectanglePort = (RectanglePort)runtimePort;
                port.IsLinkConvertable = rectanglePort.IsLinkConvertable;
                port.Value = rectanglePort.Value;
                break;
            case PortBrand.Image:
                var imagePort = (ImagePort)runtimePort;
                port.IsLinkConvertable = imagePort.IsLinkConvertable;
                port.Value = imagePort.Value != null ? new ImageMeta
                {
                    Width = imagePort.Value.Width,
                    Height = imagePort.Value.Height,
                    PixelFormat = imagePort.Value.PixelFormat
                } : new ImageMeta();
                break;
        }

        return port;
    }
}
