using NeonDashTrail.ui.menus;

namespace NeonDashTrail.manager;

public partial class MenuManager : Node
{
    [Export] public PackedScene MainMenuScene { get; set; }
    [Export] public PackedScene InGameMenuScene { get; set; }
    [Export] public CanvasLayer MenuLayer { get; set; }

    private Control _mainMenu;
    private Control _pauseMenu;

    public override void _Ready()
    {
        _mainMenu = MainMenuScene.Instantiate<MainMenu>();
        _mainMenu.Hide();
        MenuLayer.AddChild(_mainMenu);

        _pauseMenu = InGameMenuScene.Instantiate<PauseMenu>();
        _pauseMenu.Hide();
        MenuLayer.AddChild(_pauseMenu);
    }

    public void ShowMainMenu()
    {
        _pauseMenu?.Hide();
        _mainMenu.Show();
    }

    private void OnBackToMainMenu() => ShowMainMenu();
    
    private void OnStartGame() => _mainMenu.Hide();

    private void OnPauseGame() => _pauseMenu.Show();
    
    private void OnResumeGame() => _pauseMenu.Hide();
}