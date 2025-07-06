using System.Collections.Generic;
using System.Linq;
using NeonDashTrail.entities;
using NeonDashTrail.entities.level_elements;
using NeonDashTrail.interfaces;

namespace NeonDashTrail.levels;

[GlobalClass]
public partial class LevelBase : Node2D, ILevel
{
    [Export] public Runner Runner { get; set; }
    
    private IReadOnlyList<Checkpoint> _startPositions = new List<Checkpoint>();

    public override void _Ready()
    {
        _startPositions = CollectCheckpoints();
        SetCheckpointNumbers();
    }

    public void StartRunFromFirstStartPosition()
    {
        _startPositions[0].SetRunnerToPosition(Runner);
        Runner.StartRun();
    }
    
    public void StartRunFromCheckpoint(int checkpointNumber)
    {
        _startPositions[checkpointNumber].SetRunnerToPosition(Runner);
    }

    private IReadOnlyList<Checkpoint> CollectCheckpoints()
    {
        return GetTree()
            .GetNodesInGroup(Groups.Checkpoints)
            .OfType<Checkpoint>()
            .OrderBy(node => node.GlobalPosition.X)
            .ToList();
    }

    /// <summary>
    /// Sets the checkpoint numbers ordered by found direction to X-Axis
    /// </summary>
    private void SetCheckpointNumbers()
    {
        for (var i = 0; i < _startPositions.Count; i++)
        {
            _startPositions[i].CheckpointNumber = i;
        }
    }
}