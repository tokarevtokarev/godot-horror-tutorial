using Godot;
using System;
using System.Collections.Generic;

public partial class CodePaper : RigidBody3D
{
	[Export]
	public Node3D[] positions;

	[Export]
	public Door doorToUnlock;

	private RandomNumberGenerator rng = new RandomNumberGenerator();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		int chance = rng.RandiRange(0, positions.Length - 1);
		Transform3D newTransform = positions[chance].GlobalTransform;
		GD.Print("CodePaper position: " + chance);
		GlobalTransform = newTransform;
		Visible = false;
	}

	public void PickupKey()
	{
		doorToUnlock.locked = false;
		QueueFree();
		
	}

}
