using Godot;
using System;

public partial class WinScreen : Control
{
    public void OnButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/level.tscn");
    }
}
