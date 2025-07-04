using NeonDashTrail.flags;

namespace NeonDashTrail.connectors;

public partial class LevelEventsConnector : Node
{
    [Export] public LevelEventFlags ConnectToEvents { get; set; }
    
    [Signal] public delegate void CheckpointReachedEventHandler(int checkpointNumber);
    [Signal] public delegate void ResetToCheckpointEventHandler();

    public override void _Ready()
    {
        if (ConnectToEvents.HasFlag(LevelEventFlags.CheckpointReached))
            LevelEventsConnectorService.CheckpointReachedEvent += EmitSignalCheckpointReached;
        if (ConnectToEvents.HasFlag(LevelEventFlags.ResetToCheckpoint))
            LevelEventsConnectorService.ResetToCheckpointEvent += EmitSignalResetToCheckpoint;
    }

    public override void _ExitTree()
    {
        if (ConnectToEvents.HasFlag(LevelEventFlags.CheckpointReached))
            LevelEventsConnectorService.CheckpointReachedEvent -= EmitSignalCheckpointReached;
        if (ConnectToEvents.HasFlag(LevelEventFlags.ResetToCheckpoint))
            LevelEventsConnectorService.ResetToCheckpointEvent -= EmitSignalResetToCheckpoint;
    }
}