using System;

namespace NeonDashTrail.connectors;

public sealed class LevelEventsConnectorService
{
    public static event Action<int> CheckpointReachedEvent;
    public static event Action ResetToCheckpointEvent;
    
    public static void RaiseCheckpointReachedEvent(int checkpointNumber) 
        => CheckpointReachedEvent?.Invoke(checkpointNumber);
    public static void RaiseResetToCheckpointEvent() => ResetToCheckpointEvent?.Invoke();
}