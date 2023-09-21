using Godot;
using System;

public partial class BG : ParallaxBackground
{
    [Export] private CompressedTexture2D BG_Texture { get; set; }
    [Export] private float ScrollSpeed { get; set; } = 15.0f;
    private Sprite2D Sprite { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Sprite = GetNode<Sprite2D>("ParallaxLayer/Sprite2D");
        Sprite.Texture = BG_Texture;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Rect2 newRegionRect = Sprite.RegionRect;
        Vector2 newRegionRectPosition = newRegionRect.Position + (float)delta * new Vector2(ScrollSpeed, ScrollSpeed);
        if (newRegionRectPosition >= new Vector2(64, 64))
        {
            newRegionRectPosition = Vector2.Zero;
        }
        newRegionRect.Position = newRegionRectPosition;
        Sprite.RegionRect = newRegionRect;
    }
}
