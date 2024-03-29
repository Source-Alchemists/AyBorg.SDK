using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Ayborg.Gateway.Agent.V1;
using AyBorg.SDK.Common;
using AyBorg.SDK.Common.Models;
using AyBorg.SDK.Common.Ports;
using Sys = System;

namespace AyBorg.SDK.Communication.gRPC;

public class RpcMapper : IRpcMapper
{
    private static readonly JsonSerializerOptions s_jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

     public PluginMetaInfo FromRpc(PluginMetaDto rpc)
    {
        return new PluginMetaInfo
        {
            Id = Guid.Parse(rpc.Id),
            AssemblyName = rpc.AssemblyName,
            AssemblyVersion = rpc.AssemblyVersion,
            TypeName = rpc.TypeName
        };
    }

    public Step FromRpc(StepDto rpc)
    {
        var convertedPorts = new List<Port>();
        foreach (PortDto? port in rpc.Ports)
        {
            convertedPorts.Add(FromRpc(port));
        }

        var step = new Step
        {
            Id = Guid.Parse(rpc.Id),
            Name = rpc.Name,
            Categories = rpc.Categories,
            X = rpc.X,
            Y = rpc.Y,
            ExecutionTimeMs = rpc.ExecutionTimeMs,
            MetaInfo = FromRpc(rpc.MetaInfo),
            Ports = convertedPorts
        };

        return step;
    }

    public Port FromRpc(PortDto rpc)
    {
        return new Port
        {
            Id = Guid.Parse(rpc.Id),
            Name = rpc.Name,
            Direction = (PortDirection)rpc.Direction,
            Brand = (PortBrand)rpc.Brand,
            IsConnected = rpc.IsConnected,
            IsLinkConvertable = rpc.IsLinkConvertable,
            Value = UnpackPortValue(rpc)
        };
    }

    public Link FromRpc(LinkDto rpc)
    {
        return new Link
        {
            Id = Guid.Parse(rpc.Id),
            SourceId = Guid.Parse(rpc.SourceId),
            TargetId = Guid.Parse(rpc.TargetId)
        };
    }

    public PluginMetaDto ToRpc(PluginMetaInfo pluginMetaInfo)
    {
        return new PluginMetaDto
        {
            Id = pluginMetaInfo.Id.ToString(),
            AssemblyName = pluginMetaInfo.AssemblyName,
            AssemblyVersion = pluginMetaInfo.AssemblyVersion,
            TypeName = pluginMetaInfo.TypeName
        };
    }

    public StepDto ToRpc(Step step)
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

        rpc.Categories.AddRange(step.Categories);
        rpc.Ports.Add(convertedPorts);

        return rpc;
    }

    public PortDto ToRpc(Port port)
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

    public LinkDto ToRpc(Link link)
    {
        return new LinkDto
        {
            Id = link.Id.ToString(),
            SourceId = link.SourceId.ToString(),
            TargetId = link.TargetId.ToString()
        };
    }

    public LinkDto ToRpc(PortLink link)
    {
        return new LinkDto
        {
            Id = link.Id.ToString(),
            SourceId = link.SourceId.ToString(),
            TargetId = link.TargetId.ToString()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static object UnpackPortValue(PortDto rpc)
    {
        return (PortBrand)rpc.Brand switch
        {
            PortBrand.String or PortBrand.Folder => rpc.Value,
            PortBrand.Boolean => bool.Parse(rpc.Value),
            PortBrand.Numeric => double.Parse(rpc.Value.Replace(',', '.'), CultureInfo.InvariantCulture),
            PortBrand.Enum => JsonSerializer.Deserialize<Common.Models.Enum>(rpc.Value, s_jsonSerializerOptions)!,
            PortBrand.Select => JsonSerializer.Deserialize<SelectPort.ValueContainer>(rpc.Value, s_jsonSerializerOptions)!,
            PortBrand.Rectangle => JsonSerializer.Deserialize<Rectangle>(rpc.Value, s_jsonSerializerOptions)!,
            PortBrand.Image => JsonSerializer.Deserialize<CacheImage>(rpc.Value, s_jsonSerializerOptions)!,
            // Collections
            PortBrand.StringCollection => JsonSerializer.Deserialize<string[]>(rpc.Value, s_jsonSerializerOptions)?.ToImmutableList() ?? ImmutableList<string>.Empty,
            PortBrand.NumericCollection => JsonSerializer.Deserialize<double[]>(rpc.Value, s_jsonSerializerOptions)?.ToImmutableList() ?? ImmutableList<double>.Empty,
            PortBrand.RectangleCollection => JsonSerializer.Deserialize<Rectangle[]>(rpc.Value, s_jsonSerializerOptions)?.ToImmutableList() ?? ImmutableList<Rectangle>.Empty,
            _ => throw new ArgumentOutOfRangeException(nameof(rpc.Brand), rpc.Brand, null),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string PackPortValue(Port port)
    {
        return port.Brand switch
        {
            PortBrand.String or PortBrand.Folder => port.Value!.ToString()!,
            PortBrand.Boolean => port.Value!.ToString()!,
            PortBrand.Numeric => Convert.ToString(port.Value, CultureInfo.InvariantCulture)!,
            PortBrand.Enum => ConvertEnum(port.Value!),
            PortBrand.Select => JsonSerializer.Serialize(port.Value, s_jsonSerializerOptions),
            PortBrand.Rectangle => JsonSerializer.Serialize(port.Value, s_jsonSerializerOptions),
            PortBrand.Image => ConvertImage(port.Value!),
            // Collections
            PortBrand.StringCollection => ConvertCollection<string>(port.Value!),
            PortBrand.NumericCollection => ConvertCollection<double>(port.Value!),
            PortBrand.RectangleCollection => ConvertCollection<Rectangle>(port.Value!),
            _ => throw new ArgumentOutOfRangeException(nameof(port.Brand), port.Brand, null),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string ConvertCollection<T>(object obj)
    {
        string result;
        if (obj is IEnumerable<T> collection)
        {
            result = JsonSerializer.Serialize(collection, s_jsonSerializerOptions);
        }
        else
        {
            result = obj.ToString()!;
        }

        // The JsonSerializer is providing us with a invalid json, we have to fix it.
        if (result.Equals("[\"\"]"))
        {
            result = result.Replace("\"\"", string.Empty);
        }
        return result;
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

        return JsonSerializer.Serialize(resEnum, s_jsonSerializerOptions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string ConvertImage(object image)
    {
        ImageMeta imageMeta = ((CacheImage)image).Meta;

        return JsonSerializer.Serialize(imageMeta, s_jsonSerializerOptions);
    }
}
