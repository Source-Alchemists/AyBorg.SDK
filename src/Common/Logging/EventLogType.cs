using System.ComponentModel;

namespace AyBorg.SDK.Common;

public enum EventLogType
{
    [Description("Undefined")]
    Undefined = 0,
    // gRPC
    [Description("gRPC.Internal.Call")]
    GrpcInternalCall = 3,
    [Description("gRPC.ServerCallHandler")]
    GrpcServerCallHandler = 7,
    // Registry
    [Description("Connect")]
    Connect = 42000,
    [Description("Disconnect")]
    Disconnect = 42001,
    // Engine
    [Description("Engine State")]
    EngineState = 42100,
    [Description("Plugin State")]
    PluginState = 42110
}
