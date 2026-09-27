using Godot;
using System;

public partial class Lamp : GenericLightObject, InteractableObject
{
	[Export]
	private AudioStreamPlayer3D toggleSound;

	public void PlayerInteract()
	{
		ToggleLight(!isOn);
		toggleSound.Play();
	}
}
