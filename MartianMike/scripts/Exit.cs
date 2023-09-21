using Godot;
using System;

public partial class Exit : Area2D
{
    private AnimatedSprite2D Sprite { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }
    public void Animate()
    {
        Sprite.Play();
    }
}
