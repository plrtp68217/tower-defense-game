using UnityEngine;

public sealed class AttackTargetingState : StateBase<AttackTargetingStateContext>
{
    private LineRenderer _lineRenderer;
    private Tower _selectedTower;
    private Vector3 _targetPosition;
    private readonly InputService _inputManager;
    private readonly BuildingService _buildingService;

    public AttackTargetingState(StateManager stateManager)
        : base(stateManager) 
    {
        _inputManager = stateManager.InputManager;
        _buildingService = stateManager.BuildingService;
    }

    public override void OnEnter()
    {
        var lineObj = new GameObject("AttackLine");
        _lineRenderer = lineObj.AddComponent<LineRenderer>();
        _lineRenderer.startWidth = 1f;
        _lineRenderer.endWidth = 1f;
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

        _lineRenderer.SetPosition(0, _selectedTower.WorldPosition);
        _lineRenderer.SetPosition(1, _targetPosition);

        _lineRenderer.enabled = !_stateManager.InputManager.IsPointerOverUI();
    }

    public override void OnClick()
    {
        if (_inputManager.IsPointerOverUI()) return;

        Vector3 mousePos = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = _buildingService.WorldToCell(mousePos);
        var targetTower = _buildingService.GetObjectAtPosition<Tower>(gridPos);

        if (targetTower != null)
        {

            _lineRenderer.SetPosition(1, _targetPosition);
            _stateManager.SwitchToState<IdleState, IdleStateContext>();
        }
    }


    public override void OnExit()
    {
        _lineRenderer = null;
        //if (_lineRenderer != null && _lineRenderer.gameObject != null)
        //{
        //    //Object.Destroy(_lineRenderer.gameObject);
        //}
    }

}
