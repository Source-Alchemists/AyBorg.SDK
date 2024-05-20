using System.ComponentModel;

namespace AyBorg.SDK.Common;

public enum EventLogType
{
    // Registry
    [Description("Connect")]
    Connect = 42000,
    [Description("Disconnect")]
    Disconnect = 42001,
    [Description("Upload")]
    Upload = 42002,
    [Description("Download")]
    Download = 42003,

    // Common
    [Description("Result")]
    Result = 42010,
    [Description("User Interaction")]
    UserInteraction = 42020,

    // Engine
    [Description("Engine")]
    Engine = 42100,
    [Description("Plugin")]
    Plugin = 42110,
    [Description("Project State")]
    ProjectState = 42200,
    [Description("Project saved")]
    ProjectSaved = 42201,
    [Description("Project removed")]
    ProjectRemoved = 42202,

    // Audit
    [Description("Audit")]
    Audit = 42300,

    // Cognitive
    [Description("Cognitive")]
    Cognitive = 42400
}
