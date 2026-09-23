using Godot;
using System;

public partial class fov : Camera3D
{
	[Export]
	private int zoomFov = 30;

	[Export]
	private int normalFov = 75;

	// exported zoom speed slider
	[Export(PropertyHint.Range, "0.1,20,0.1")]
	private float zoomSpeed = 5f;

	private float targetFov;
	private bool zooming = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		zooming = Input.IsActionPressed("zoom");
		targetFov = zooming ? zoomFov : normalFov;
			
	}

    public override void _PhysicsProcess(double delta)
    {
        if (Fov != targetFov)
		{
			Fov = Mathf.Lerp(Fov, targetFov, zoomSpeed * (float)delta);
		}
    }

}
