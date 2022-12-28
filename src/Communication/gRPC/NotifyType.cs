namespace AyBorg.SDK.Communication.gRPC;

public enum NotifyType {
    Unknown = -1,

    AgentEngineStateChanged = 0,
    AgentIterationFinished = 1,
    AgentAutomationFlowChanged = 2
}
