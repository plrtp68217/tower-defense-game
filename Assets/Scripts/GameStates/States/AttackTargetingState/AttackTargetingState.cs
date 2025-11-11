using UnityEngine;

public sealed class AttackTargetingState : StateBase<AttackTargetingStateContext>
{
    private Connection _connection;

    private readonly InputService _inputManager;
    private readonly BuildingService _buildingService;
    private readonly ConnectionService _connectionService;

    public AttackTargetingState(StateManager stateManager)
        : base(stateManager) 
    {
        _inputManager = stateManager.InputManager;
        _buildingService = stateManager.BuildingService;
        _connectionService = stateManager.ConnectionService;
    }

    public override void OnEnter()
    {
        _connection = new(Context.SelectedTower);
    }

    public override void OnUpdate()
    {
        if (_connection == null) return;
        Vector3 mousePos = _inputManager.GetSelectedMapPosition();
        _connection.MoveTo(mousePos);

        //_connection.enabled = !_stateManager.InputManager.IsPointerOverUI();
    }

    public override void OnClick()
    {
        if (_inputManager.IsPointerOverUI()) return;

        Vector3 mousePos = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = _buildingService.WorldToCell(mousePos);

        var targetTower = _buildingService.GetObjectAtPosition<Tower>(gridPos);

        bool isConnectionBlocked = _connectionService.IsConnectionBlocked(_connection.StartTower, targetTower);

        if (targetTower == null || isConnectionBlocked)
        {
            _connection.Destroy();
            _stateManager.SwitchToState<IdleState, IdleStateContext>();
        }

        _connection.SetTarget(targetTower);

        _connectionService.AddConnection(_connection);

        _stateManager.SwitchToState<IdleState, IdleStateContext>();
    }


    public override void OnExit()
    {
        if (_connection != null) _connection.Destroy();

    }
}
