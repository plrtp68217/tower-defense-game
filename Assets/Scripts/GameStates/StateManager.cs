using UnityEngine;

public sealed class StateManager : MonoBehaviour
{
    [field: SerializeField] public InputService InputManager { get; private set; }
    [field: SerializeField] public BuildingService BuildingService { get; private set; }
    [field: SerializeField] public ConnectionService ConnectionService { get; private set; }
    [field: SerializeField] public EnemySpawner EnemySpawner { get; private set; }

    [field: SerializeField] public UIState IdleUI { get; private set; }
    [field: SerializeField] public UIState BuildUI { get; private set; }
    [field: SerializeField] public UIState FightUI { get; private set; }

    [field: SerializeField] public AudioSource AudioSourceSuccess { get; private set; }

    private IStateFactory _stateFactory;
    private IState _currentState;

    public void SwitchToState<TState, TContext>(TContext context = default)
        where TState : StateBase<TContext>
        where TContext : IStateContext , new()
    {
        _currentState?.OnExit();
        _currentState = _stateFactory.CreateState<TState, TContext>(context ?? new TContext().Default<TContext>());
        _currentState.OnEnter();
    }


    private void Start()
    {
        _stateFactory = new StateFactory(this);

        InputManager.OnClicked += OnClick;

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
