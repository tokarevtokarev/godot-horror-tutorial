using Godot;
using System;

public partial class PlayerUi : Control
{
	private CanvasLayer pauseMenu;
	private CanvasLayer taskUI;
	private CanvasLayer safeUi;
	private Node3D codePaper;

	private AnimationPlayer fadePlayer;

	private AnimationPlayer safeAnimationPlayer;

	
	private RandomNumberGenerator rng = new RandomNumberGenerator();

	// 4 digit random number for safe password, from 0000 to 9999, inclusive
	private string safePassword = "";
	private bool isSafeOpen = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		pauseMenu = GetNode<CanvasLayer>("pauseMenu");
		pauseMenu.Visible = false;

		safeUi = GetNode<CanvasLayer>("SafeUi");
		safeUi.Visible = false;

		taskUI = GetNode<CanvasLayer>("taskUI");
		setTask("Ring the doorbell");

		fadePlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		fadePlayer.PlayBackwards("fade");

		safeAnimationPlayer = GetTree().CurrentScene.GetNode<AnimationPlayer>("house/safe/AnimationPlayer");

		codePaper = GetTree().CurrentScene.GetNode<Node3D>("objective/CodePaper");

		generateSafePassword();
	}

	private void generateSafePassword()
	{
		var p1 = rng.RandiRange(0, 9);
		var p2 = rng.RandiRange(0, 9);
		var p3 = rng.RandiRange(0, 9);
		var p4 = rng.RandiRange(0, 9);
		safePassword = p1.ToString() + p2.ToString() + p3.ToString() + p4.ToString();

		(codePaper.GetNode<MeshInstance3D>("CodeText").Mesh as TextMesh).Text = safePassword;
		GD.Print("Safe password: " + safePassword);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause"))
		{
			if (safeUi.Visible)
			{
				exitSafe();
				return;
			}

			togglePause();
		}
	}

	private void togglePause()
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

	public void openSafePasswordUI()
	{
		if (!safeUi.Visible && !isSafeOpen)
		{
			safeUi.Visible = true;
			Input.MouseMode = Input.MouseModeEnum.Visible;
			GetTree().Paused = true;
		}
	}

	private void exitSafe()
	{
		safeUi.Visible = false;
		Input.MouseMode = Input.MouseModeEnum.Captured;
		GetTree().Paused = false;
	}

	private void confirmPassowrd()
	{
		LineEdit passwordInput = safeUi.GetNode<LineEdit>("password");
		if (passwordInput.Text == safePassword)
		{
			safeAnimationPlayer.Play("open");
			exitSafe();
			isSafeOpen = true;
		}
		else
		{
			GD.Print("Incorrect password");
			passwordInput.Text = "";
			StaticBody3D safeBody = GetTree().CurrentScene.GetNode<StaticBody3D>("house/safe/safeBody");
		}
	}
}
