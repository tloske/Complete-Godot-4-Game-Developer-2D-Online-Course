using Godot;
using System;

public partial class JumpPad : Area2D
{

    private AnimatedSprite2D AnimatedSprite { get; set; }
    [Export] private float JumpForce { get; set; } = 300;

    public override void _Ready()
    {
        base._Ready();
        AnimatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public void OnJumpPadBodyEntered(Node2D body)
    {
        if (body is Player player)
        {
            player.Jump(JumpForce);
            AnimatedSprite.Play("jump");
        }
    }

}
