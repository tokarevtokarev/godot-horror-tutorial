using Godot;
using System;

public partial class fp_cam : Node3D
{
    [Export(PropertyHint.Range, "0.1,20,0.1")]
    public float sensitivity = 0.2f;

    private SpotLight3D flashlight;
    private const float MinPitchDeg = -90f;
    private const float MaxPitchDeg = 90f;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
        flashlight = GetNode<SpotLight3D>("flashlight");
    }

    public override void _Input(InputEvent @event)
    {
        // Handle mouse motion for camera rotation
        if (@event is InputEventMouseMotion mouseMotion)
        {
            if (Input.MouseMode == Input.MouseModeEnum.Captured)
                HandleCameraRotation(mouseMotion);
        }

        // Exit mouse capture on Escape key press
        if (@event is InputEventKey keyEvent && keyEvent.Keycode == Key.Escape && keyEvent.Pressed)
        {
            if (Input.MouseMode == Input.MouseModeEnum.Captured)
                Input.MouseMode = Input.MouseModeEnum.Visible;
            else
                Input.MouseMode = Input.MouseModeEnum.Captured;
        }
    }

    public override void _Process(double delta)
    {
        //check for flashlight input and call toggle flashlight function
        if (Input.IsActionJustReleased("flashlight"))
        {
            ToggleFlashLight();
        }
    }

    private void ToggleFlashLight()
    {
        GD.Print("Toggling flashlight");
        flashlight.Visible = !flashlight.Visible;
    }

    private void HandleCameraRotation(InputEventMouseMotion mouseMotion)
    {
        GetParentNode3D().RotateY(Mathf.DegToRad(-mouseMotion.Relative.X * sensitivity));
        RotateX(Mathf.DegToRad(-mouseMotion.Relative.Y * sensitivity));

        var rotation = RotationDegrees;
        rotation.X = Mathf.Clamp(rotation.X, MinPitchDeg, MaxPitchDeg);
        RotationDegrees = rotation;
    }

}
