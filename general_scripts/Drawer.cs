using Godot;
using System;

public partial class Drawer : Node3D, InteractableObject
{

	[Export]
	private AudioStreamPlayer3D openSound;

	[Export]
	private AudioStreamPlayer3D closeSound;

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
			openSound.Play();
		}
		else
		{
			animationPlayer.PlayBackwards("open");
			closeSound.Play();
		}
	}

	public void PlayerInteract()
	{
		ToggleDrawer();
	}
}
