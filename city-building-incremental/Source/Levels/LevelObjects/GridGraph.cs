using Godot;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public partial class GridGraph : Node2D
{
	public Building[] buildingList; //TODO: list of all "built" Buildings
	public GraphTile[] tileList;

	[Export] public PackedScene graphTileScene;
	[Export] public PackedScene buildingScene;

	[Export] public int height;
	[Export] public int width;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        BuildGraph(width, height);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void BuildGraph(int width, int height)
    {
		buildingList = new Building[width * height];
		tileList = new GraphTile[width * height];

		var tempTile = graphTileScene.Instantiate<GraphTile>();
		var tileOffset = ((RectangleShape2D)tempTile.GetNode<CollisionShape2D>("CollisionShape2D").Shape).Size.X;
		tempTile.QueueFree();

		var tileRoot = GetNode<Node2D>("Tiles");

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
				var gt = graphTileScene.Instantiate<GraphTile>();
				gt.GridIndex = i * width + j;
				gt.isDarkColour = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);

				tileList[i * width + j] = gt;
				buildingList[i * width + j] = null;

				(GetTree().CurrentScene as MainScene).constructionManager.RegisterGraphTile(gt);

				var transform = new Vector2((j + 0.5f) * tileOffset, (i + 0.5f) * tileOffset);
				gt.Position = transform;
				tileRoot.AddChild(gt);
            }
        }
    }

	public GraphTile GetTile(int x, int y)
    {
		return tileList[y * width + x];
    }

	public GraphTile GetTile(int index)
    {
        return tileList[index];
    }

	public Building GetTileBuilding(int x, int y)
    {
        return buildingList[y * width + x];
    }

	public Building GetTileBuilding(int index)
    {
        return buildingList[index];
    }

	// public void OnTileClicked(int index)
    // {
    //     var b = buildingScene.Instantiate<Building>();
	// 	buildingList[index] = b;

	// 	// b.outputs[0].amount = index * 2 + 1;
	// 	// b.TimerAmount = 4 + index * 1.4f;

	// 	b.Position = GetTile(index).Position;

	// 	GetNode("Buildings").AddChild(b);
    // }

	public void AddBuilding(int index, PackedScene building)
    {
        var b = building.Instantiate<Building>();
		buildingList[index] = b;

		b.Position = GetTile(index).Position;

		GetNode("Buildings").AddChild(b);
    }
}
