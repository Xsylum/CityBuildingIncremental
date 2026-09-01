using Godot;
using System;

public partial class ConstructionManager : Node
{
	[Export] public bool startingMenuVisible = false;

	public int selectedGridIndex = -1;

	[Export] public Control buildingMenu;

	[Export] public OptionButton buildingOption;
	[Export] public Button constructButton;

	[Export] public PackedScene farmScene;
	[Export] public PackedScene quarryScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        buildingMenu.Visible = startingMenuVisible;

		constructButton.Pressed += ConstructBuilding;
    }

	public void RegisterGraphTile(GraphTile gt)
    {
        gt.OnTileClicked += SelectTile;
    }

	public void SelectTile(int index)
    {
		GD.Print("bing");
        selectedGridIndex = index;
		buildingMenu.Visible = true;
    }

	public void ConstructBuilding()
    {
        if (selectedGridIndex == -1)
			return;
		
		int optionIndex = buildingOption.Selected;

		if (optionIndex < 0)
        {
            GD.PushError("ConstructionManager no option for buliding was selecteed in ConstructBuilding()!");
			return;
        }

		string optionString = buildingOption.GetItemText(optionIndex);

		PackedScene buildingScene = null;

		switch (optionString)
        {
            case "farm":
				buildingScene = farmScene;
				break;
			case "quarry":
				buildingScene = quarryScene;
				break;
        }

		if (buildingScene != null)
        {
            GetNode<MainScene>("../..").buildingGrid.AddBuilding(selectedGridIndex, buildingScene);
			selectedGridIndex = -1;
			buildingMenu.Visible = false;
        }
		else
        {
            GD.PushError("ConstructionManager building scene was null in ConstructBuilding()!");
		}
    }
}
