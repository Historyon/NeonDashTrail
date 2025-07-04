

using NeonDashTrail.levels;

namespace NeonDashTrail.manager;

public partial class LevelManager : Node
{
    [Export] public PackedScene TestLevel { get; set; }
    [Export] public Node2D ParentToConnectLevel { get; set; }

    private LevelBase _activeLevel;
    
    private void OnStartGame()
    {
        RemoveActiveLevel();
        
        _activeLevel = TestLevel.Instantiate() as LevelBase;
        ParentToConnectLevel.AddChild(_activeLevel);
        _activeLevel!.Show();
        
        _activeLevel.StartRunFromFirstStartPosition();
    }

    private void OnBackToMainMenu() => RemoveActiveLevel();

    private void RemoveActiveLevel()
    {
        if (_activeLevel is null) return;
        
        ParentToConnectLevel.RemoveChild(_activeLevel);
        _activeLevel.Hide();
        _activeLevel.QueueFree();
        _activeLevel = null;
    }
}