using Godot;
using System;

public partial class Death : Control
{
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		GetNode<AnimationPlayer>("AnimationPlayer").Play("death");
		await ToSignal(GetTree().CreateTimer(5.5, false), SceneTreeTimer.SignalName.Timeout);
		GetTree().Quit();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
