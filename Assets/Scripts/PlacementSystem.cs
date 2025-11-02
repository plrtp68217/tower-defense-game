using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject _mouseIndicator;

    [SerializeField]
    private InputManager _inputManager;

    [SerializeField]
    private Grid _grid;

    [SerializeField]
    private ObjectsDatabaseSO _database;

    [SerializeField]
    private GameObject _gridVisualisation;

    [SerializeField]
    private AudioSource _audioSourceSuccess;

    private int _selectedObjectIndex = -1;

    private GridData _gridData;

    private List<GameObject> _placedGameObjects = new();

    [SerializeField]
    private PreviewSystem _previewSystem;

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

        _previewSystem.StartShowingPlacementPreview(
            _database.objectsData[_selectedObjectIndex].Prefab,
            _database.objectsData[_selectedObjectIndex].Size
            );

        _inputManager.OnClicked += PlaceStructure;
        _inputManager.OnExit += StopPlacement;
    }

    public void StopPlacement()
    {
        _selectedObjectIndex = -1;

        _gridVisualisation.SetActive(false);

        _previewSystem.StopShowingPlacementPreview();

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

        bool placementValidity = CheckPlacementValidity(gridPosition, _selectedObjectIndex);

        if (placementValidity == false)
        {
            return;
        }

        GameObject newObject = Instantiate(_database.objectsData[_selectedObjectIndex].Prefab);
        newObject.transform.position = _grid.CellToWorld(gridPosition);

        _placedGameObjects.Add(newObject);

        _gridData.AddObject(
            gridPosition, 
            _database.objectsData[_selectedObjectIndex].Size,
            _database.objectsData[_selectedObjectIndex].ID,
            _placedGameObjects.Count - 1
            );

        _audioSourceSuccess.Play();

        //StopPlacement();
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        Vector2Int objectSize = _database.objectsData[selectedObjectIndex].Size;
        bool placementValidity = _gridData.CanPlaceObject(gridPosition, objectSize);
        return placementValidity;
    }

    private void Start()
    {
        StopPlacement();

        _mouseIndicator.SetActive(false);

        _gridData = new();
    }

    private void Update()
    {   
        if ( _selectedObjectIndex < 0 )
        {
            return;
        }

        Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, _selectedObjectIndex);

        _previewSystem.UpdatePosition(_grid.CellToWorld(gridPosition), placementValidity);

        _mouseIndicator.transform.position = mousePosition;
    }
}
