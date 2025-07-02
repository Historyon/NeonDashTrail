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
}