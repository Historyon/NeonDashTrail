namespace NeonDashTrail.scripts;

public static class Constants
{
    /// <summary>
    /// Represents the minimum X component value of the frontal collision normal.
    /// This constant is used to evaluate and filter collision normals to determine
    /// if a collision is considered frontal based on the X-axis component.
    /// </summary>
    public const float MinFrontalCollisionNormalX = -0.7f;

    /// <summary>
    /// Represents the maximum absolute Y component value of the frontal collision normal.
    /// This constant is used to assess and constrain the Y-axis component of collision normals,
    /// ensuring accurate determination of frontal collisions based on the Y-axis behavior.
    /// </summary>
    public const float MaxFrontalCollisionNormalYAbsolute = 0.3f;
}

public static class Groups
{
    public const string Checkpoints = "checkpoints";
}

public static class Controls
{
    public const string Jump = "jump";
    public const string Pause = "pause";
    public const string Reset = "reset";
}