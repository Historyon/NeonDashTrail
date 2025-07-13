namespace NeonDashTrail.states.runner_states;

[GlobalClass]
public partial class RunnerStateRunning : RunnerStateBase
{
    public override RunnerState State => RunnerState.Running;

    public override void Enter(StateTransitionArgs transitionArgs = null)
    {
        Runner.DashResetRequired = false;
    }

    public override void HandleProcess(float delta)
    {
        if (!Runner.IsOnFloor())
        {
            StateMachine.TransitionTo(RunnerState.Falling);
            return;
        }

        if (Input.IsActionJustPressed(Controls.Jump))
        {
            Runner.Velocity = new Vector2(Runner.Velocity.X, -Runner.JumpForce);
            StateMachine.TransitionTo(RunnerState.Falling);
            return;
        }

        if (Input.IsActionJustPressed(Controls.Dash) && Runner.IsDashPossible)
        {
            StateMachine.TransitionTo(RunnerState.Dashing);
            return;
        }

        var velocity = Runner.Velocity;
        velocity.X = Runner.Speed;
        Runner.Velocity = velocity;
    }
}