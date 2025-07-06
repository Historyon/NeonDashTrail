using System;

namespace NeonDashTrail.connectors;

public sealed class LevelEventsConnectorService
{
    public static event Action<int> CheckpointReachedEvent;
    public static event Action ResetToCheckpointEvent;
    public static event Action GoalReachedEvent;
    
    public static void RaiseCheckpointReachedEvent(int checkpointNumber) 
        => CheckpointReachedEvent?.Invoke(checkpointNumber);
    public static void RaiseResetToCheckpointEvent() => ResetToCheckpointEvent?.Invoke();
    public static void RaiseGoalReachedEvent() => GoalReachedEvent?.Invoke();
}