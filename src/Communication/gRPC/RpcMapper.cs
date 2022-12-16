using System.Runtime.CompilerServices;
using System.Text.Json;
using AyBorg.SDK.Common;
using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Communication.gRPC;

public static class RpcMapper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Data.Bindings.Step FromRpc(Ayborg.Gateway.V1.Step rpc)
    {
        var convertedPorts = new List<Data.Bindings.Port>();
        foreach (Ayborg.Gateway.V1.Port? port in rpc.Ports)
        {
            convertedPorts.Add(FromRpc(port));
        }

        var step = new Data.Bindings.Step
        {
            Id = Guid.Parse(rpc.Id),
            Name = rpc.Name,
            X = rpc.X,
            Y = rpc.Y,
            ExecutionTimeMs = rpc.ExecutionTimeMs,
            MetaInfo = FromRpc(rpc.MetaInfo),
            Ports = convertedPorts
        };

        return step;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ayborg.Gateway.V1.Step ToRpc(Data.Bindings.Step step)
    {
        var convertedPorts = new List<Ayborg.Gateway.V1.Port>();
        foreach(Data.Bindings.Port port in step.Ports!)
        {
            convertedPorts.Add(ToRpc(port));
        }
        var rpc = new Ayborg.Gateway.V1.Step
        {
            Id = step.Id.ToString(),
            Name = step.Name,
            X = step.X,
            Y = step.Y,
            ExecutionTimeMs = step.ExecutionTimeMs,
            MetaInfo = ToRpc(step.MetaInfo),
        };

        rpc.Ports.Add(convertedPorts);

        return rpc;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PluginMetaInfo FromRpc(Ayborg.Gateway.V1.PluginMetaInfo rpc)
    {
        var pluginMetaInfo = new PluginMetaInfo
        {
            Id = Guid.Parse(rpc.Id),
            AssemblyName = rpc.AssemblyName,
            AssemblyVersion = rpc.AssemblyVersion,
            TypeName = rpc.TypeName
        };

        return pluginMetaInfo;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ayborg.Gateway.V1.PluginMetaInfo ToRpc(PluginMetaInfo pluginMetaInfo)
    {
        return new Ayborg.Gateway.V1.PluginMetaInfo
        {
            Id = pluginMetaInfo.Id.ToString(),
            AssemblyName = pluginMetaInfo.AssemblyName,
            AssemblyVersion = pluginMetaInfo.AssemblyVersion,
            TypeName = pluginMetaInfo.TypeName
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Data.Bindings.Port FromRpc(Ayborg.Gateway.V1.Port rpc)
    {
        var port = new Data.Bindings.Port
        {
            Id = Guid.Parse(rpc.Id),
            Name = rpc.Name,
            Direction = (PortDirection)rpc.Direction,
            Brand = (PortBrand)rpc.Brand,
            IsConnected = rpc.IsConnected,
            IsLinkConvertable = rpc.IsLinkConvertable,
            Value = UnpackPortValue(rpc)
        };

        return port;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ayborg.Gateway.V1.Port ToRpc(Data.Bindings.Port port)
    {
        return new Ayborg.Gateway.V1.Port
        {
            Id = port.Id.ToString(),
            Name = port.Name,
            Direction = (int)port.Direction,
            Brand = (int)port.Brand,
            IsConnected = port.IsConnected,
            IsLinkConvertable = port.IsLinkConvertable,
            Value = PackPortValue(port)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ayborg.Gateway.V1.Link ToRpc(Data.Bindings.Link link)
    {
        return new Ayborg.Gateway.V1.Link
        {
            Id = link.Id.ToString(),
            SourceId = link.SourceId.ToString(),
            TargetId = link.TargetId.ToString()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ayborg.Gateway.V1.Link ToRpc(PortLink link)
    {
        return new Ayborg.Gateway.V1.Link
        {
            Id = link.Id.ToString(),
            SourceId = link.SourceId.ToString(),
            TargetId = link.TargetId.ToString()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Data.Bindings.Link FromRpc(Ayborg.Gateway.V1.Link rpc)
    {
        return new Data.Bindings.Link
        {
            Id = Guid.Parse(rpc.Id),
            SourceId = Guid.Parse(rpc.SourceId),
            TargetId = Guid.Parse(rpc.TargetId)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static object UnpackPortValue(Ayborg.Gateway.V1.Port rpc)
    {
        return (PortBrand)rpc.Brand switch
        {
            PortBrand.String or PortBrand.Folder => rpc.Value,
            PortBrand.Boolean => bool.Parse(rpc.Value),
            PortBrand.Numeric => double.Parse(rpc.Value),
            PortBrand.Enum => JsonSerializer.Deserialize<Data.Bindings.Enum>(rpc.Value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!,
            PortBrand.Rectangle => JsonSerializer.Deserialize<Data.Bindings.Rectangle>(rpc.Value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!,
            PortBrand.Image => JsonSerializer.Deserialize<Data.Bindings.ImageMeta>(rpc.Value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!,
            _ => throw new ArgumentOutOfRangeException(nameof(rpc.Brand), rpc.Brand, null),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string PackPortValue(Data.Bindings.Port port)
    {
        return port.Brand switch
        {
            PortBrand.String or PortBrand.Folder => port.Value!.ToString()!,
            PortBrand.Boolean => port.Value!.ToString()!,
            PortBrand.Numeric => port.Value!.ToString()!,
            PortBrand.Enum => ConvertEnum((Enum)port.Value!),
            PortBrand.Rectangle => JsonSerializer.Serialize(port.Value, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
            PortBrand.Image => JsonSerializer.Serialize(port.Value, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
            _ => throw new ArgumentOutOfRangeException(nameof(port.Brand), port.Brand, null),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string ConvertEnum(Enum inEnum)
    {
        var resEnum = new Data.Bindings.Enum
        {
            Name = inEnum.ToString(),
            Names = Enum.GetNames(inEnum.GetType())
        };

        return JsonSerializer.Serialize(resEnum, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}
