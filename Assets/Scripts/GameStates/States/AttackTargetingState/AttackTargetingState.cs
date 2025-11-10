using UnityEngine;

public sealed class AttackTargetingState : StateBase<AttackTargetingStateContext>
{
    private LineRenderer _lineRenderer;
    private Tower _selectedTower;
    private Vector3 _targetPosition;
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
        var lineObj = new GameObject("AttackLine");
        _lineRenderer = lineObj.AddComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.5f;
        _lineRenderer.endWidth = 0.5f;
        _lineRenderer.material = new Material(Shader.Find("Unlit/Color"))
        {
            color = Color.brown
        };
        _lineRenderer.positionCount = 2;
         
        _selectedTower = Context.SelectedTower;
    }

    public override void OnUpdate()
    {

        Vector3 mousePos    = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPos  = _buildingService.WorldToCell(mousePos);
       
        _targetPosition = _buildingService.CellToWorld(gridPos);

        _lineRenderer.SetPosition(0, _selectedTower.Center);
        _lineRenderer.SetPosition(1, _targetPosition);

        _lineRenderer.enabled = !_stateManager.InputManager.IsPointerOverUI();
    }

    public override void OnClick()
    {
        if (_inputManager.IsPointerOverUI()) return;

        Vector3 mousePos = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = _buildingService.WorldToCell(mousePos);

        var targetTower = _buildingService.GetObjectAtPosition<Tower>(gridPos);

        if (targetTower == null) return;

        bool isConnectionBlocked = _connectionService.IsConnectionBlocked(_selectedTower, targetTower);

        if (isConnectionBlocked == false)
        {
            _targetPosition = targetTower.Center;

            _lineRenderer.SetPosition(1, _targetPosition);

            _connectionService.AddConnection(_selectedTower, targetTower); // USLESS

            _selectedTower.AddTarget(targetTower);

            _stateManager.SwitchToState<IdleState, IdleStateContext>();
        }
        else
        {
            Debug.Log("Connection blocked! Cannot create connection.");
        }
    }


    public override void OnExit()
    {
        _lineRenderer = null;
    }
}
