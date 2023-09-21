using Godot;
using System;

public partial class Fall : State
{
    // // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void Enter() { }

    public override void Exit() { }

    public override void PhysicsUpdate(double delta)
    {
        if (Player.Velocity.Y > 0)
        {
            AnimatedSprite.Play("fall");
        }
        else
        {
            AnimatedSprite.Play("jump");
        }
        float direction = Input.GetAxis("move_left", "move_right");
        Player.Move(delta, direction);

        if (Player.IsOnFloor() && Player.Velocity.X == 0.0f) { StateMachine.ChangeState("Idle"); return; }
        if (Player.IsOnFloor() && Player.Velocity.X != 0.0f) { StateMachine.ChangeState("Run"); return; }
    }

    public override void Update(double delta) { }
}
