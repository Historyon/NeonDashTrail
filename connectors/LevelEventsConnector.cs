using NeonDashTrail.flags;

namespace NeonDashTrail.connectors;

public partial class LevelEventsConnector : Node
{
    [Export] public LevelEventFlags ConnectToEvents { get; set; }
    
    [Signal] public delegate void CheckpointReachedEventHandler(int checkpointNumber);
    [Signal] public delegate void ResetToCheckpointEventHandler();
    [Signal] public delegate void GoalReachedEventHandler();

    public override void _Ready()
    {
        if (ConnectToEvents.HasFlag(LevelEventFlags.CheckpointReached))
            LevelEventsConnectorService.CheckpointReachedEvent += EmitSignalCheckpointReached;
        if (ConnectToEvents.HasFlag(LevelEventFlags.ResetToCheckpoint))
            LevelEventsConnectorService.ResetToCheckpointEvent += EmitSignalResetToCheckpoint;
        if (ConnectToEvents.HasFlag(LevelEventFlags.GoalReached))
            LevelEventsConnectorService.GoalReachedEvent += EmitSignalGoalReached;
    }

    public override void _ExitTree()
    {
        if (ConnectToEvents.HasFlag(LevelEventFlags.CheckpointReached))
            LevelEventsConnectorService.CheckpointReachedEvent -= EmitSignalCheckpointReached;
        if (ConnectToEvents.HasFlag(LevelEventFlags.ResetToCheckpoint))
            LevelEventsConnectorService.ResetToCheckpointEvent -= EmitSignalResetToCheckpoint;
        if (ConnectToEvents.HasFlag(LevelEventFlags.GoalReached))
            LevelEventsConnectorService.GoalReachedEvent -= EmitSignalGoalReached;
    }
}