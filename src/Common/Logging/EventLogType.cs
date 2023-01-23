using System.ComponentModel;

namespace AyBorg.SDK.Common;

public enum EventLogType
{
    // Registry
    [Description("Connect")]
    Connect = 42000,
    [Description("Disconnect")]
    Disconnect = 42001,
    // Common
    [Description("Result")]
    Result = 42010,
    // Engine
    [Description("Engine State")]
    EngineState = 42100,
    [Description("Plugin State")]
    PluginState = 42110,
    [Description("Project State")]
    ProjectState = 42200,
    [Description("Project saved")]
    ProjectSaved = 42201,
    [Description("Project removed")]
    ProjectRemoved = 42202
}
