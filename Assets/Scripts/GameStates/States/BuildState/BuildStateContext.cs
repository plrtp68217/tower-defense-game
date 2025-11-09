using UnityEngine;

public sealed class BuildStateContext : IStateContext
{
    public GameObject Prefab { get; set; }

    public T Default<T>() where T : IStateContext, new() => new();
}