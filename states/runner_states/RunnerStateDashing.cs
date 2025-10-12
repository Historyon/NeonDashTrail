namespace NeonDashTrail.states.runner_states;

[GlobalClass]
public partial class RunnerStateDashing : RunnerStateBase
{
    public override RunnerState State => RunnerState.Dashing;
    
    [Export] public Timer DashTimer { get; set; }
    [Export] public AudioStreamPlayer2D DashSound { get; set; }

    protected override void AfterInit()
    {
        DashTimer.WaitTime = Runner.DashDuration;
        DashTimer.OneShot = true;
    }

    public override void Enter(StateTransitionArgs transitionArgs = null)
    {
        DashSound.Play();
        Runner.DashResetRequired = true;
        Runner.Velocity = new Vector2(Runner.Speed + Runner.DashForce, 0);
        DashTimer.Start();
    }

    public override void HandleProcess(float delta)
    {
        if (Input.IsActionJustPressed(Controls.WallRun) && PlayerScanner.IsWallRunPossible)
            StateMachine.TransitionTo(RunnerState.WallRunning);
    }

    public override void Exit()
    {
        DashTimer.Stop();
        Runner.Velocity = new Vector2(Runner.Speed, 0);
    }

    private void OnDashTimerTimeout()
    {
        if (Runner.IsOnFloor())
            StateMachine.TransitionTo(RunnerState.Running);
        else
            StateMachine.TransitionTo(RunnerState.Falling);
    }
}