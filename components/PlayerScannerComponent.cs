using NeonDashTrail.levels;

namespace NeonDashTrail.components;

public partial class PlayerScannerComponent : Area2D
{
    [Signal] public delegate void WallRunDirectionChangedEventHandler();

    public bool IsWallRunPossible { get; private set; }
    public Vector2 WallRunDirection { get; private set; }

    private TileMapLayer _environmentLayer { get; set; }


    public override void _Ready()
    {
        _environmentLayer = Searchers.FindParentOfType<LevelBase>(this).EnvironmentTiles;
    }


    public void ScanArea()
    {
        var cell = _environmentLayer.LocalToMap(GlobalPosition);
        var data = _environmentLayer.GetCellTileData(cell);

        if (data is null)
        {
            IsWallRunPossible = false;
            return;
        }

        IsWallRunPossible = data.HasCustomData(TileMapDataLayers.WallRunDirection);

        if (IsWallRunPossible) 
            ProceedWallRunData(data.GetCustomData(TileMapDataLayers.WallRunDirection).AsVector2());
    }

    private void ProceedWallRunData(Vector2 wallRunDirection)
    {
        if (WallRunDirection != wallRunDirection)
            EmitSignalWallRunDirectionChanged();

        WallRunDirection = wallRunDirection;
    }
}
