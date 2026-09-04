using Godot;
using System;

public partial class ProductionMultiplierEffect : WorkerEffect
{
	[Export] public int SoftWorkerCap = 3;
	[Export] public float MultiplierPerWorker = 0.5f;
	[Export] public float DiminishedMultiplierPerWorker = 0.1f;

	public override void Apply(Building building, int workerCount)
    {
        int normalWorkers = Mathf.Min(workerCount, SoftWorkerCap);
		int excessWorkers = Mathf.Max(0, workerCount - SoftWorkerCap);
		float multiplier = 1f + (normalWorkers * MultiplierPerWorker) + (excessWorkers * DiminishedMultiplierPerWorker);

		var producer = building.GetComponent<IProductionMultipliable>();
		producer?.SetProductionMultiplier(multiplier);
    }
}
