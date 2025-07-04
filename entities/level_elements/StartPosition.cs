namespace NeonDashTrail.entities.level_elements;

public partial class StartPosition : StaticBody2D
{
    [Export] public Marker2D RunnerPosition { get; set; }

    public int CheckpointNumber { get; set; }
    
    public void SetRunnerToPosition(Runner runner) => runner.GlobalPosition = RunnerPosition.GlobalPosition;
}