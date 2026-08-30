using Godot;

[GlobalClass]
public partial class MaterialOutput : Resource
{
    [Export] public MaterialType resource;
    [Export] public float amount;

    public MaterialOutput() { }

    public MaterialOutput(MaterialType resource, float amount)
    {
        this.resource = resource;
        this.amount = amount;
    }
}