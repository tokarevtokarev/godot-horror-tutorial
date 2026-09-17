using Godot;
using System;

public partial class DoorBell : Node3D, InteractableObject
{
	[Export]
	public Door door;

	private PlayerUi playerUi;

	public int timesRung = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		door.locked = true;
		playerUi = GetTree().CurrentScene.GetNode<PlayerUi>("player/player_ui");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	async public void PlayerInteract()
	{
		AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		if (animationPlayer.IsPlaying())
			return;

		timesRung++;
		animationPlayer.Play("press");

		if (timesRung >= 3)
		{
			await ToSignal(GetTree().CreateTimer(4.0f, false), SceneTreeTimer.SignalName.Timeout);
			door.locked = false;
			playerUi.setTask("Enter the house");
		}
	}
}
