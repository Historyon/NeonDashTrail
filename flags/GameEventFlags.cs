using System;

namespace NeonDashTrail.flags;

[Flags]
public enum GameEventFlags
{
    None = 0,
    QuitGame = 1 << 0,
    StartGame = 1 << 1,
    PauseGame = 1 << 2,
    ResumeGame = 1 << 3,
    BackToMainMenu = 1 << 4
}