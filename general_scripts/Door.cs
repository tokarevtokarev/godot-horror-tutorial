using Godot;
using System;

public partial class Door : Node3D, InteractableObject
{
	bool opened = false;

	[Export]
	public bool locked = false;

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

	public void EnemyOpenDoor(Node3D body)
	{
		if (body is not Enemy || locked || animationPlayer.CurrentAnimation == "open")
			return;

		opened = true;
		animationPlayer.Play("open");
	}

	public void EnemyCloseDoor(Node3D body)
	{
		if (body is not Enemy || locked || animationPlayer.CurrentAnimation == "open")
			return;

		opened = false;
		animationPlayer.PlayBackwards("open");
	}
}
