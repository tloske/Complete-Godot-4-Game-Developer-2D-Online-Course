using Godot;
using System;

public partial class Start : StaticBody2D
{
    public Vector2 GetSpawnPosition()
    {
        return GetNode<Marker2D>("SpawnPosition").GlobalPosition;
    }
}
