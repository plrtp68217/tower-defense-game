using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Data/Unit Data", order = 1)]
public class UnitData : ScriptableObject
{
    [Header("Основные параметры")]
    public string Name;
    public Vector2Int Size;


    [Header("Характеристики")]
    public float MoveSpeed = 5f;
    public int Health = 1;
    public float Damage = 1;

    [Header("Дополнительные параметры")]
    public Team EntityTeam = Team.Blue;
}
