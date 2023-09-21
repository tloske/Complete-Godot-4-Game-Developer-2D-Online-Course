using Godot;
using System;

public partial class PathEnemy : Path2D
{
    public PathFollow2D PathFollow { get; private set; }
    public Enemy Enemy { get; private set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        PathFollow = GetNode<PathFollow2D>("PathFollow2D");
        Enemy = GetNode<Enemy>("PathFollow2D/Enemy");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        PathFollow.ProgressRatio = Mathf.Clamp(PathFollow.ProgressRatio - 0.25f * (float)delta, 0, 1);
        if (PathFollow.ProgressRatio <= 0)
        {
            QueueFree();
        }
    }
}
