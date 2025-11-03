using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PreviewSystem _previewSystem;
    [SerializeField] private GameObject _gridVisualisation;
    [SerializeField] private Grid _grid;
    [SerializeField] private ObjectsDatabaseSO _database;
    [SerializeField] private AudioSource _audioSourceSuccess;

    // ÂÐÅÌÅÍÍÎ ÄËß ÏÐÎÂÅÐÊÈ ÐÀÁÎÒÛ SwitchToState ×ÅÐÅÇ ÔÀÁÐÈÊÓ
    [SerializeField] private Button _building1Button;
    [SerializeField] private Button _building2Button;
    [SerializeField] private Button _building3Button;
    [SerializeField] private Button _escapeButton;

    public InputManager InputManager => _inputManager;
    public PreviewSystem PreviewSystem => _previewSystem;
    public Grid Grid => _grid;
    public ObjectsDatabaseSO Database => _database;
    public AudioSource AudioSourceSuccess => _audioSourceSuccess;

    public GridData GridData { get; private set; }
    public List<GameObject> PlacedGameObjects { get; private set; } = new();

    private IPlacementStateFactory _stateFactory;
    private IPlacementState _currentState;

    public void SwitchToState(PlacementStateType stateType, int objectID = -1)
    {
        _currentState?.OnExit();
        _currentState = _stateFactory.CreateState(stateType);
        _currentState.OnEnter(objectID);
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

    private void Awake()
    {
        _escapeButton.onClick.AddListener(() => SwitchToState(PlacementStateType.Default));
        _building1Button.onClick.AddListener(() => SwitchToState(PlacementStateType.Build, 0));
        _building2Button.onClick.AddListener(() => SwitchToState(PlacementStateType.Build, 1));
        _building3Button.onClick.AddListener(() => SwitchToState(PlacementStateType.Build, 2));
    }

    private void Start()
    {
        GridData = new();

        _stateFactory = new PlacementStateFactory(this);

        _inputManager.OnClicked += OnClick;
        _inputManager.OnExit += OnExit;

        SwitchToState(PlacementStateType.Default);
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