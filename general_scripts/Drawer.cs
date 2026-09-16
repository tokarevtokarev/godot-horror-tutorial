using Godot;
using System;

public partial class Drawer : Node3D, InteractableObject
{
	bool opened = false;

	public void ToggleDrawer()
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
		ToggleDrawer();
	}
}
