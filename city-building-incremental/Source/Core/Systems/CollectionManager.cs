using Godot;
using System;
using System.Collections.Generic;

public partial class CollectionManager : Node
{
	public class CollectionModifier
    {
        public float Value;
		public float? ExpiresAt;
    }

	[Export] public ResourceManager resourceManager;

	private Dictionary<Button, Action> _handlers = new();

	public Dictionary<MaterialType, float> BaseCollectionButtonAmounts = new();

	public Dictionary<MaterialType, List<CollectionModifier>> ActiveCollectionModifiers = new(); // These are temporary, timed bonuses; permanent bonuses are added to Base Collection Amounts

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (MaterialType mr in (MaterialType[]) Enum.GetValues(typeof(MaterialType))) {// https://stackoverflow.com/a/105402
			BaseCollectionButtonAmounts[mr] = 1.0f;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void RegisterCollectionButton(Button button, MaterialType resourceType)
    {
		Action handler = () => CollectResource(resourceType);
		_handlers[button] = handler;	// so if we ever remove the button, we can remove the action as well
		button.Pressed += handler;
    }

	public void CollectResource(MaterialType resourceType)
    {
		var baseAmount = BaseCollectionButtonAmounts[resourceType];
		var multiplier = 1.0f;
		
		if (ActiveCollectionModifiers.ContainsKey(resourceType))
        {
            foreach (var modifier in ActiveCollectionModifiers[resourceType])
			{
				multiplier += modifier.Value;
			}
        }
		
		var collectionOutput = new MaterialOutput(resourceType, baseAmount * multiplier);
		resourceManager.ChangeResources([collectionOutput]);
    }
}
