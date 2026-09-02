using Godot;
using System;
using System.Collections.Generic;

public partial class ResourceManager : Node
{
	Dictionary<MaterialType, float> resources = new();

	[Export] public LeftSideHud Hud;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        foreach (MaterialType mr in (MaterialType[]) Enum.GetValues(typeof(MaterialType))) // https://stackoverflow.com/a/105402
        {
            resources.Add(mr, 0);
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void RegisterNewBuilding(Building b)
    {
        b.ResourceOutput += ChangeResources;
    }

	public void ChangeResources(Godot.Collections.Array<MaterialOutput> newResources)
    {
		foreach (MaterialOutput o in newResources)
        {
            resources[o.resource] += o.amount;
        }
		UpdateResourceLabels();
    }

	public void UpdateResourceLabels()
    {
        foreach (KeyValuePair<MaterialType, float> resourcePair in resources)
        {
            Hud.UpdateResourceLabel(resourcePair.Key, resourcePair.Value);
        }
    }
}
