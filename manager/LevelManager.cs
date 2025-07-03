

using NeonDashTrail.levels;

namespace NeonDashTrail.manager;

public partial class LevelManager : Node
{
    [Export] public PackedScene TestLevel { get; set; }
    [Export] public Node2D ParentToConnectLevel { get; set; }

    private Node2D _activeLevel;
    
    private void OnStartGame()
    {
        RemoveActiveLevel();
        
        _activeLevel = TestLevel.Instantiate<TestLevel>();
        ParentToConnectLevel.AddChild(_activeLevel);
        _activeLevel.Show();
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