using NeonDashTrail.interfaces;

namespace NeonDashTrail.components;

public partial class JumpAreaComponent : Area2D
{
    [Export] public float JumpForce { get; set; } = 500.0f;
    
    [Signal] public delegate void JumpAreaActivatedEventHandler();
    
    private void OnBodyEntered(Node2D body)
    {
        if (body is not IJumpableObject jumpableObject) return;
        
        jumpableObject.AddJumpForce(JumpForce);
        EmitSignalJumpAreaActivated();
    }
}