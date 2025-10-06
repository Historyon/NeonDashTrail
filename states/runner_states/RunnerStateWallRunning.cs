using NeonDashTrail.scripts.enums;
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

        var velocityX = Runner.WallRunDirection == WallRunDirection.Left ? Runner.WallRunSpeed * -1
            : Runner.WallRunDirection == WallRunDirection.Right ? Runner.WallRunSpeed : 0f;
        var velocityY = Runner.WallRunDirection == WallRunDirection.Up ? Runner.WallRunSpeed * -1
                    : Runner.WallRunDirection == WallRunDirection.Down ? Runner.WallRunSpeed : 0f;

        Runner.Velocity = new Vector2(velocityX, velocityY);
    }
}
