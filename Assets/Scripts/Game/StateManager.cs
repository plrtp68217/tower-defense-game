using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public sealed class StateManager : MonoBehaviour
{
    [SerializeField]
    private InputManager _inputManager;

    [SerializeField]
    private PreviewSystem _previewSystem;

    [SerializeField]
    private GameObject _gridVisualisation;

    [SerializeField]
    private Grid _grid;

    [SerializeField]
    private ObjectsDatabaseSO _database;

    [SerializeField]
    private AudioSource _audioSourceSuccess;

    public InputManager InputManager => _inputManager;
    public PreviewSystem PreviewSystem => _previewSystem;
    public Grid Grid => _grid;
    public ObjectsDatabaseSO Database => _database;
    public AudioSource AudioSourceSuccess => _audioSourceSuccess;

    public GridData GridData { get; private set; }
    public IList<GameObject> PlacedGameObjects { get; } = new List<GameObject>();

    public IObjectFactory ObjectFactory { get; private set; }

    private IStateFactory _stateFactory;
    private IState _currentState;

    public void SwitchToState<TState, TContext>(TContext context)
        where TState : StateBase<TContext>
        where TContext : IStateContext
    {
        _currentState?.OnExit();
        _currentState = _stateFactory.CreateState<TState, TContext>(context);
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

    private void Start()
    {
        GridData = new();

        ObjectFactory = gameObject.AddComponent<ObjectFactory>();

        _stateFactory = new StateFactory(this);

        _inputManager.OnClicked += OnClick;
        _inputManager.OnExit += OnExit;

        SwitchToState<IdleState, IdleStateContext>(new IdleStateContext());
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
