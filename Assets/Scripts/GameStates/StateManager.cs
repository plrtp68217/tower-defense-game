using UnityEngine;

public sealed class StateManager : MonoBehaviour
{
    [field: SerializeField] public InputService InputManager { get; private set; }
    [field: SerializeField] public BuildingService BuildingService { get; private set; }
    [field: SerializeField] public PreviewService PreviewService { get; private set; }

    [field: SerializeField] public AudioSource AudioSourceSuccess { get; private set; }
    [field: SerializeField] public GameObject GridVisualisation { get; private set; }

    private IStateFactory _stateFactory;
    private IState _currentState;

    public System.Action<System.Type> OnStateChanged;

    public void SwitchToState<TState, TContext>(TContext context = default)
        where TState : StateBase<TContext>
        where TContext : IStateContext , new()
    {
        _currentState?.OnExit();
        _currentState = _stateFactory.CreateState<TState, TContext>(context ?? new TContext().Default<TContext>());
        _currentState.OnEnter();

        OnStateChanged?.Invoke(typeof(TState));
    }

    public void ShowVisual()
    {
        GridVisualisation.SetActive(true);
    }

    public void HideVisual()
    {
        GridVisualisation.SetActive(false);
    }

    private void Start()
    {
        _stateFactory = new StateFactory(this);

        InputManager.OnClicked += OnClick;
        InputManager.OnExit += OnExit;

        SwitchToState<IdleState, IdleStateContext>();
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
