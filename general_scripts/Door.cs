using Godot;
using System;

public partial class Door : Node3D, InteractableObject
{
	bool opened = false;

	[Export]
	public bool locked = false;

	[Export]
	private AudioStreamPlayer3D openSound;

	[Export]
	private AudioStreamPlayer3D closeSound;

	private AnimationPlayer animationPlayer;

	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}


	public void ToggleDoor()
	{
		if (animationPlayer.IsPlaying())
		{
			return;
		}
		opened = !opened;
		if (opened)
		{
			openDoor();
		}
		else
		{
			closeDoor();
		}
	}

	private void openDoor()
	{
		animationPlayer.Play("open");
		openSound.Play();
	}

	private void closeDoor()
	{
		animationPlayer.PlayBackwards("open");
		closeSound.Play();
	}

	public void PlayerInteract()
	{
		if (locked)
			return;

		ToggleDoor();
	}

	public void EnemyOpenDoor(Node3D body)
	{
		if (body is not Enemy || locked || animationPlayer.CurrentAnimation == "open" || opened)
			return;

		opened = true;
		openDoor();
	}

	public void EnemyCloseDoor(Node3D body)
	{
		if (body is not Enemy || locked || animationPlayer.CurrentAnimation == "open" || !opened)
			return;

		opened = false;
		closeDoor();
	}
}
