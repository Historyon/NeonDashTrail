using NeonDashTrail.entities;

namespace NeonDashTrail.states.runner_states;

[GlobalClass]
public partial class RunnerStateBase : Node
{
    protected RunnerStateMachine StateMachine;
    protected Runner Runner;

    public virtual RunnerState State => RunnerState.None;

    public void Init(RunnerStateMachine stateMachine, Runner runner)
    {
        StateMachine = stateMachine;
        Runner = runner;
    }

    public virtual void Enter(StateTransitionArgs transitionArgs = null) {  }

    public virtual void Exit() {  }

    public virtual void HandleInput(InputEvent @event) { }

    public virtual void PhysicsProcess(float delta) { }
}