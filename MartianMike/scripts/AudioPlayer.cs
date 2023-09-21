using Godot;
using System;

public partial class AudioPlayer : Node
{
    public static AudioPlayer Instance { get; private set; }
    public AudioStream Hurt { get; private set; } = ResourceLoader.Load("res://assets/audio/hurt.wav") as AudioStream;
    public AudioStream Jump { get; private set; } = ResourceLoader.Load("res://assets/audio/jump.wav") as AudioStream;

    public AudioPlayer()
    {
        Instance = this;
    }

    public void PlaySFX(String name)
    {
        AudioStream stream = null;
        switch (name)
        {
            case "Hurt":
                stream = Hurt;
                break;
            case "Jump":
                stream = Jump;
                break;
            default:
                GD.Print("Invalid SFX Name");
                return;
        }

        AudioStreamPlayer asp = new AudioStreamPlayer();
        asp.Stream = stream;
        asp.Name = "SFX";

        AddChild(asp);

        asp.Play();
        asp.Finished += asp.QueueFree;
    }
}
