using Godot;
using System;

public partial class HUD : Control
{
    private Label TimeLabel { get; set; }

    public override void _Ready()
    {
        base._Ready();
        TimeLabel = GetNode<Label>("TimeLabel");
    }

    public void SetTimeLabel(int value)
    {
        TimeLabel.Text = $"TIME: {value}";
    }
}
