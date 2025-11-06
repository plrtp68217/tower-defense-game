using UnityEngine;

public class AttackTargetingStateContext : IStateContext
{
    public IPlacable SelectedTower { get; set; }

    public T Default<T>() where T : IStateContext, new() => new();
}
