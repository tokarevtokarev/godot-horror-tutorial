using Godot;
using System;

public partial class DoorBell : Node3D, InteractableObject
{
	[Export]
	public Door door;


	public int timesRung = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		door.locked = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PlayerInteract()
	{
		AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		if (animationPlayer.IsPlaying())
			return;

		timesRung++;
		animationPlayer.Play("press");

		if (timesRung >= 3)
		{
			door.locked = false;
		}
	}
}
