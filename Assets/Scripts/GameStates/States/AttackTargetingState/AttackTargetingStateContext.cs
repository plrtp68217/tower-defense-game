using UnityEngine;

public class AttackTargetingStateContext : IStateContext
{
    public Tower SelectedTower { get; set; }

    public T Default<T>() where T : IStateContext, new() => new();
}
