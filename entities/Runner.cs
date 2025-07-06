using NeonDashTrail.connectors;
using NeonDashTrail.entities.level_elements;
using NeonDashTrail.interfaces;

namespace NeonDashTrail.entities;

public partial class Runner : CharacterBody2D, IJumpableObject
{
    [Export] public float Speed { get; set; } = 100.0f;
    [Export] public float Gravity { get; set; } = 800.0f;
    [Export] public float JumpForce { get; set; } = 250.0f;
    [Export] public RayCast2D CheckpointGoalRayCast { get; set; }
    [Export] public AudioStreamPlayer2D CheckpointAudio { get; set; }

    private int _lastReachedCheckpointNumber;
    private float? _externalJumpForce;

    public override void _Ready()
    {
        // Disable Processing and running from the beginning
        StopRun();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed(Controls.Pause))
            GameEventsConnectorService.RaisePauseGameEvent();
        
        if (@event.IsActionPressed(Controls.Reset))
            LevelEventsConnectorService.RaiseResetToCheckpointEvent();
    }

    public override void _PhysicsProcess(double delta)
    {
        HandleMovement((float)delta);
        HandleCollisions();
    }
    
    public void StartRun() => SetPhysicsProcess(true);
    
    public void StopRun() => SetPhysicsProcess(false);
    
    public void AddJumpForce(float jumpForce) => _externalJumpForce = jumpForce;

    private void HandleMovement(float delta)
    {
        var currentVelocity = Velocity;
        
        currentVelocity = ApplyGravity(currentVelocity, delta);
        currentVelocity = ProcessJump(currentVelocity);
        currentVelocity = ApplySpeed(currentVelocity);
        
        Velocity = currentVelocity;
        
        MoveAndSlide();
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
            LevelEventsConnectorService.RaiseResetToCheckpointEvent();
    }

    private Vector2 ApplySpeed(Vector2 velocity)
    {
        velocity.X = Speed;
        return velocity;
    }

    private Vector2 ApplyGravity(Vector2 velocity, float delta)
    {
        if (IsOnFloor()) return velocity;

        velocity.Y += Gravity * delta;
        return velocity;
    }

    private Vector2 ProcessJump(Vector2 velocity)
    {
        if (!IsOnFloor())
        {
            _externalJumpForce = null;
            return velocity;
        }

        if (_externalJumpForce is > 0)
        {
            velocity.Y = -_externalJumpForce.Value;
            _externalJumpForce = null;
            return velocity;       
        }

        if (Input.IsActionJustPressed(Controls.Jump))
        {
            velocity.Y = -JumpForce;
        }

        return velocity;
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
}