using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;
    private AnimatedSprite2D AnimatedSprite { get; set; }
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    public bool ReachedGoal { get; set; } = false;
    public AudioStreamPlayer JumpAudioStream { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        AnimatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        JumpAudioStream = GetNode<AudioStreamPlayer>("Jump");
    }

    // // Get the gravity from the project settings to be synced with RigidBody nodes.
    // public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    // public override void _PhysicsProcess(double delta)
    // {
    //     Vector2 velocity = Velocity;

    //     // Add the gravity.
    //     if (!IsOnFloor())
    //         velocity.Y = Mathf.Min(velocity.Y + gravity * (float)delta, 500);

    //     // Handle Jump.
    //     if (Input.IsActionJustPressed("jump") && IsOnFloor())
    //     {
    //         velocity.Y = JumpVelocity;
    //     }

    //     float direction = Input.GetAxis("move_left", "move_right");
    //     if (direction != 0.0f)
    //     {
    //         velocity.X = direction * Speed;
    //     }
    //     else
    //     {
    //         velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
    //     }


    //     Velocity = velocity;
    //     PlayAnimation();
    //     MoveAndSlide();
    // }

    public void Move(double delta, float direction)
    {
        Vector2 velocity = Velocity;

        if (!IsOnFloor())
        {
            velocity.Y = Mathf.Min(velocity.Y + gravity * (float)delta, 500);
        }

        if (direction != 0.0f)
        {
            velocity.X = direction * Player.Speed;
            AnimatedSprite.FlipH = direction < 0;
        }
        else
        {
            velocity.X = Mathf.MoveToward(velocity.X, 0, Player.Speed);
        }

        Velocity = velocity;

        MoveAndSlide();
    }

    public void Jump(float force)
    {
        Vector2 velocity = Velocity;

        velocity.Y = -force;

        Velocity = velocity; ;
        MoveAndSlide();
    }

}
