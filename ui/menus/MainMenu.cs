using NeonDashTrail.connectors;

namespace NeonDashTrail.ui.menus;

public partial class MainMenu : Control
{
    private void OnStartButtonPressed()
    {
        GameEventsConnectorService.RaiseStartGameEvent();
    }

    private void OnQuitGameButtonPressed()
    {
        GameEventsConnectorService.RaiseQuitGameEvent();
    }
}