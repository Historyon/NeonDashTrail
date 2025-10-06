using NeonDashTrail.entities.level_elements;

namespace NeonDashTrail.components;

public partial class PlayerScannerComponent : Area2D
{
    [Signal] public delegate void WallRunWallDetectedEventHandler(WallRunWall wallRunWall);
    [Signal] public delegate void WallRunWallLeaveEventHandler(WallRunWall wallRunWall);

    private void OnAreaEntered(Area2D area)
    {
        if (area is WallRunWall wallRunWall) EmitSignalWallRunWallDetected(wallRunWall);
    }

    private void OnAreaExited(Area2D area)
    {
        if (area is WallRunWall wallRunWall) EmitSignalWallRunWallLeave(wallRunWall);
    }
}
