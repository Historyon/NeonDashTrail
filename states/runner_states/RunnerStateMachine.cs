using System.Collections.Generic;
using NeonDashTrail.entities;

namespace NeonDashTrail.states.runner_states;

public partial class RunnerStateMachine : Node
{
    [Export] public RunnerState InitialState { get; set; }
    [Export] public Runner Runner { get; set; }
    
    private readonly Dictionary<RunnerState, RunnerStateBase> _states = new();
    private RunnerStateBase _currentState;

    public override void _Ready()
    {
        foreach (var child in GetChildren())
        {
            if (child is RunnerStateBase state)
            {   
                state.Init(this, Runner);
                _states.Add(state.State, state);
            }
        }
        
        _currentState = _states[InitialState];
    }

    public override void _Input(InputEvent @event)
    {
        _currentState?.HandleInput(@event);
    }

    public override void _PhysicsProcess(double delta)
    {
        _currentState?.PhysicsProcess((float) delta);
    }

    public void TransitionTo(RunnerState toState, StateTransitionArgs transitionArgs = null)
    {
        _currentState?.Exit();
        _currentState = _states[toState];
        _currentState?.Enter(transitionArgs);
    }
}