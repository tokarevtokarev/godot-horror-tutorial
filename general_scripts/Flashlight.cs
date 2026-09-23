using Godot;
using System;

public partial class Flashlight : SpotLight3D
{
	[Export (PropertyHint.Range, "0.1,20,0.1")]
	private float rotationSpeed = 5f;


	private fp_cam playerCam;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerCam = GetTree().CurrentScene.GetNode<fp_cam>("player/head");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//check for flashlight input and call toggle flashlight function
        if (Input.IsActionJustReleased("flashlight"))
        {
            ToggleFlashLight();
        }

		// set flashlight position to match the camera position
		Transform3D transform = GlobalTransform;
		transform.Origin = playerCam.GlobalTransform.Origin;
		GlobalTransform = transform;
	}

	public override void _PhysicsProcess(double delta)
	{
		// lerp flashlight rotation to match the camera rotation
		float rotY = Mathf.LerpAngle(GlobalRotation.Y, playerCam.GlobalRotation.Y, rotationSpeed * (float)delta);
		float rotX = Mathf.LerpAngle(GlobalRotation.X, playerCam.GlobalRotation.X, rotationSpeed * (float)delta);
		float rotZ = Mathf.LerpAngle(GlobalRotation.Z, playerCam.GlobalRotation.Z, rotationSpeed * (float)delta);
		GlobalRotation = new Vector3(rotX, rotY, rotZ);
	}

	private void ToggleFlashLight()
    {
        GD.Print("Toggling flashlight");
        Visible = !Visible;
    }
}
