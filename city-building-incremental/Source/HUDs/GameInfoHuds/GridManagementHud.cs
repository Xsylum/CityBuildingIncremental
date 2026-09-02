using Godot;
using System;

public partial class GridManagementHud : MarginContainer
{
	[Export] public Label selectedTileLabel;

	[Export] public Control constructionMenu;
	[Export] public OptionButton constructionBuildingOption;
	[Export] public Button constructButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
