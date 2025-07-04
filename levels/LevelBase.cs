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
    
    private IReadOnlyList<StartPosition> _startPositions = new List<StartPosition>();

    public override void _Ready()
    {
        _startPositions = CollectStartPositions();
    }

    public void StartRunFromFirstStartPosition()
    {
        _startPositions[0].SetRunnerToPosition(Runner);
        Runner.StartRun();
    }

    private IReadOnlyList<StartPosition> CollectStartPositions()
    {
        return GetTree()
            .GetNodesInGroup(Groups.StartPositions)
            .OfType<StartPosition>()
            .ToList();
    }
}