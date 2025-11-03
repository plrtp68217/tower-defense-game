using System;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PreviewSystem _previewSystem;
    [SerializeField] private GameObject _gridVisualisation;
    [SerializeField] private Grid _grid;
    [SerializeField] private ObjectsDatabaseSO _database;
    [SerializeField] private AudioSource _audioSourceSuccess;

    public InputManager InputManager => _inputManager;
    public PreviewSystem PreviewSystem => _previewSystem;
    public Grid Grid => _grid;
    public ObjectsDatabaseSO Database => _database;
    public AudioSource AudioSourceSuccess => _audioSourceSuccess;

    public GridData GridData { get; private set; }
    public List<GameObject> PlacedGameObjects { get; private set; } = new();

    private IPlacementState _currentState;
    private DefaultState _defaultState;
    private PlacementState _placementState;

    public void SwitchToPlacementState(int ID)
    {
        _currentState?.OnExit();
        _currentState = _placementState;
        _currentState.OnEnter(ID);
    }

    public void SwitchToDefaultState()
    {
        _currentState?.OnExit();
        _currentState = _defaultState;
        _currentState.OnEnter();
    }

    public void ShowVisual()
    {
        _gridVisualisation.SetActive(true);
    }

    public void HideVisual()
    {
        _gridVisualisation.SetActive(false);
        _previewSystem.StopShowingPlacementPreview();
    }

    public void PlaceStructure(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GameObject newObject = Instantiate(_database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = _grid.CellToWorld(gridPosition);

        PlacedGameObjects.Add(newObject);

        GridData.AddObject(
            gridPosition, 
            _database.objectsData[selectedObjectIndex].Size,
            _database.objectsData[selectedObjectIndex].ID,
            PlacedGameObjects.Count - 1
        );

        AudioSourceSuccess.Play();
    }

    private void Start()
    {
        GridData = new();

        _defaultState = new DefaultState(this);
        _placementState = new PlacementState(this);

        _inputManager.OnClicked += OnClick;
        _inputManager.OnExit += OnExit;

        SwitchToDefaultState();
    }

    private void Update()
    {
        _currentState?.OnUpdate();
    }

    private void OnClick()
    {
        _currentState?.OnClick();
    }

    private void OnExit()
    {
        _currentState?.OnExit();
    }
}