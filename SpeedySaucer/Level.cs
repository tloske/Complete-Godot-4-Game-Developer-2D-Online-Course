using Godot;
using System;

public partial class Level : Node2D
{
    public void OnMazeBodyExited(Node2D body)
    {
        GetTree().ReloadCurrentScene();
    }
}
