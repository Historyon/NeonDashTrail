using NeonDashTrail.scripts.enums;

namespace NeonDashTrail.entities.level_elements;

[Tool]
public partial class WallRunWall : Area2D
{
    [Export] public int LengthInTiles { get => _lengthInTiles; set => UpdateWall(value, HeightInTiles, TileSize); }
    [Export] public int HeightInTiles { get => _heightInTiles; set => UpdateWall(LengthInTiles, value, TileSize); }
    [Export] public Vector2I TileSize { get => _tileSize; set => UpdateWall(LengthInTiles, HeightInTiles, value); }
    [Export] private CollisionShape2D CollisionShape { get; set; }
    [Export] private Sprite2D Sprite { get; set; }
    [Export] public WallRunDirection Direction { get; set; } = WallRunDirection.Right;

    private int _lengthInTiles = 1;
    private int _heightInTiles = 1;
    private Vector2I _tileSize = new(16, 16);

    private void UpdateWall(int lengthInTiles, int heightInTiles, Vector2I tileSize)
    {
        if (Sprite is null || CollisionShape is null) return;

        _lengthInTiles = lengthInTiles;
        _heightInTiles = heightInTiles;
        _tileSize = tileSize;

        Sprite.RegionRect = new Rect2I(Vector2I.Zero, new(_tileSize.X * _lengthInTiles, _tileSize.Y * _heightInTiles));
        CollisionShape.Shape = new RectangleShape2D { Size = new Vector2(_tileSize.X * _lengthInTiles, _tileSize.Y * _heightInTiles) };
        CollisionShape.Position = new Vector2(_tileSize.X * _lengthInTiles / 2f - _tileSize.X / 2f , _tileSize.Y * _heightInTiles / 2f - _tileSize.Y / 2f); 
    }
}