namespace NeonDashTrail.entities.level_elements;

[Tool]
public partial class WallRunWall : Area2D
{
    [Export] public int LengthInTiles { get => _lengthInTiles; set => UpdateWall(value, TileSize); }
    [Export] public Vector2I TileSize { get => _tileSize; set => UpdateWall(LengthInTiles, value); }
    [Export] public bool IsLeftWall { get; set; } = true;
    [Export] private CollisionShape2D CollisionShape { get; set; }
    [Export] private Sprite2D Sprite { get; set; }

    private int _lengthInTiles = 1;
    private Vector2I _tileSize = new(16, 16);

    private void UpdateWall(int lengthInTiles, Vector2I tileSize)
    {
        if (Sprite is null || CollisionShape is null) return;

        _lengthInTiles = lengthInTiles;
        _tileSize = tileSize;

        Sprite.RegionRect = new Rect2I(Vector2I.Zero, new(_tileSize.X * _lengthInTiles, _tileSize.Y));
        CollisionShape.Shape = new RectangleShape2D { Size = new Vector2(_tileSize.X * _lengthInTiles, _tileSize.Y) };
    }
}