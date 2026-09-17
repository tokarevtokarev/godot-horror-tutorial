using Godot;
using System;

public partial class TaskTrigger : Area3D
{
	[Export]
	public String taskText;

	private bool triggered = false;
	private PlayerUi playerUi;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerUi = GetTree().CurrentScene.GetNode<PlayerUi>("player/player_ui");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void EnterTrigger(Node3D body)
	{
		if (!triggered && body is Player)
		{
			triggered = true;
			playerUi.setTask(taskText);
		}
	}
	
}
