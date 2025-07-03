using Godot;
using NeonDashTrail.connectors;

namespace NeonDashTrail.entities;

public partial class Runner : CharacterBody2D
{
    [Export] public float Speed { get; set; } = 100.0f;
    [Export] public float Gravity { get; set; } = 800.0f;
    [Export] public float JumpForce { get; set; } = 250.0f;

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("pause"))
            GameEventsConnectorService.RaisePauseGameEvent();
    }

    public override void _PhysicsProcess(double delta)
    {
        HandleMovement((float)delta);
    }

    private void HandleMovement(float delta)
    {
        var currentVelocity = Velocity;
        
        currentVelocity = ApplyGravity(currentVelocity, delta);
        currentVelocity = ProcessJump(currentVelocity);
        currentVelocity = ApplySpeed(currentVelocity);
        
        Velocity = currentVelocity;
        
        MoveAndSlide();
        
        if (CollisionWithObstacle())
            GameEventsConnectorService.RaiseBackToMainMenuEvent();
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
        if (!Input.IsActionJustPressed("jump") || !IsOnFloor()) return velocity;

        velocity.Y = -JumpForce;
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
}