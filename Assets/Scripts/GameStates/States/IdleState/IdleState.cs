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

        Vector3 mousePos = _inputManager.GetSelectedMapPosition();

        GameObject obj = _inputManager.GetObjectInRadius(mousePos, radius: 0.25f, LayerMask.NameToLayer(Layers.Objects));

        if (obj == null) return;

        if (obj.TryGetComponent(out Tower tower))
        {
            _stateManager.SwitchToState<AttackTargetingState, AttackTargetingStateContext>(
                new() { SelectedTower = tower }
            );

            return;
        }
        else if (obj.TryGetComponent(out Connection connection))
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
