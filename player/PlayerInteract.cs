using Godot;
using System;

public partial class PlayerInteract : RayCast3D
{
	private string[] interactableNames = { "doorbody", "lightswitchbody" };

    public override void _PhysicsProcess(double delta)
    {
        if (!Input.IsActionJustPressed("interact"))
            return;

        if (!IsColliding())
            return;

        Node3D hit = GetCollider() as Node3D;
        if (hit == null)
            return;

        if (!Array.Exists(interactableNames, element => element == hit.Name))
            return;

        // Search for InteractableObject script in parent nodes
        Node currentNode = hit;
        while (currentNode != null)
        {
            if (currentNode is InteractableObject interactable)
            {
                interactable.PlayerInteract();
                break;
            }
            currentNode = currentNode.GetParent();
        }
    }

}
