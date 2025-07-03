using NeonDashTrail.manager;

namespace NeonDashTrail.scenes;

public partial class Game : Node2D
{
    [Export] public MenuManager MenuManager { get; set; }

    public override void _Ready()
    {
        MenuManager.ShowMainMenu();
    }
    
    private void OnQuitGame()
    {
        GetTree().Quit();
    }
    
    private void OnPauseGame() => GetTree().Paused = true;
    
    private void OnResumeGame() => GetTree().Paused = false;
    
    private void OnBackToMainMenu() => GetTree().Paused = false;
}