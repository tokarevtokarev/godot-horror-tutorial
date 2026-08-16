using Godot;
using System;

public partial class Lightbulb : Node3D
{
	[Export]
	private bool isOn = true;

	[Export]
	private StandardMaterial3D onMaterial;

	[Export]
	private StandardMaterial3D offMaterial;

	[Export]
	private MeshInstance3D lightBulb;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		lightBulb.SetSurfaceOverrideMaterial(0, isOn ? onMaterial : offMaterial);
		GetNode<OmniLight3D>("OmniLight3D").Visible = isOn;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ToggleLight(bool state)
	{
		GD.Print("Toggling light to " + state);
		isOn = state;
		lightBulb.MaterialOverride = isOn ? onMaterial : offMaterial;
		GetNode<OmniLight3D>("OmniLight3D").Visible = isOn;
	}
}
