using Godot;
using System;
using System.Threading.Tasks;

public partial class Gameintro : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		AnimationPlayer fadePlayer = GetTree().CurrentScene.GetNode<AnimationPlayer>("cutsceneui/AnimationPlayer");
		fadePlayer.PlayBackwards("fade");

		AnimationPlayer cutscenePlayer = GetTree().CurrentScene.GetNode<AnimationPlayer>("AnimationPlayer");
		cutscenePlayer.Play("cutscene");
		
		await ToSignal(GetTree().CreateTimer(7.1667), SceneTreeTimer.SignalName.Timeout);
		fadePlayer.Play("fade");
		await ToSignal(GetTree().CreateTimer(1), SceneTreeTimer.SignalName.Timeout);
		GetTree().ChangeSceneToFile("res://levels/level.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
