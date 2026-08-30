using Godot;
using System;

public partial class Building : Node2D
{
	[Export] public BuildingDefinition Definition;
	private BuildingDefinition _runtimeDefinition;

	[Export] public Timer ResourceTimer;

	[Signal]
	public delegate void ResourceOutputEventHandler(Godot.Collections.Array<MaterialOutput> resourceDelta);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		(GetTree().CurrentScene as MainScene).resourceManager.RegisterNewBuilding(this);

		_runtimeDefinition = (BuildingDefinition)Definition.Duplicate(true);

		ResourceTimer.WaitTime = _runtimeDefinition.TimerAmount;

		ResourceTimer.Start();
		ResourceTimer.Timeout += OnResourceTimeOut;
	}

	// TODO: can create methods like "ApplyProductionMultiplier" to increase the _runtimeDefinition values

	public void OnResourceTimeOut()
	{
		GD.Print("dong");
		EmitSignal(SignalName.ResourceOutput, _runtimeDefinition.Outputs);
	}
}
