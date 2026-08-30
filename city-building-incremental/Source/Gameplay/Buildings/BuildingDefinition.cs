using Godot;

[GlobalClass]
public partial class BuildingDefinition : Resource
{
    [Export] public string BuildingName = "";
    [Export] public float TimerAmount = 1.0f;
    [Export] public Godot.Collections.Array<MaterialOutput> Outputs = new();
}