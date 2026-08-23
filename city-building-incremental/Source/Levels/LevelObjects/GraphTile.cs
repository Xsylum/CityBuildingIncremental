using Godot;
using System;

public partial class GraphTile : Area2D
{
	public int GridIndex;

	[Export] public bool isDarkColour = false;
	[Export] public Color lightColour = Color.Color8(10, 255, 40);
	[Export] public Color darkColour = Color.Color8(0, 165, 20);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (isDarkColour)
		{
			Modulate = darkColour;
		}
		else
		{
			Modulate = lightColour;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{
				GD.Print($"Area2D clicked with left mouse button! {this.Name} {this.GridIndex}");
				GetViewport().SetInputAsHandled();
			}
		}
	}

}
