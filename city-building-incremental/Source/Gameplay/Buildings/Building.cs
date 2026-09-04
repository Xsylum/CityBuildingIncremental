using Godot;
using System;
using System.Collections.Generic;

public partial class Building : Node2D
{

	[Export] public BuildingDefinition Definition;
	private BuildingDefinition _runtimeDefinition;
	public BuildingDefinition RuntimeDefinition => _runtimeDefinition;

	[Signal]
	public delegate void ResourceOutputEventHandler(Godot.Collections.Array<MaterialOutput> resourceDelta);

	public T GetComponent<T>() where T : class
    {
        foreach (Node child in GetChildren())
        {
            if (child is T match)
				return match;
        }
		return null;
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		(GetTree().CurrentScene as MainScene).resourceManager.RegisterNewBuilding(this);

		_runtimeDefinition = (BuildingDefinition)Definition.Duplicate(true);

		foreach(Node child in GetChildren())
        {
            if (child is IBuildingComponent component) {
				component.Initialize(this);
			}
        }
	}

	public void EmitResourceOutput(Godot.Collections.Array<MaterialOutput> outputs)
    {
        EmitSignal(SignalName.ResourceOutput, outputs);
    }
}
