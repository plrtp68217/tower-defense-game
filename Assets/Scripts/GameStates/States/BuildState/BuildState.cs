using UnityEngine;

/// <summary>
/// Состояние строительства: отображает превью объекта и обрабатывает его размещение.
/// </summary>
public sealed class BuildState : StateBase<BuildStateContext>
{
    private readonly BuildingService _buildingService;
    private readonly PreviewService _previewService;
    private readonly InputService _inputManager;

    private TowerEntityBase _currentTower;

    private readonly UIState _ui;

    public BuildState(
        StateManager stateManager
    ) : base(stateManager)
    {
        _buildingService = stateManager.BuildingService;
        _previewService = stateManager.PreviewService;
        _inputManager = stateManager.InputManager;


        _ui = stateManager.BuildUI;
    }

    public override void OnEnter()
    {
        _ui.Show();
        if (Context.Data == null) return;
       
        _currentTower = new Tower()
        {
            Prefab = Context.Data.Prefab,
            Name = Context.Data.EntityName,
            Size = Context.Data.Size,
            Health = Context.Data.MaxHealth,
            Team = Context.Data.EntityTeam
        };

        _previewService.ShowPreview(_currentTower);

        _buildingService.ShowGrid();

    }

    public override void OnExit()
    {
        _previewService.HidePreview();

        _buildingService.HideGrid();

        _ui.Hide();
    }

    public override void OnUpdate()
    {
        if (Context.Data == null) return;

        // Получаем позицию мыши на карте
        Vector3 mousePos = _inputManager.GetSelectedMapPosition();

        // Преобразуем в координаты сетки
        Vector3Int gridPos = _buildingService.WorldToCell(mousePos);

        // Обновляем позицию объекта в контексте
        _currentTower.GridPosition = gridPos;

        // Обновляем превью: позиция и цвет (в зависимости от возможности размещения)
        _previewService.UpdatePreviewPosition();
    }

    public override void OnClick()
    {
        if (_inputManager.IsPointerOverUI()) return;
        if (Context.Data == null) return;

        // Получаем позицию мыши и преобразуем в координаты сетки
        Vector3 mousePos = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = _buildingService.WorldToCell(mousePos);
        // Пытаемся разместить объект
        if (_buildingService.TryPlace(_currentTower))
        {
            _currentTower.GridPosition = gridPos;
            
            _stateManager.AudioSourceSuccess.Play();
        }
    }
}
