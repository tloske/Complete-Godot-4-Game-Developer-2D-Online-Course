using Godot;
using System;

public partial class Run : State
{
    public override void Enter() => AnimatedSprite.Play("run");

    public override void Exit() { }

    public override void PhysicsUpdate(double delta)
    {
        if (Player.ReachedGoal) { StateMachine.ChangeState("Goal"); }
        float direction = Input.GetAxis("move_left", "move_right");
        Player.Move(delta, direction);

        if (Input.IsActionJustPressed("jump")) { StateMachine.ChangeState("Jump"); return; }
        if (!Player.IsOnFloor()) { StateMachine.ChangeState("Fall"); return; }
        if (Player.Velocity.X == 0.0f) { StateMachine.ChangeState("Idle"); return; }
    }

    public override void Update(double delta) { }
}
