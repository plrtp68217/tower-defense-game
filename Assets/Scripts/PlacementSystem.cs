using System;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject _mouseIndicator, _cellIndicator;

    [SerializeField]
    private InputManager _inputManager;

    [SerializeField]
    private Grid _grid;

    [SerializeField]
    private ObjectsDatabaseSO _database;

    [SerializeField]
    private GameObject _gridVisualisation;

    private int _selectedObjectIndex = -1;

    public void StartPlacement(int ID)
    {
        StopPlacement();

        _selectedObjectIndex = _database.objectsData.FindIndex(data => data.ID == ID);

        if (_selectedObjectIndex < 0)
        {
            Debug.Log($"Идентификатор {ID} не найден.");
            return;
        }

        _gridVisualisation.SetActive(true);
        _cellIndicator.SetActive(true);

        _inputManager.OnClicked += PlaceStructure;
        _inputManager.OnExit += StopPlacement;
    }

    public void StopPlacement()
    {
        _selectedObjectIndex = -1;

        _gridVisualisation.SetActive(false);
        _cellIndicator.SetActive(false);

        _inputManager.OnClicked -= PlaceStructure;
        _inputManager.OnExit -= StopPlacement;
    }

    private void PlaceStructure()
    {
        if (_inputManager.IsPointerOverUI())
        {
            return;
        }

        Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

        GameObject newObject = Instantiate(_database.objectsData[_selectedObjectIndex].Prefab);
        newObject.transform.position = _grid.CellToWorld(gridPosition);

        StopPlacement();
    }

    private void Start()
    {
        StopPlacement();
    }

    private void Update()
    {   
        if ( _selectedObjectIndex < 0 )
        {
            return;
        }

        Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

        _mouseIndicator.transform.position = mousePosition;
        _cellIndicator.transform.position = _grid.CellToWorld(gridPosition);
    }
}
