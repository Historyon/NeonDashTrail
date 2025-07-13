using System.Linq;
using NeonDashTrail.entities;
using NeonDashTrail.entities.level_elements;
using NeonDashTrail.interfaces;

namespace NeonDashTrail.levels;

[GlobalClass]
public partial class LevelBase : Node2D, ILevel
{
    [Export] public Runner Runner { get; set; }
    
    private Checkpoint[] _startPositions = [];

    public override void _Ready()
    {
        _startPositions = CollectCheckpoints();
        SetCheckpointNumbers();
    }

    public void StartRunFromFirstStartPosition()
    {
        StartRunFromCheckpoint(0);
        Runner.StartRun();
    }
    
    public void StartRunFromCheckpoint(int checkpointNumber)
    {
        Runner.DashResetRequired = false;
        _startPositions[checkpointNumber].SetRunnerToPosition(Runner);
    }

    private Checkpoint[] CollectCheckpoints()
    {
        return GetTree()
            .GetNodesInGroup(Groups.Checkpoints)
            .OfType<Checkpoint>()
            .OrderBy(node => node.GlobalPosition.X)
            .ToArray();
    }

    /// <summary>
    /// Sets the checkpoint numbers ordered by found direction to X-Axis
    /// </summary>
    private void SetCheckpointNumbers()
    {
        for (var i = 0; i < _startPositions.Length; i++)
        {
            _startPositions[i].CheckpointNumber = i;
        }
    }
}