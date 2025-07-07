namespace NeonDashTrail.states.runner_states;

[GlobalClass]
public partial class RunnerStateFalling : RunnerStateBase
{
    public override RunnerState State => RunnerState.Falling;

    public override void PhysicsProcess(float delta)
    {
        var velocity = Runner.Velocity;
        velocity.Y += Runner.Gravity * delta;
        Runner.Velocity = velocity;
        
        if (Runner.IsOnFloor())
            StateMachine.TransitionTo(RunnerState.Running);
    }
}