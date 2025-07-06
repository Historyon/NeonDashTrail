

using NeonDashTrail.connectors;
using NeonDashTrail.levels;

namespace NeonDashTrail.manager;

public partial class LevelManager : Node
{
    [Export] public PackedScene TestLevel { get; set; }
    [Export] public Node2D ParentToConnectLevel { get; set; }
    [Export] public AudioStreamPlayer GoalReachedAudio { get; set; }

    private LevelBase _activeLevel;
    private int _reachedCheckpointNumber;
    
    private void OnStartGame()
    {
        RemoveActiveLevel();
        
        _activeLevel = TestLevel.Instantiate() as LevelBase;
        ParentToConnectLevel.AddChild(_activeLevel);
        _activeLevel!.Show();

        _reachedCheckpointNumber = 0;
        _activeLevel.StartRunFromFirstStartPosition();
    }
    
    private void RemoveActiveLevel()
    {
        if (_activeLevel is null) return;
        
        ParentToConnectLevel.RemoveChild(_activeLevel);
        _activeLevel.Hide();
        _activeLevel.QueueFree();
        _activeLevel = null;
    }

    private void OnBackToMainMenu() => RemoveActiveLevel();
    
    private void OnCheckpointReached(int checkpointNumber) => _reachedCheckpointNumber = checkpointNumber;
    
    private void OnResetToCheckpoint() => _activeLevel.StartRunFromCheckpoint(_reachedCheckpointNumber);

    private void OnGoalReached()
    {
        GoalReachedAudio.Play();
        GameEventsConnectorService.RaiseBackToMainMenuEvent();
    }
}