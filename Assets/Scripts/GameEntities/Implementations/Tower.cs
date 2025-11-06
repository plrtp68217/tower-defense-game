using UnityEngine;

public class Tower : TowerEntityBase
{
    [field: SerializeField] public TowerData Data { get; set; }
    public Team Team { get; set; }
    public string Name { get; set; }

}