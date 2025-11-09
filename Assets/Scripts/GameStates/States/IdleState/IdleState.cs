using UnityEngine;

public sealed class IdleState : StateBase<IdleStateContext>
{
    private readonly BuildingService _buildingService;
    private readonly InputService _inputManager;

    private readonly UIState _ui;

    public IdleState(StateManager stateManager)
        : base(stateManager)
    {
        _buildingService = stateManager.BuildingService;
        _inputManager = stateManager.InputManager;

        _ui = stateManager.IdleUI;
    }

    public override void OnEnter()
    {
        _ui.Show();
    }

    public override void OnUpdate()
    {
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
