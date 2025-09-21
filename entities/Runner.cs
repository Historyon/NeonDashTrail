using NeonDashTrail.connectors;
using NeonDashTrail.entities.level_elements;
using NeonDashTrail.interfaces;
using NeonDashTrail.states.runner_states;
using NeonDashTrail.states.runner_states.args;

namespace NeonDashTrail.entities;

public partial class Runner : CharacterBody2D, IJumpableObject
{
    [Export] public float Speed { get; set; } = 100.0f;
    [Export] public float Gravity { get; set; } = 800.0f;
    [Export] public float JumpForce { get; set; } = 250.0f;
    [Export] public RayCast2D CheckpointGoalRayCast { get; set; }
    [Export] public AudioStreamPlayer2D CheckpointAudio { get; set; }
    [Export] public RunnerStateMachine StateMachine { get; set; }
    [Export, ExportCategory("Dash")] public float DashForce { get; set; } = 300.0f;
    [Export] public float DashDuration { get; set; } = 0.2f;
    [Export] public Timer DashLockTimer { get; set; }

    private int _lastReachedCheckpointNumber;

    public bool DashResetRequired { get; set; }
    public bool IsDashPossible => DashLockTimer.TimeLeft <= 0 && !DashResetRequired;
    public bool IsWallRunPossible => _isPlayerOnRunnableWall;

    private bool _isPlayerOnRunnableWall;

    public override void _Ready()
    {
        // Disable Processing and running from the beginning
        StopRun();
        StateMachine.Init();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed(Controls.Pause))
            GameEventsConnectorService.RaisePauseGameEvent();

        if (@event.IsActionPressed(Controls.Reset))
        {
            LevelEventsConnectorService.RaiseResetToCheckpointEvent();
            StateMachine.TransitionTo(RunnerState.Running);
        }

        StateMachine?.Input(@event);
    }

    public override void _PhysicsProcess(double delta)
    {
        StateMachine?.Process((float)delta);
        MoveAndSlide();
        HandleCollisions();
    }

    public void StartRun()
    {
        SetPhysicsProcess(true);
        StateMachine.SetPhysicsProcess(true);
    }

    public void StopRun()
    {
        SetPhysicsProcess(false);
        StateMachine.SetPhysicsProcess(false);
    }

    public void AddJumpForce(float jumpForce)
    {
        DashLockTimer.Start();
        StateMachine.TransitionTo(RunnerState.Jumping, new RunnerJumpingStateArgs(jumpForce));
    }

    private void HandleCollisions()
    {
        if (IsGoalReached())
        {
            GoalReached();
            return;
        }

        CheckForCheckpoint();

        if (CollisionWithObstacle())
        {
            LevelEventsConnectorService.RaiseResetToCheckpointEvent();
            StateMachine.TransitionTo(RunnerState.Running);
        }
    }

    /// <summary>
    /// Determines if the runner has collided with an obstacle based on slide collision data.
    /// Checks the normal of the collision to determine if it aligns with the predefined obstacle criteria.
    /// </summary>
    /// <returns>
    /// A boolean value indicating whether a collision with an obstacle has occurred (true) or not (false).
    /// </returns>
    private bool CollisionWithObstacle()
    {
        var slideCollisionCount = GetSlideCollisionCount();

        if (slideCollisionCount == 0) return false;

        for (var i = 0; i < slideCollisionCount; i++)
        {
            var collision = GetSlideCollision(i);

            var collisionNormal = collision.GetNormal();

            if (collisionNormal.X < Constants.MinFrontalCollisionNormalX &&
                Mathf.Abs(collisionNormal.Y) < Constants.MaxFrontalCollisionNormalYAbsolute)
            {
                return true;
            }
        }

        return false;
    }

    private void CheckForCheckpoint()
    {
        if (!CheckpointGoalRayCast.IsColliding() ||
            CheckpointGoalRayCast.GetCollider() is not Checkpoint checkpoint ||
            checkpoint.CheckpointNumber == _lastReachedCheckpointNumber) return;

        _lastReachedCheckpointNumber = checkpoint.CheckpointNumber;
        LevelEventsConnectorService.RaiseCheckpointReachedEvent(checkpoint.CheckpointNumber);
        CheckpointAudio.Play();
    }

    private bool IsGoalReached() => CheckpointGoalRayCast.IsColliding() && CheckpointGoalRayCast.GetCollider() is Goal;

    private void GoalReached()
    {
        StopRun();
        LevelEventsConnectorService.RaiseGoalReachedEvent();
    }

    private void OnWallRunWallDetected(bool entered)
    {
        _isPlayerOnRunnableWall = entered;
    }

    private void OnStateChanged(RunnerState fromState, RunnerState toState)
    {
        
    }
}