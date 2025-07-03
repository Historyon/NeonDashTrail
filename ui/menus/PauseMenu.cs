using NeonDashTrail.connectors;

namespace NeonDashTrail.ui.menus;

public partial class PauseMenu : Control
{
    private void OnResumeButtonPressed()
    {
        GameEventsConnectorService.RaiseResumeGameEvent();
    }
    
    private void OnMainMenuButtonPressed()
    {
        GameEventsConnectorService.RaiseBackToMainMenuEvent();
    }
}