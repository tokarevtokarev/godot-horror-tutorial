using Godot;
using System;

public partial class Enemy : CharacterBody3D
{
	[Export]
	public Node3D[] patrolDestinations = new Node3D[0];

	[Export(PropertyHint.Range, "0.1,20,0.1")]
	public float speed = 3.0f;

	private Player player;
	private NavigationAgent3D navAgent;

	private RandomNumberGenerator rng = new RandomNumberGenerator();

	public Node3D destination;
	public int? destinationValue = null;

	private bool chasing = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetTree().CurrentScene.GetNode<Player>("player");
		navAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
		pickDestination();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (destination == null)
			return;

		updateTargetLocation();

	}

	public override void _PhysicsProcess(double delta)
	{
		if (!IsOnFloor())
		{
			Velocity += GetGravity() * (float)delta;
		}

		if (destination != null)
		{
			// Move enemy to target
			var currentLocation = GlobalTransform.Origin;
			var nextLocation = navAgent.GetNextPathPosition();
			var newVelocity = (nextLocation - currentLocation).Normalized() * speed;
			Velocity = Velocity.MoveToward(newVelocity, 0.25f);

			// Rotate enemy to moving direction
			var lookDir = Mathf.LerpAngle(Mathf.DegToRad(GlobalRotationDegrees.Y), Mathf.Atan2(-Velocity.X, -Velocity.Z), 0.5f);
			var tmpRotation = GlobalRotationDegrees;
			tmpRotation.Y = Mathf.RadToDeg(lookDir);
			GlobalRotationDegrees = tmpRotation;

			MoveAndSlide();
		}
	}

	public void pickDestination()
	{
		int chance = rng.RandiRange(0, patrolDestinations.Length - 1);
		destination = patrolDestinations[chance];
		if (chance == destinationValue)
			pickDestination();
		else
		{
			GD.Print("Next Enemy destination: ", chance);
			destinationValue = chance;
		}
	}

	private void updateTargetLocation()
	{
		navAgent.TargetPosition = destination.GlobalTransform.Origin;
	}
}
