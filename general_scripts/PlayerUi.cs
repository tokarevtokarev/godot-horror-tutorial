using Godot;
using System;

public partial class PlayerUi : Control
{
	private CanvasLayer pauseMenu;
	private CanvasLayer taskUI;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		pauseMenu = GetNode<CanvasLayer>("pauseMenu");
		taskUI = GetNode<CanvasLayer>("taskUI");
		pauseMenu.Visible = false;

		setTask("Ring the doorbell");
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause"))
		{
			pauseMenu.Visible = !pauseMenu.Visible;
			GetTree().Paused = pauseMenu.Visible;
			if (GetTree().Paused)
			{
				Input.MouseMode = Input.MouseModeEnum.Visible;
			}
			else
			{
				Input.MouseMode = Input.MouseModeEnum.Captured;
			}
		}
	}

	public void resumeGame()
	{
		pauseMenu.Visible = false;
		GetTree().Paused = false;
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public void quitGame()
	{
		GetTree().Quit();
	}

	public void setTask(String taskText)
	{
		RichTextLabel taskLabel = taskUI.GetNode<RichTextLabel>("taskText");
		taskLabel.Text = taskText;
	}
}
