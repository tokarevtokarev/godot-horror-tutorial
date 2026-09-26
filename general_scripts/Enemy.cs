using Godot;
using System;

public partial class Enemy : CharacterBody3D
{
	[Export]
	public Node3D[] patrolDestinations = new Node3D[0];

	[Export(PropertyHint.Range, "0.1,20,0.1")]
	private float speed = 2.0f;

	[Export(PropertyHint.Range, "0.1,20,0.1")]
	private float chasingSpeed = 4.0f;

	private Player player;
	private NavigationAgent3D navAgent;
	private RayCast3D chaseCast;
	private RayCast3D chaseCast2;
	private RayCast3D chaseCast3;
	private RayCast3D chaseCast4;
	private RayCast3D chaseCast5;
	private AnimationPlayer monsterAnim;

	private RandomNumberGenerator rng = new RandomNumberGenerator();

	public Node3D destination;
	public int? destinationValue = null;

	private bool chasing = false;

	private float chaseTimer = 0f;

	private float wantedSpeed;

	private bool idle = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetTree().CurrentScene.GetNode<Player>("player");
		navAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
		chaseCast = GetNode<RayCast3D>("chasecast");
		chaseCast2 = GetNode<RayCast3D>("chasecast2");
		chaseCast3 = GetNode<RayCast3D>("chasecast3");
		chaseCast4 = GetNode<RayCast3D>("chasecast4");
		chaseCast5 = GetNode<RayCast3D>("chasecast5");
		monsterAnim = GetNode<AnimationPlayer>("monster/AnimationPlayer");
		monsterAnim.Play("idle");
		monsterAnim.SpeedScale = 1;
		
		pickDestination();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (chasing)
		{
			if (chaseTimer < 15.0f)
			{
				chaseTimer += 1f * (float)delta;
			}
			else
			{
				GD.Print("Enemy aborted chasing.");
				chaseTimer = 0f;
				chasing = false;
				pickDestination();
			}
		}
		if (destination == null)
			return;

		updateTargetLocation();

	}

	public override void _PhysicsProcess(double delta)
	{
		chasePlayer(chaseCast);
		chasePlayer(chaseCast2);
		chasePlayer(chaseCast3);
		chasePlayer(chaseCast4);
		chasePlayer(chaseCast5);

		if (destination != null)
		{

			// Move enemy to target
			wantedSpeed = idle ? 0 : chasing ? chasingSpeed : speed; // when idle -> speed = 0; when chasing -> speed = 5; else 3
			var currentLocation = GlobalTransform.Origin;
			var nextLocation = navAgent.GetNextPathPosition();
			var newVelocity = (nextLocation - currentLocation).Normalized() * wantedSpeed;
			navAgent.Velocity = newVelocity;
			// Velocity = Velocity.MoveToward(newVelocity, 0.25f);
			// MoveAndSlide();


			if (!idle)
			{
				// Rotate enemy to moving direction
				var lookDir = Mathf.LerpAngle(Mathf.DegToRad(GlobalRotationDegrees.Y), Mathf.Atan2(-Velocity.X, -Velocity.Z), 0.5f);
				var tmpRotation = GlobalRotationDegrees;
				tmpRotation.Y = Mathf.RadToDeg(lookDir);
				GlobalRotationDegrees = tmpRotation;
			}

		}
	}

	public void computeVelocity(Vector3 safeVelocity)
	{
		Velocity = Velocity.MoveToward(safeVelocity, 0.25f);
		MoveAndSlide();
	}

	public void pickDestination()
	{
		GD.Print("Enemy picking next destination.");
		if (chasing)
			return;

		monsterAnim.Play("walk");
		monsterAnim.SpeedScale = 1;
		int chance = rng.RandiRange(0, patrolDestinations.Length - 1);
		destination = patrolDestinations[chance];
		if (chance == destinationValue)
			pickDestination();
		else
		{
			GD.Print("Next Enemy destination: ", chance);
			destinationValue = chance;
			idle = false;
		}
	}

	private void updateTargetLocation()
	{
		navAgent.TargetPosition = destination.GlobalTransform.Origin;
	}

	public void chasePlayer(RayCast3D chaseCast)
	{
		if (chaseCast.IsColliding() && chaseCast.GetCollider() is Node3D)
		{
			var hit = chaseCast.GetCollider() as Node3D;

			if (hit.Name == "player" && !chasing)
			{
				GD.Print("Enemy chasing player.");
				chasing = true;
				idle = false;
				destination = player;
				monsterAnim.Play("chase");
				monsterAnim.SpeedScale = 2;
			}
		}
	}

	public void stopEnemy()
	{
		GD.Print("Enemy stopped.");
		idle = true;
		monsterAnim.Play("idle");
		monsterAnim.SpeedScale = 1;
	}
}
