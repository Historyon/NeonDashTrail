using NeonDashTrail.ui.menus;

namespace NeonDashTrail.manager;

public partial class MenuManager : Node
{
    [Export] public PackedScene MainMenuScene { get; set; }
    [Export] public PackedScene InGameMenuScene { get; set; }
    [Export] public CanvasLayer MenuLayer { get; set; }

    private Control _mainMenu;
    private Control _pauseMenu;

    private bool _showMainMenu;

    public override void _Ready()
    {
        _mainMenu = MainMenuScene.Instantiate<MainMenu>();
        _mainMenu.Hide();
        MenuLayer.AddChild(_mainMenu);
    }

    public void ShowMainMenu()
    {
        if (_showMainMenu) return;

        _showMainMenu = true;
        _pauseMenu?.Hide();
        _mainMenu.Show();
    }

    public void HideMainMenu()
    {
        if (!_showMainMenu) return;
        
        _showMainMenu = false;
        _mainMenu.Hide();
    }

    private void OnBackToMainMenu() => ShowMainMenu();
    
    private void OnStartGame() => HideMainMenu();
}