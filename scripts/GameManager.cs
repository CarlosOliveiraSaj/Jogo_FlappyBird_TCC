using Godot;
using System;

public partial class GameManager : Node
{
	public static GameManager Instance {get; private set; }
	public bool IsGamePlaying = false;

    public override void _Ready()
    {
        Instance = this;
    }

}
