using Godot;
using System;

public partial class Destination : Node3D
{

	private RandomNumberGenerator rng = new RandomNumberGenerator();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public async void EnterTrigger(Node3D body)
	{
		if (body is not Enemy)
			return;

		Enemy enemy = body as Enemy;
		if (enemy.destination == this)
		{
			GD.Print("Enemy arrived at destination.");
			enemy.stopEnemy();
			int secondsToWait = rng.RandiRange(0, 10);
			await ToSignal(GetTree().CreateTimer(secondsToWait, false), SceneTreeTimer.SignalName.Timeout);
			GD.Print("Enemy picking next destination.");
			enemy.pickDestination();
		}
	}
}
