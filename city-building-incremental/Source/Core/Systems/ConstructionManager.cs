using Godot;
using System;

public partial class ConstructionManager : Node
{
	public int selectedGridIndex = -1;

	[Export] public LeftSideHud Hud;

	[Export] public PackedScene farmScene;
	[Export] public PackedScene quarryScene;

	[Export] public BuildingDefinition farmDefinition;
	[Export] public BuildingDefinition quarryDefinition;

	[Export] public ResourceManager resourceManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        Hud.gridManagementHud.Hide();

		Hud.gridManagementHud.constructButton.Pressed += ConstructBuilding;
		Hud.gridManagementHud.constructionBuildingOption.ItemSelected += ChangeDisplayedCosts;
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

	public void ChangeDisplayedCosts(long index)
    {
        string option = Hud.gridManagementHud.constructionBuildingOption.GetItemText((int)index);

		BuildingDefinition def = option switch
        {
            "farm" => farmDefinition,
			"quarry" => quarryDefinition,
			_ => null
        };

		Hud.gridManagementHud.FormatCostLabel(def?.constructionCosts);
    }

	public void ConstructBuilding()
    {
        if (selectedGridIndex == -1)	// TODO: grid index > max index ==> out of bounds on GetTileBuilding or AddBuilding
			return;
		
		int optionIndex = Hud.gridManagementHud.constructionBuildingOption.Selected;

		var buildingGrid = GetNode<MainScene>("../..").buildingGrid;

		if (buildingGrid.GetTileBuilding(selectedGridIndex) != null)
        {
			GD.PushError("ConstructionManager trying to build in occupied tile in ConstructBuilding()");
			return;
        }

		if (optionIndex < 0)
        {
            GD.PushError("ConstructionManager no option for buliding was selected in ConstructBuilding()!");
			return;
        } 

		string optionString = Hud.gridManagementHud.constructionBuildingOption.GetItemText(optionIndex);

		PackedScene buildingScene = optionString switch
        {
            "farm" => farmScene,
			"quarry" => quarryScene,
			_ => null
		};

		BuildingDefinition def = optionString switch
        {
            "farm" => farmDefinition,
			"quarry" => quarryDefinition,
			_ => null
        };

		if (def == null || buildingScene == null)
        {
            GD.PushError("Construction Manager building scene or definition was null in ConstructBuilding()!");
        }

		if (!resourceManager.CanAfford(def.constructionCosts))
        {
			GD.Print("Not enough resources to construct this building.");
			return;
            
        }

		resourceManager.SpendResources(def.constructionCosts);
		buildingGrid.AddBuilding(selectedGridIndex, buildingScene);

		selectedGridIndex = -1;
		Hud.gridManagementHud.selectedTileLabel.Text = "";
		Hud.gridManagementHud.Hide();
    }
}
