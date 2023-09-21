using Godot;
using System;
using System.Collections.Generic;

public partial class Level : Node2D
{
    [Export] private PackedScene NextLevel { get; set; }
    [Export] private int LevelTime { get; set; }
    [Export] private bool IsFinalLevel { get; set; }


    private Start StartPosition { get; set; }
    private Exit Exit { get; set; }
    private Player Player { get; set; }
    private Area2D Deathzone { get; set; }
    private HUD HUD { get; set; }
    private UILayer UILayer { get; set; }
    private Timer TimerNode { get; set; }
    private int TimeLeft { get; set; }
    private bool Win { get; set; }

    public override void _Ready()
    {
        base._Ready();
        StartPosition = GetNode<Start>("Start");
        Player = GetNode<Player>("%Player");
        Exit = GetNode<Exit>("Exit");
        Deathzone = GetNode<Area2D>("Deathzone");
        HUD = GetNode<HUD>("UILayer/HUD");
        UILayer = GetNode<UILayer>("UILayer");

        Player.GlobalPosition = StartPosition.GetSpawnPosition();


        Godot.Collections.Array<Node> traps = GetTree().GetNodesInGroup("traps");

        foreach (Node node in traps)
        {
            if (node is Trap trap)
            {
                trap.TouchedPlayer += OnTrapTouchedPlayer;
            }
        }
        Exit.BodyEntered += OnExitBodyEntered;
        Deathzone.BodyEntered += OnDeathzoneBodyEntered;

        TimeLeft = LevelTime;
        HUD.SetTimeLabel(TimeLeft);

        TimerNode = new Timer();
        TimerNode.Name = "LevelTimer";
        TimerNode.WaitTime = 1;
        TimerNode.Timeout += OnLevelTimerTimeout;
        AddChild(TimerNode);
        TimerNode.Start();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("quit"))
        {
            GetTree().Quit();
        }
        else if (Input.IsActionJustPressed("reset"))
        {
            GetTree().ReloadCurrentScene();
        }
    }

    public void OnDeathzoneBodyEntered(Node2D body)
    {
        if (body is Player)
        {
            ResetPlayer();
        }
    }

    public void OnTrapTouchedPlayer()
    {
        ResetPlayer();
    }

    public void ResetPlayer()
    {
        AudioPlayer.Instance.PlaySFX("Hurt");
        Player.Velocity = Vector2.Zero;
        Player.GlobalPosition = StartPosition.GetSpawnPosition();
    }

    private async void OnExitBodyEntered(Node2D body)
    {
        if (body is Player && (IsFinalLevel || NextLevel != null))
        {
            Player.ReachedGoal = true;
            Exit.Animate();
            Win = true;
            await ToSignal(GetTree().CreateTimer(1.5), Timer.SignalName.Timeout);
            if (IsFinalLevel)
            {
                UILayer.ShowWinScreen(true);
            }
            else
            {
                GetTree().ChangeSceneToPacked(NextLevel);
            }
        }
    }

    private void OnLevelTimerTimeout()
    {
        if (Win == false)
        {
            TimeLeft -= 1;
            HUD.SetTimeLabel(TimeLeft);
            if (TimeLeft < 0)
            {
                ResetPlayer();
                TimeLeft = LevelTime;
                HUD.SetTimeLabel(TimeLeft);
            }
        }
    }
}
