using Godot;
using System;

public partial class SceneChangeTrigger : Area3D
{
	[Export]
	public string sceneName;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void bodyEntered(Node3D body)
	{
		if (body is Player)
		{
			GetTree().ChangeSceneToFile("res://" + sceneName + ".tscn");
		}
	}
}
