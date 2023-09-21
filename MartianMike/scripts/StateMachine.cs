using Godot;
using System;

public partial class StateMachine : Node
{
    public State CurrentState { get; private set; }

    public void ChangeState(String state)
    {
        if (CurrentState != null)
            CurrentState.Exit();
        CurrentState = GetNode<State>(state);
        CurrentState.Enter();
    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Player player = Owner as Player;
        AnimatedSprite2D animatedSprite = player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        foreach (State state in GetChildren())
        {

            state.StateMachine = this;
            state.AnimatedSprite = animatedSprite;
            state.Player = player;
            // GD.Print(state.Name);
        }

        ChangeState("Idle");
        return;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        CurrentState.Update(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        CurrentState.PhysicsUpdate(delta);
    }
}
