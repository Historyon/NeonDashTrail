namespace NeonDashTrail.states.runner_states.args;

public class RunnerJumpingStateArgs(float jumpingForce) : StateTransitionArgs
{
    public float JumpingForce { get; set; } = jumpingForce;
}