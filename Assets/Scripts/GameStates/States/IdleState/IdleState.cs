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
        _inputManager.GetSelectedMapPosition(out GameObject selectedObject);

        if (selectedObject == null) return;
        
        if (selectedObject.TryGetComponent<LineRenderer>(out var line))
        {
            Debug.Log(line);
            _connectionService.RemoveConnectionByLine(line);
        }
    }

    public override void OnClick()
    {
        if (_inputManager.IsPointerOverUI()) return;

        Vector3 mousePos = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = _buildingService.WorldToCell(mousePos);

        var selectedObject = _buildingService.GetObjectAtPosition<Tower>(gridPos);

        if (selectedObject != null)
        {
            _stateManager.SwitchToState<AttackTargetingState, AttackTargetingStateContext>(
                new AttackTargetingStateContext() { SelectedTower = selectedObject }
            );
        }
        else
        {
            Debug.Log("object not found");
        }
    }

    public override void OnExit()
    {
        _ui.Hide();
    }
}
