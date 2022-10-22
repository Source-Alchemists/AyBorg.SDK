namespace Atomy.SDK.Runtime;

public enum EngineState 
{
    /// <summary>
    /// The engine is idle.
    /// </summary>
    Idle = 0,
    /// <summary>
    /// The engine is starting.
    /// </summary>
    Starting = 1,
    /// <summary>
    /// The engine is running.
    /// </summary>
    Running = 2,
    /// <summary>
    /// The engine is stopping.
    /// </summary>
    Stopping = 3,
    /// <summary>
    /// The engine is stopped.
    /// </summary>
    Stopped = 4,
    /// <summary>
    /// The engine is aborting.
    Aborting = 5,
    /// <summary>
    /// The engine is aborted.
    /// </summary>
    Aborted = 6,
    /// <summary>
    /// The engined finished.
    /// </summary>
    Finished = 7
}