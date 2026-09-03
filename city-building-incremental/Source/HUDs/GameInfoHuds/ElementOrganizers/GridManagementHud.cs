using Godot;
using System;
using System.Linq;

public partial class GridManagementHud : MarginContainer
{
	[Export] public Label selectedTileLabel;

	[Export] public Control constructionMenu;
	[Export] public OptionButton constructionBuildingOption;
	[Export] public Button constructButton;
	[Export] public Label constructCostLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void FormatCostLabel(Godot.Collections.Array<MaterialOutput> costs)
    {
		string labelText = "";
		if (costs == null)
        {
            labelText = "";
        }
		else if (costs.Count == 0)
        {
            labelText = "FREE!"; 
        }
		else
        {
            foreach (MaterialOutput mo in costs.Take(costs.Count - 1))
			{
				labelText += $"{mo.resource}: {mo.amount}\n";
			}
			var lastElem = costs[costs.Count - 1];
			labelText += $"{lastElem.resource}: {lastElem.amount}";
        }
        
		constructCostLabel.Text = labelText;
    }
}
