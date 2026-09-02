using Godot;
using System;
using System.Collections.ObjectModel;

public partial class ResourceCollectionButtons : PanelContainer
{
	[Export] Godot.Collections.Array<CollectionButton> collectionButtons;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		var collectionManager = (GetTree().CurrentScene as MainScene).collectionManager;

        foreach (var button in collectionButtons)
        {
            collectionManager.RegisterCollectionButton(button, button.matType);
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
