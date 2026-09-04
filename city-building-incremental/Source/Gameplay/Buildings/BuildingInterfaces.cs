using Godot;
using System;

public interface IWorkerAssignable
{
    void AssignWorker(Worker w);
	void UnassignWorker(Worker w);
}

public interface IUpgradable
{
    int CurrentUpgradeLevel { get; }
	bool TryUpgrade();
}

public interface IProductionSelectable
{
    void SetProductionTarget(MaterialType type);
}

public interface IProductionMultipliable
{
    void SetProductionMultiplier(float multiplier);
}

public interface IBuildingComponent
{
    void Initialize(Building building);
}
