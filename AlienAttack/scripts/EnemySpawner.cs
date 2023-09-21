using Godot;
using System;

public partial class EnemySpawner : Node2D
{
    private Path2D SpawnPath { get; set; }
    private PathFollow2D SpawnPathFollow { get; set; }
    [Export] private PackedScene _enemyScene;
    [Export] private PackedScene _pathEnemyScene;
    [Signal] public delegate void EnemySpawnedEventHandler(Enemy enemy);
    [Signal] public delegate void PathEnemySpawnedEventHandler(PathEnemy enemy);
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SpawnPath = GetNode<Path2D>("SpawnPath");
        SpawnPathFollow = GetNode<PathFollow2D>("SpawnPath/SpawnPathFollow");

        SpawnPath.Curve.AddPoint(new Vector2(GetViewportRect().Size.X + 10, 50));
        SpawnPath.Curve.AddPoint(new Vector2(GetViewportRect().Size.X + 10, GetViewportRect().Size.Y - 50));
    }

    public void OnSpawnTimerTimeout()
    {
        Enemy enemy = _enemyScene.Instantiate<Enemy>();
        EmitSignal(SignalName.EnemySpawned, enemy);
        SpawnPathFollow.ProgressRatio = GD.Randf();
        enemy.GlobalPosition = SpawnPathFollow.Position;
    }

    public void OnPathEnemyTimerTimeout()
    {
        PathEnemy pathEnemy = _pathEnemyScene.Instantiate<PathEnemy>();
        EmitSignal(SignalName.PathEnemySpawned, pathEnemy);
    }
}
