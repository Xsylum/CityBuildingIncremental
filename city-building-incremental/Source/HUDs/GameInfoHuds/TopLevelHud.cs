using Godot;
using System;

public partial class TopLevelHud : AspectRatioContainer
{
	[Export] public ResourceCountsHud resourceCountsHud;
	[Export] public GridManagementHud gridManagementHud;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateResourceLabel(MaterialType label, float amount)
    {
        resourceCountsHud.UpdateResourceLabel(label, amount);
    }
}
