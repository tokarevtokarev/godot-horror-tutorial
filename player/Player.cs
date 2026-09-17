using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	private CollisionShape3D collisionShape;

	bool crouching = false;

	public override void _Ready()
	{
		collisionShape = GetNode<CollisionShape3D>("CollisionShape3D");
	}

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("crouch"))
		{
			crouching = !crouching;
			Speed = crouching ? 2f : 5.0f;
		}
    }


	public override void _PhysicsProcess(double delta)
	{
		handleCrouch();
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		Vector2 inputDir = Input.GetVector("left", "right", "foreward", "backwards");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void handleCrouch()
	{
		if (crouching && ((CapsuleShape3D)collisionShape.Shape).Height > 0.25)
		{
			// lerp crouch height
			float couchHeight = Mathf.Lerp(((CapsuleShape3D)collisionShape.Shape).Height, 0.25f, 1f);
			((CapsuleShape3D)collisionShape.Shape).Height = couchHeight;
		}
		else if (!crouching && ((CapsuleShape3D)collisionShape.Shape).Height < 2.0)
		{
			// lerp stand height
			float standHeight = Mathf.Lerp(((CapsuleShape3D)collisionShape.Shape).Height, 2.0f, 0.2f);
			((CapsuleShape3D)collisionShape.Shape).Height = standHeight;
		}
	}
}
