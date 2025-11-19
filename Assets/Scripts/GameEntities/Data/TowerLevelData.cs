using UnityEngine;

[CreateAssetMenu(fileName = "TowerLevelData", menuName = "Data/TowerLevelData")]
public class TowerLevelData : ScriptableObject
{
    [Header("3D Данные")]
    public Mesh towerMesh;
    public Material towerMaterial;

    [Header("Харатеристики")]
    public int UnitsPerLevel = 10;
    public float SpawnInterval = 1f;
    public bool IsMaxLevel = false;
}