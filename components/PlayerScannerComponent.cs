using NeonDashTrail.entities.level_elements;

namespace NeonDashTrail.components;

public partial class PlayerScannerComponent : Area2D
{
    [Signal] public delegate void WallRunWallDetectedEventHandler(bool entered);

    private void OnAreaEntered(Area2D area)
    {
        if (area is WallRunWall) EmitSignalWallRunWallDetected(true);
    }

    private void OnAreaExited(Area2D area)
    {
        if (area is WallRunWall) EmitSignalWallRunWallDetected(false);
    }
}
