using System.Runtime.CompilerServices;
using System.Text.Json;
using Ayborg.Gateway.Agent.V1;
using AyBorg.SDK.Common;
using AyBorg.SDK.Common.Models;
using AyBorg.SDK.Common.Ports;
using Sys = System;

namespace AyBorg.SDK.Communication.gRPC;

public static class RpcMapper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Step FromRpc(StepDto rpc)
    {
        var convertedPorts = new List<Common.Models.Port>();
        foreach (PortDto? port in rpc.Ports)
        {
            convertedPorts.Add(FromRpc(port));
        }

        var step = new Common.Models.Step
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
    public static StepDto ToRpc(Step step)
    {
        var convertedPorts = new List<PortDto>();
        foreach (Port port in step.Ports!)
        {
            convertedPorts.Add(ToRpc(port));
        }
        var rpc = new StepDto
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
    public static PluginMetaInfo FromRpc(PluginMetaDto rpc)
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
    public static PluginMetaDto ToRpc(PluginMetaInfo pluginMetaInfo)
    {
        return new PluginMetaDto
        {
            Id = pluginMetaInfo.Id.ToString(),
            AssemblyName = pluginMetaInfo.AssemblyName,
            AssemblyVersion = pluginMetaInfo.AssemblyVersion,
            TypeName = pluginMetaInfo.TypeName
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Port FromRpc(PortDto rpc)
    {
        var port = new Port
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
    public static PortDto ToRpc(Port port)
    {
        return new PortDto
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
    public static LinkDto ToRpc(Link link)
    {
        return new LinkDto
        {
            Id = link.Id.ToString(),
            SourceId = link.SourceId.ToString(),
            TargetId = link.TargetId.ToString()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinkDto ToRpc(PortLink link)
    {
        return new LinkDto
        {
            Id = link.Id.ToString(),
            SourceId = link.SourceId.ToString(),
            TargetId = link.TargetId.ToString()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Link FromRpc(LinkDto rpc)
    {
        return new Link
        {
            Id = Guid.Parse(rpc.Id),
            SourceId = Guid.Parse(rpc.SourceId),
            TargetId = Guid.Parse(rpc.TargetId)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static object UnpackPortValue(PortDto rpc)
    {
        return (PortBrand)rpc.Brand switch
        {
            PortBrand.String or PortBrand.Folder => rpc.Value,
            PortBrand.Boolean => bool.Parse(rpc.Value),
            PortBrand.Numeric => double.Parse(rpc.Value),
            PortBrand.Enum => JsonSerializer.Deserialize<Common.Models.Enum>(rpc.Value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!,
            PortBrand.Rectangle => JsonSerializer.Deserialize<Rectangle>(rpc.Value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!,
            PortBrand.Image => JsonSerializer.Deserialize<ImageMeta>(rpc.Value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!,
            _ => throw new ArgumentOutOfRangeException(nameof(rpc.Brand), rpc.Brand, null),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string PackPortValue(Common.Models.Port port)
    {
        return port.Brand switch
        {
            PortBrand.String or PortBrand.Folder => port.Value!.ToString()!,
            PortBrand.Boolean => port.Value!.ToString()!,
            PortBrand.Numeric => port.Value!.ToString()!,
            PortBrand.Enum => ConvertEnum(port.Value!),
            PortBrand.Rectangle => JsonSerializer.Serialize(port.Value, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
            PortBrand.Image => JsonSerializer.Serialize(port.Value, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
            _ => throw new ArgumentOutOfRangeException(nameof(port.Brand), port.Brand, null),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string ConvertEnum(object inEnum)
    {
        Common.Models.Enum resEnum;
        if (inEnum is Sys.Enum enumObject)
        {
            resEnum = new Common.Models.Enum
            {
                Name = enumObject.ToString(),
                Names = Sys.Enum.GetNames(enumObject.GetType())
            };
        }
        else
        {
            resEnum = (Common.Models.Enum)inEnum;
        }

        return JsonSerializer.Serialize(resEnum, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}
