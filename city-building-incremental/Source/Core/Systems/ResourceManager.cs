using Godot;
using System;

public partial class ResourceManager : Node
{
	[Export] public float Gold = 0.0f;
	[Export] public Label GoldValueLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void RegisterNewBuilding(Building b)
    {
        b.ResourceOutput += ChangeGold;
    }

	public void ChangeGold(float delta)
    {
		GD.Print("ding");
        Gold += delta;
		GoldValueLabel.Text = Gold.ToString();
    }
}
