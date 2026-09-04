using Godot;
using System;

public partial class CropSelectorComponent : Node, IProductionSelectable
{
	[Export] public Godot.Collections.Array<MaterialType> AvailableCrops;
	public void SetProductionTarget(MaterialType type)
    {
        
    }
}
