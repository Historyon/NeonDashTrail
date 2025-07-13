namespace NeonDashTrail.states.runner_states;

[GlobalClass]
public partial class RunnerStateFalling : RunnerStateBase
{
    [Export] public Timer DashLockTimer { get; set; }
    
    public override RunnerState State => RunnerState.Falling;

    public override void HandleProcess(float delta)
    {
        var velocity = Runner.Velocity;
        velocity.Y += Runner.Gravity * delta;
        Runner.Velocity = velocity;
        
        if (Runner.IsOnFloor())
            StateMachine.TransitionTo(RunnerState.Running);

        if (Input.IsActionJustPressed(Controls.Dash) && Runner.IsDashPossible) 
            StateMachine.TransitionTo(RunnerState.Dashing);
    }
}