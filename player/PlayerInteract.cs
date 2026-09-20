using Godot;
using System;

public partial class PlayerInteract : RayCast3D
{

    public CenterContainer crosshair;
    public PlayerUi playerUi;
    private string[] interactableNames = { "doorbody",
    "lightswitchbody",
    "lampbody",
    "drawerSlot1Body",
    "drawerSlot2Body",
    "closetDoor1Body",
    "closetDoor2Body",
    "doorBellBody",
    "safeBody" };

    public override void _Ready()
    {
        playerUi = GetParent().GetParent().GetNode<PlayerUi>("player_ui");
        crosshair = playerUi.GetNode<CenterContainer>("CanvasLayer/crosshair");
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

        if (hit.Name == "safeBody")
        {
            playerUi.openSafePasswordUI();
            return;
        }

        handleInteractableObject(hit);
    }

    private void handleInteractableObject(Node3D hit)
    {
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
