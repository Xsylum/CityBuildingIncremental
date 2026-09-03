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
        b.ResourceOutput += AddResources;
    }

    public bool CanAfford(Godot.Collections.Array<MaterialOutput> costs)
    {
        foreach (MaterialOutput mo in costs)
        {
            if (resources[mo.resource] < mo.amount)
            {
                return false;
            }
        }

        return true;
    }

    public float GetResourceCount(MaterialType type)
    {
        return resources[type];
    }

	public void AddResources(Godot.Collections.Array<MaterialOutput> newResources)
    {
        // TODO: should probably have something so if any are negative below 0 (unless the resource allows debt) that nothing is changed? Acts as an extra safeguard
		foreach (MaterialOutput o in newResources)
        {
            resources[o.resource] += o.amount;
        }
		UpdateResourceLabels();
    }

    public void SpendResources(Godot.Collections.Array<MaterialOutput> spentResources)
    {
        foreach (MaterialOutput o in spentResources)
        {
            resources[o.resource] -= o.amount;
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
