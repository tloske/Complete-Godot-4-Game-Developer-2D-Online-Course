using Godot;
using System;

public partial class Enemy : Area2D
{
    [Export] public float Speed { get; private set; } = 300.0f;
    [Signal] public delegate void DiedEventHandler();

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector2 globalPosition = GlobalPosition;
        globalPosition.X -= Speed * (float)delta;
        GlobalPosition = globalPosition;
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body is Player player)
        {
            player.TakeDamage();
            Die();
        }
    }

    public void OnVisibleOnScreenNotifierScreenExited()
    {
        QueueFree();
    }

    public void Die()
    {
        EmitSignal(SignalName.Died);
        QueueFree();
    }
}
