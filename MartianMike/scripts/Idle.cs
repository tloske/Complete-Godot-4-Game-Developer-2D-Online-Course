using Godot;
using System;

public partial class Idle : State
{
    public override void Enter() => AnimatedSprite.Play("idle");

    public override void Exit() { }

    public override void PhysicsUpdate(double delta)
    {
        if (Player.ReachedGoal) { StateMachine.ChangeState("Goal"); }
        if (Input.IsActionJustPressed("jump")) { StateMachine.ChangeState("Jump"); return; }
        if (!Player.IsOnFloor()) { StateMachine.ChangeState("Fall"); return; }
        if (Input.GetAxis("move_left", "move_right") != 0.0f) { StateMachine.ChangeState("Run"); return; }
    }

    public override void Update(double delta) { }
}
