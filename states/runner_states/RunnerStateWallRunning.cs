using NeonDashTrail.states;
using NeonDashTrail.states.runner_states;

[GlobalClass]
public partial class RunnerStateWallRunning : RunnerStateBase
{
    [Export] public Timer DashLockTimer { get; set; }

    public override RunnerState State => RunnerState.WallRunning;

    public override void Enter(StateTransitionArgs transitionArgs = null)
    {
        DashLockTimer.Start();
    }

    public override void HandleProcess(float delta)
    {
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

        if (!Runner.IsWallRunPossible)
        {
            StateMachine.TransitionTo(RunnerState.Falling);
            return;
        }

        Runner.Velocity = new Vector2(Runner.Speed, 0f);
    }
}
