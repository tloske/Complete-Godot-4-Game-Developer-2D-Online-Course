using Godot;
using System;

public partial class Player : RigidBody2D
{
    const int FORCE = 1000;
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector2 input = Input.GetVector("left", "right", "up", "down").Normalized();
        ApplyForce(input * FORCE);
    }
}
