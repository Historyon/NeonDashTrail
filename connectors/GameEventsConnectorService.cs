using System;

namespace NeonDashTrail.connectors;

public sealed class GameEventsConnectorService
{
    public static event Action QuitGameEvent;
    public static event Action StartGameEvent;
    public static event Action PauseGameEvent;
    public static event Action ResumeGameEvent;
    public static event Action BackToMainMenuEvent;
    
    public static void RaiseQuitGameEvent() => QuitGameEvent?.Invoke();
    public static void RaiseStartGameEvent() => StartGameEvent?.Invoke();
    public static void RaisePauseGameEvent() => PauseGameEvent?.Invoke();
    public static void RaiseResumeGameEvent() => ResumeGameEvent?.Invoke();
    public static void RaiseBackToMainMenuEvent() => BackToMainMenuEvent?.Invoke();
}