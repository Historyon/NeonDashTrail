namespace NeonDashTrail.states.runner_states;

[GlobalClass]
public partial class RunnerStateFalling : RunnerStateBase
{
    
    public override RunnerState State => RunnerState.Falling;

    public override void HandleProcess(float delta)
    {
        var velocity = Runner.Velocity;
        velocity.Y += Runner.Gravity * delta;
        velocity.X = Runner.Speed;
        Runner.Velocity = velocity;
        
        if (Runner.IsOnFloor())
            StateMachine.TransitionTo(RunnerState.Running);
            
        if (Input.IsActionJustPressed(Controls.WallRun) && Runner.IsWallRunPossible)
        {
            StateMachine.TransitionTo(RunnerState.WallRunning);
            return;
        }

        if (Input.IsActionJustPressed(Controls.Dash) && Runner.IsDashPossible)
            StateMachine.TransitionTo(RunnerState.Dashing);
    }
}