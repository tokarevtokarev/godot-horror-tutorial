using Godot;
using System;

public partial class GenericLightObject : Node3D
{
    [Export]
    protected bool isOn = true;

    [Export]
    private StandardMaterial3D onMaterial;

    [Export]
    private StandardMaterial3D offMaterial;

    [Export]
    private MeshInstance3D lightBulb;

    [Export]
    private Node3D lightNode;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (lightBulb != null)
        {
            lightBulb.MaterialOverride = isOn ? onMaterial : offMaterial;
        }
        lightNode.Visible = isOn;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void ToggleLight(bool state)
    {
        GD.Print("Toggling light to " + state);
        isOn = state;
        if (lightBulb != null)
        {
            lightBulb.MaterialOverride = isOn ? onMaterial : offMaterial;
        }
        lightNode.Visible = isOn;
    }
}
