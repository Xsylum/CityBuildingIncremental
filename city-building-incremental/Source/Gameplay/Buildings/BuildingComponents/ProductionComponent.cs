using Godot;
using System;

public partial class ProductionComponent : Node, IBuildingComponent
{
	[Export] public Timer ResourceTimer;
	[Export] public ProgressBar OutputProgressBar;

	private Building _building;
	private float _multiplier = 1f;

	public void Initialize(Building building)
    {
        _building = building;
		ResourceTimer.WaitTime = _building.RuntimeDefinition.TimerAmount;
		ResourceTimer.Start();
		ResourceTimer.Timeout += OnResourceTimeOut;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_building == null) return;
		OutputProgressBar.Value = (1 - ResourceTimer.TimeLeft / ResourceTimer.WaitTime) * 100;
	}

	public void SetProductionMultiplier(float multiplier)
    {
        _multiplier = multiplier;
    }

	private void OnResourceTimeOut()
    {
        var scaledOutputs = new Godot.Collections.Array<MaterialOutput>();
		foreach (var output in _building.RuntimeDefinition.Outputs)
        {
            var scaled = (MaterialOutput)output.Duplicate();
			scaled.amount = Mathf.CeilToInt(output.amount * _multiplier);
			scaledOutputs.Add(scaled);
        }

		_building.EmitResourceOutput(scaledOutputs);
    }
}
