using Godot;
using System;

public partial class Trap : Node2D
{
    [Signal] public delegate void TouchedPlayerEventHandler();

    public void OnArea2dBodyEntered(Node2D body)
    {
        if (body is Player player)
        {
            EmitSignal(SignalName.TouchedPlayer);
        }
    }
}
