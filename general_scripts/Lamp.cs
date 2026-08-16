using Godot;
using System;

public partial class Lamp : GenericLightObject, InteractableObject
{
	public void PlayerInteract()
	{
		ToggleLight(!isOn);
	}
}
