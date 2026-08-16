using Godot;
using System;

public partial class Lightswitch : Node3D, InteractableObject
{
	[Export]
	public Boolean isOn = false;

	[Export]
	public Node3D OnNode;

	[Export]
	public Node3D OffNode;

	[Export]
	private Lightbulb[] lightbulbs = new Lightbulb[0];

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		OnNode.Visible = isOn;
		OffNode.Visible = !isOn;
		ToggleLights();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void ToggleSwitch()
	{
		isOn = !isOn;
		OnNode.Visible = isOn;
		OffNode.Visible = !isOn;
	}

	private void ToggleLights()
	{
		foreach (Lightbulb lightbulb in lightbulbs)
		{
			lightbulb.ToggleLight(isOn);
		}
	}

	public void PlayerInteract()
	{
		ToggleSwitch();
		ToggleLights();
	}
}
