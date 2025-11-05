using UnityEngine;

public class AttackTargetingStateContext : IStateContext
{
    public GameObject SelectedTower { get; private set; }

    public AttackTargetingStateContext(GameObject selectedTower)
    {
        SelectedTower = selectedTower;
    }

    public T Default<T>() where T : IStateContext, new() => new();
}
