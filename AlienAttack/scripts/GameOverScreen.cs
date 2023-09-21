using Godot;
using System;

public partial class GameOverScreen : Control
{
    public void SetScore(int newScore) => GetNode<Label>("Panel/VBoxContainer/Score").Text = $"SCORE: {newScore}";

    public void OnRetryButtonPressed() => GetTree().ReloadCurrentScene();
}
