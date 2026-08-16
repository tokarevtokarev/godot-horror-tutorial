using Godot;
using System;

public partial class Door : Node3D, InteractableObject
{
	bool opened = false;

	public void ToggleDoor()
	{
		AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		if (animationPlayer.IsPlaying())
		{
			return;
		}
		opened = !opened;
		animationPlayer.Play(opened ? "open" : "close");
	}

	public void PlayerInteract()
	{
		ToggleDoor();
	}
}
