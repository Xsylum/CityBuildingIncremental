using Godot;
using System;

public partial class ConstructionManager : Node
{
	public int selectedGridIndex = -1;

	[Export] public TopLevelHud Hud;

	[Export] public PackedScene farmScene;
	[Export] public PackedScene quarryScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        Hud.gridManagementHud.Hide();

		Hud.gridManagementHud.constructButton.Pressed += ConstructBuilding;
    }

	public void RegisterGraphTile(GraphTile gt)
    {
        gt.OnTileClicked += SelectTile;
    }

	public void SelectTile(int index)
    {
		var coords = GetNode<MainScene>("../..").buildingGrid.GetCoordinatesByIndex(index);
		Hud.gridManagementHud.selectedTileLabel.Text = $"({coords.Item1}, {coords.Item2})"; 
        selectedGridIndex = index;
		Hud.gridManagementHud.Show();
    }

	public void ConstructBuilding()
    {
        if (selectedGridIndex == -1)
			return;
		
		int optionIndex = Hud.gridManagementHud.constructionBuildingOption.Selected;

		if (optionIndex < 0)
        {
            GD.PushError("ConstructionManager no option for buliding was selecteed in ConstructBuilding()!");
			return;
        }

		string optionString =  Hud.gridManagementHud.constructionBuildingOption.GetItemText(optionIndex);

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
			Hud.gridManagementHud.selectedTileLabel.Text = "";
			Hud.gridManagementHud.Hide();
        }
		else
        {
            GD.PushError("ConstructionManager building scene was null in ConstructBuilding()!");
		}
    }
}
