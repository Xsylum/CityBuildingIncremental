using Godot;
using System;

public partial class MainScene : Node
{
	[Export] public ResourceManager resourceManager;
	[Export] public ConstructionManager constructionManager;
	[Export] public CollectionManager collectionManager;

	[Export] public GridGraph buildingGrid;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
