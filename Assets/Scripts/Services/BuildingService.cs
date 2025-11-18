using System.Collections.Generic;
using UnityEngine;

public class BuildingService : MonoBehaviour
{
    [SerializeField] private PlacementMapService _map;

    [SerializeField] private TowerGenerator _towerGenerator;
    [field: SerializeField] public GameObject TowerPrefab { get; private set; }

    public void BuildTowers(Team team)
    {
        List<Vector3> towerPositions = _towerGenerator.GenerateTowersByPerlin(team);

        Debug.Log(towerPositions.Count);

        foreach (Vector3 position in towerPositions)
        {
            GameObject towerObject = Instantiate(TowerPrefab);
            Tower tower = towerObject.GetComponent<Tower>();

            Vector3Int gridPos = WorldToCell(position);
            
            tower.WorldPosition = CellToWorld(gridPos);
            tower.GridPosition  = WorldToCell(position);

            tower.Team = team;

            _map.PlaceObject(tower);
        }
    }

    public Vector3Int WorldToCell(Vector3 worldPosition) => _map.WorldToCell(worldPosition);
    public Vector3 CellToWorld(Vector3Int cellPosition)  => _map.CellToWorld(cellPosition);

    public bool CanPlace(TowerEntityBase pos) => _map.CanPlaceObject(pos);

    public bool TryPlace(TowerEntityBase obj)
    {
        if (obj == null) return false;

        if (_map.CanPlaceObject(obj) == false)
        {
            return false;
        }

        _map.PlaceObject(obj);

        return true;
    }

    public T GetObjectAtPosition<T>(Vector3Int position)
        where T : TowerEntityBase
    {
        return _map.GetObjectAtPosition(position) as T;
    }

    public bool RemoveObject(TowerEntityBase obj) => _map.RemoveObject(obj);

    public void ShowGrid()
    {
        _map.ShowGridVisualisation();
    }

    public void HideGrid()
    {
        _map.HideGridVisualisation();
    }
}
