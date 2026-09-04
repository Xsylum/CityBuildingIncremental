using Godot;
using System;

public abstract partial class WorkerEffect : Resource
{
    public abstract void Apply(Building building, int workerCount);
}
