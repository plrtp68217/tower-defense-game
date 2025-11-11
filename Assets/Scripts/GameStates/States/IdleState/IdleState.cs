using UnityEngine;

public sealed class IdleState : StateBase<IdleStateContext>
{
    private readonly BuildingService _buildingService;
    private readonly InputService _inputManager;
    private readonly ConnectionService _connectionService;

    private readonly UIState _ui;

    public IdleState(StateManager stateManager)
        : base(stateManager)
    {
        _buildingService = stateManager.BuildingService;
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

        Vector3 mousePos = _inputManager.GetSelectedMapPosition(out GameObject selectedObject);
        Vector3Int gridPos = _buildingService.WorldToCell(mousePos);
        
        Tower tower = _buildingService.GetObjectAtPosition<Tower>(gridPos);

        if (tower != null)
        {
            _stateManager.SwitchToState<AttackTargetingState, AttackTargetingStateContext>(
                new AttackTargetingStateContext() { SelectedTower = tower }
            );
        }
        else if (selectedObject == null)
        {
            return;
        }
        else if (selectedObject.TryGetComponent<LineRenderer>(out var line))
        {
            _connectionService.RemoveConnectionByLine(line);
        }
    }

    public override void OnExit()
    {
        _ui.Hide();
    }
}
