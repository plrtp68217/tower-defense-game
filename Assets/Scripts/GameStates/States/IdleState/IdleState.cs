using UnityEngine;

public sealed class IdleState : StateBase<IdleStateContext>
{
    private readonly BuildingService _buildingService;
    private readonly PreviewService _previewService;
    private readonly InputService _inputManager;

    private readonly UIState _ui;

    public IdleState(StateManager stateManager)
        : base(stateManager)
    {
        _buildingService = stateManager.BuildingService;
        _previewService = stateManager.PreviewService;
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
        // Получаем позицию мыши на карте
        Vector3 mousePos = _inputManager.GetSelectedMapPosition();

        // Преобразуем в координаты сетки
        Vector3Int gridPos = _buildingService.WorldToCell(mousePos);

        var selectedObject = _buildingService.GetObjectAtPosition<Tower>(gridPos);

        // Переходим в состояние AttackTargetingState и передаем в контекст башню, от которой будем строить путь
        if (selectedObject != null)
        {
            _stateManager.SwitchToState<AttackTargetingState, AttackTargetingStateContext>(
                new AttackTargetingStateContext() { SelectedTower = selectedObject }
            );
        }
    }

    public override void OnExit()
    {
        _ui.Hide();
    }
}
