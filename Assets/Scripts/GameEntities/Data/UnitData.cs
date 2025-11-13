using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Data/Unit Data", order = 1)]
public class UnitData : ScriptableObject
{
    [Header("Основные параметры")]
    public string Name;
    public Team Team = Team.Blue;


    [Header("Характеристики")]
    public float MoveSpeed = 5f;
    public int Health = 1;
    public int Damage = 1;

    [Header("Дополнительные параметры")]
    public Vector2Int Size;

    public UnitData Clone()
    {
        UnitData clone = CreateInstance<UnitData>();
        clone.Name = Name;
        clone.Team = Team;
        clone.MoveSpeed = MoveSpeed;
        clone.Health = Health;
        clone.Damage = Damage;
        clone.Size = Size;
        return clone;
    }
}
