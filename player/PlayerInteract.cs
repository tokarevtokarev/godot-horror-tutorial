using Godot;
using System;

public partial class PlayerInteract : RayCast3D
{

    public CenterContainer crosshair;
	private string[] interactableNames = { "doorbody", "lightswitchbody", "lampbody", "drawerSlot1Body", "drawerSlot2Body", "closetDoor1Body", "closetDoor2Body" };

    public override void _Ready()
    {
        crosshair = GetParent().GetParent().GetNode<CenterContainer>("player_ui/CanvasLayer/crosshair");
    }

    public override void _PhysicsProcess(double delta)
    {
        crosshair.Visible = false;
        if (!IsColliding())
            return;

        Node3D hit = GetCollider() as Node3D;
        if (hit == null)
            return;

        if (!Array.Exists(interactableNames, element => element == hit.Name))
            return;

        // Here the palyer is looking at an interactable object
        // Change crosshair visibility
        crosshair.Visible = true;

        if (!Input.IsActionJustPressed("interact"))
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
