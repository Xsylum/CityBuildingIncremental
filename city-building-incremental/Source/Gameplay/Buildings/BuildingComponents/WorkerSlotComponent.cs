using Godot;
using System;
using System.Collections.Generic;

public partial class WorkerSlotComponent : Node, IWorkerAssignable
{
	[Export] public int HardCapWorkers = 6; // this will always act as a HARD cap. Soft caps should go into WorkerEffect
	[Export] public WorkerEffect Effect;	// To differentiate how workers affect farms, quarries, etc.: create a concrete subclass of WorkerEffect

	private List<Worker> _assigned = new();

	public void AssignWorker(Worker w)
    {
        if (_assigned.Count >= HardCapWorkers)
        {
            GD.Print("Worker slots full.");
			return;
        }

		_assigned.Add(w);
		w.AssignedTo = this;
		Effect.Apply(GetParent<Building>(), _assigned.Count);
    }

	public void UnassignWorker(Worker w)
    {
        if (_assigned.Remove(w))
        {
            w.AssignedTo = null;
			Effect.Apply(GetParent<Building>(), _assigned.Count);
        }
    }
}
