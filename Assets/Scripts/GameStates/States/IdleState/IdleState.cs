using UnityEngine;

public sealed class IdleState : StateBase<IdleStateContext>
{
    private readonly InputService _inputManager;
    private readonly ConnectionService _connectionService;

    private readonly UIState _ui;

    public IdleState(StateManager stateManager)
        : base(stateManager)
    {
        _inputManager = stateManager.InputManager;
        _connectionService = stateManager.ConnectionService;

        _ui = stateManager.IdleUI;
    }

    public override void OnEnter()
    {
        _ui.Show();
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnPressed()
    {
        if (_inputManager.IsPointerOverUI()) return;

        _inputManager.GetObjectInMap(LayerMask.NameToLayer(Layers.Objects), out GameObject gameObject);


        if (gameObject == null) return;


        if (gameObject.TryGetComponent(out Tower tower))
        {
            _stateManager.SwitchToState<AttackTargetingState, AttackTargetingStateContext>(
                new() { SelectedTower = tower }
            );

            return;
        }
        else if (gameObject.TryGetComponent(out Connection connection))
        {
            _connectionService.RemoveConnection(connection);

            return;
        }
    }

    public override void OnExit()
    {
        _ui.Hide();
    }
}
