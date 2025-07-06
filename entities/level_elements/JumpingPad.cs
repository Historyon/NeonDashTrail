using NeonDashTrail.components;

namespace NeonDashTrail.entities.level_elements;

public partial class JumpingPad : StaticBody2D
{
    [Export] public AnimatedSprite2D AnimatedSprite { get; set; }
    [Export] public JumpAreaComponent JumpAreaComponent { get; set; }
    [Export] public AudioStreamPlayer2D ActivationSound { get; set; }

    private const string IdleAnimation = "idle";
    private const string ActivatedAnimation = "activated";

    private void OnJumpAreaActivated()
    {
        JumpAreaComponent.SetDeferred("disabled", true);
        AnimatedSprite.Play(ActivatedAnimation);
        ActivationSound.Play();
    }

    private void OnAnimationFinished()
    {
        JumpAreaComponent.SetDeferred("disabled", false);
        AnimatedSprite.Play(IdleAnimation);
    }
}