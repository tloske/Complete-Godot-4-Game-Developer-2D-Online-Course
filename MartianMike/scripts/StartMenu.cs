using Godot;
using System;

public partial class StartMenu : Control
{
    public void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }

    public void OnStartButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/level.tscn");
    }
}
