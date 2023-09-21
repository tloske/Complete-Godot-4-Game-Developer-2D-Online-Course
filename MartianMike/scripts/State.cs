using Godot;
using System;

public abstract partial class State : Node
{

    public StateMachine StateMachine { get; set; }
    public Player Player { get; set; }
    public AnimatedSprite2D AnimatedSprite { get; set; }

    public abstract void Enter();

    public abstract void Update(double delta);

    public abstract void PhysicsUpdate(double delta);

    public abstract void Exit();
}
