using Godot;
using System;

public partial class Ending : Control
{
	private AnimationPlayer animationPlayer;
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("fade");
		await ToSignal(GetTree().CreateTimer(8.0, true), SceneTreeTimer.SignalName.Timeout);
		GetTree().Quit();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
