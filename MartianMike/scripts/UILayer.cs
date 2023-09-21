using Godot;
using System;

public partial class UILayer : CanvasLayer
{

    private WinScreen WinScreen { get; set; }

    public override void _Ready()
    {
        base._Ready();

        WinScreen = GetNode<WinScreen>("WinScreen");
    }
    public void ShowWinScreen(bool flag)
    {
        WinScreen.Visible = flag;
    }
}
