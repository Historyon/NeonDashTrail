using System;

namespace NeonDashTrail.flags;


[Flags]
public enum LevelEventFlags
{
    None = 0,
    CheckpointReached = 1 << 0,
    ResetToCheckpoint = 1 << 1,
    GoalReached = 1 << 2
}