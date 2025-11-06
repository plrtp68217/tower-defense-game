using UnityEngine;

public class SimpleUnit : UnitEntityBase, IUnit
{
    [field: SerializeField] public UnitData Data { get; set; }
    public Team Team { get; set; }
    public string Name { get; set; }
}
