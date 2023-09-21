using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Signal] public delegate void tookDamageEventHandler();
    public const float Speed = 300.0f;
    public const float AttackSpeed = 1.0f;
    private bool _canShoot = true;

    [Export] public PackedScene RocketScene { get; private set; }
    public Node2D RocketSpawnLocation { get; private set; }
    public Timer ShootTimer { get; private set; }

    public AudioStreamPlayer RocketSound { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        RocketSpawnLocation = GetNode<Node2D>("RocketSpawn");
        ShootTimer = GetNode<Timer>("Timer");
        RocketSound = GetNode<AudioStreamPlayer>("RocketSound");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (Input.IsActionPressed("shoot") && _canShoot)
        {
            Shoot();
        }
    }



    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        if (direction != Vector2.Zero)
        {
            velocity = direction * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();
        GlobalPosition = GlobalPosition.Clamp(Vector2.Zero, GetViewportRect().Size);
    }


    public void Shoot()
    {
        Rocket rocket = RocketScene.Instantiate<Rocket>();
        rocket.GlobalPosition = RocketSpawnLocation.GlobalPosition;
        GetTree().Root.AddChild(rocket);
        RocketSound.Play();
        _canShoot = false;
        ShootTimer.Start();
    }

    public void TakeDamage()
    {
        EmitSignal(SignalName.tookDamage);
    }


    public void Die()
    {
        QueueFree();
    }

    public void OnTimerTimeout()
    {
        _canShoot = true;
    }
}
