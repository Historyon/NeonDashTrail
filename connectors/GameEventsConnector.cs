using NeonDashTrail.flags;

namespace NeonDashTrail.connectors;

public partial class GameEventsConnector : Node
{
    [Export] public GameEventFlags ConnectToEvents { get; set; }
    
    [Signal] public delegate void QuitGameEventHandler();
    [Signal] public delegate void StartGameEventHandler();
    [Signal] public delegate void PauseGameEventHandler();
    [Signal] public delegate void ResumeGameEventHandler();
    [Signal] public delegate void BackToMainMenuEventHandler();

    public override void _Ready()
    {
        if (ConnectToEvents.HasFlag(GameEventFlags.QuitGame))
            GameEventsConnectorService.QuitGameEvent += EmitSignalQuitGame;
        if (ConnectToEvents.HasFlag(GameEventFlags.StartGame))
            GameEventsConnectorService.StartGameEvent += EmitSignalStartGame;
        if (ConnectToEvents.HasFlag(GameEventFlags.PauseGame))
            GameEventsConnectorService.PauseGameEvent += EmitSignalPauseGame;
        if (ConnectToEvents.HasFlag(GameEventFlags.ResumeGame))
            GameEventsConnectorService.ResumeGameEvent += EmitSignalResumeGame;
        if (ConnectToEvents.HasFlag(GameEventFlags.BackToMainMenu))
            GameEventsConnectorService.BackToMainMenuEvent += EmitSignalBackToMainMenu;
    }

    public override void _ExitTree()
    {
        if (ConnectToEvents.HasFlag(GameEventFlags.QuitGame))
            GameEventsConnectorService.QuitGameEvent -= EmitSignalQuitGame;
        if (ConnectToEvents.HasFlag(GameEventFlags.StartGame))
            GameEventsConnectorService.StartGameEvent -= EmitSignalStartGame;
        if (ConnectToEvents.HasFlag(GameEventFlags.PauseGame))
            GameEventsConnectorService.PauseGameEvent -= EmitSignalPauseGame;
        if (ConnectToEvents.HasFlag(GameEventFlags.ResumeGame))
            GameEventsConnectorService.ResumeGameEvent -= EmitSignalResumeGame;
        if (ConnectToEvents.HasFlag(GameEventFlags.BackToMainMenu))
            GameEventsConnectorService.BackToMainMenuEvent -= EmitSignalBackToMainMenu;
    }
}