using Godot;
using System;

public partial class Door : Node3D, InteractableObject
{
	bool opened = false;
	[Export]
	public bool locked = false;

	public void ToggleDoor()
	{
		AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		if (animationPlayer.IsPlaying())
		{
			return;
		}
		opened = !opened;
		if (opened)
		{
			animationPlayer.Play("open");
		}
		else
		{
			animationPlayer.PlayBackwards("open");
		}
	}

	public void PlayerInteract()
	{
		if (locked)
			return;

		ToggleDoor();
	}
}
