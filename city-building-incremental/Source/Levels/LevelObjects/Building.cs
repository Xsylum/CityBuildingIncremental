using Godot;
using System;

public partial class Building : Node2D
{
	[Export] public Timer resourceTimer;
	[Export] public float timerAmount = 1.0f;
	[Export] public float resourceChange;

	[Signal]
	public delegate void ResourceOutputEventHandler(float resourceDelta);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		(Owner as MainScene).resourceManager.RegisterNewBuilding(this);

		resourceTimer.WaitTime = timerAmount;

		resourceTimer.Start();
		resourceTimer.Timeout += OnResourceTimeOut;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnResourceTimeOut()
	{
		GD.Print("dong");
		EmitSignal(SignalName.ResourceOutput, resourceChange);
	}
}
