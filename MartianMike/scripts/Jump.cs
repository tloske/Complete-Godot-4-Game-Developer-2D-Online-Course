using Godot;
using System;

public partial class Jump : State
{
    public override void Enter()
    {
        Vector2 velocity = Player.Velocity;
        velocity.Y = Player.JumpVelocity;
        Player.Velocity = velocity;
        AnimatedSprite.Play("jump");
        AudioPlayer.Instance.PlaySFX("Jump");
    }

    public override void Exit() { }

    public override void PhysicsUpdate(double delta)
    {
        float direction = Input.GetAxis("move_left", "move_right");
        Player.Move(delta, direction);
        if (Player.Velocity.Y > 0) { StateMachine.ChangeState("Fall"); return; }
    }

    public override void Update(double delta) { }
}
