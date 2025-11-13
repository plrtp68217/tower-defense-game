using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Data/Unit Data", order = 1)]
public class UnitData : ScriptableObject
{
    [Header("Общие")]
    public string Name;
    public float MoveSpeed = 5f;
    public Vector2Int Size;

    [Header("Частные")]
    public Team Team = Team.Blue;
    public int Health = 1;
    public int Damage = 1;
}
