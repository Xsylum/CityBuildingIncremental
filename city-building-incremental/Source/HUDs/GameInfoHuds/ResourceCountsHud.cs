using Godot;
using System;

public partial class ResourceCountsHud : MarginContainer
{
	[Export] public Label GoldValueLabel;
	[Export] public Label WoodValueLabel;
	[Export] public Label StoneValueLabel;
	[Export] public Label FoodValueLabel;
	[Export] public Label ResearchValueLabel;
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
        switch(label)
        {
            case MaterialType.Gold:
					GoldValueLabel.Text = amount.ToString();
					break;
				case MaterialType.Wood:
					WoodValueLabel.Text = amount.ToString();
					break;
				case MaterialType.Stone:
					StoneValueLabel.Text = amount.ToString();
					break;
				case MaterialType.Food:
					FoodValueLabel.Text = amount.ToString();
					break;
				case MaterialType.Research:
					ResearchValueLabel.Text = amount.ToString();
					break;
		}
	}
}
