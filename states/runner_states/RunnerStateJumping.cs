using NeonDashTrail.states.runner_states.args;

namespace NeonDashTrail.states.runner_states;

[GlobalClass]
public partial class RunnerStateJumping : RunnerStateBase
{
    public override RunnerState State => RunnerState.Jumping;
    public float JumpForce { get; set; }

    public override void Enter(StateTransitionArgs transitionArgs = null)
    {
        JumpForce = transitionArgs is RunnerJumpingStateArgs runnerJumpingStateArgs 
            ? runnerJumpingStateArgs.JumpingForce 
            : Runner.JumpForce;
            
        
        Runner.Velocity = new Vector2(Runner.Velocity.X, -JumpForce);
        StateMachine.TransitionTo(RunnerState.Falling);
    }
}