using Godot;
using System;

public partial class HUD : Control
{
    private Label _scoreLabel;
    private Label _livesLeft;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _scoreLabel = GetNode<Label>("VBoxContainer/Score");
        _livesLeft = GetNode<Label>("VBoxContainer/HBoxContainer/LivesLeft");
    }

    public void SetScoreLabel(int newScore)
    {
        _scoreLabel.Text = $"SCORE: {newScore}";
    }

    public void SetLives(int amount)
    {
        _livesLeft.Text = $"{amount}";
    }
}
