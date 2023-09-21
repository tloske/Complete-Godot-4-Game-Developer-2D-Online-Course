using Godot;
using System;

public partial class Rocket : Area2D
{

    [Export] private const float Speed = 600.0f;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector2 globalPosition = GlobalPosition;
        globalPosition.X += Speed * (float)delta;
        GlobalPosition = globalPosition;
    }

    public void OnVisibleOnScreenNotifierScreenExited()
    {
        QueueFree();
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area is Enemy enemy)
        {
            QueueFree();
            enemy.Die();
        }
    }
}
