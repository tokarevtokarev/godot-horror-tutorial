using Godot;
using System;

public partial class Paintingframe : Node3D
{
	// Export painting var: standartmatierial2d
	[Export]
	private StandardMaterial3D paintingMaterial;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		setPainting(paintingMaterial);
	}

	// Get child node named "plane" and set override material
	private void setPainting(StandardMaterial3D painting)
	{
		GetNode<MeshInstance3D>("Plane").SetSurfaceOverrideMaterial(0, painting);
	} 
}
