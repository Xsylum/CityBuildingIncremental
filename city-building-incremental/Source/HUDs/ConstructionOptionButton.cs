using Godot;
using System;

public partial class ConstructionOptionButton : OptionButton
{
	// TODO: this should pull from some resource of all the buildings (building scene, building_definition?)
	[Export] public Godot.Collections.Array<string> buildings;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (string item in buildings)
        {
            AddItem(item);
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


}
