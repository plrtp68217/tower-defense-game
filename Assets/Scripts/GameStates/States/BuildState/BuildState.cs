using UnityEngine;

/// <summary>
/// Состояние строительства: отображает превью объекта и обрабатывает его размещение.
/// </summary>
public sealed class BuildState : StateBase<BuildStateContext>
{
    private readonly BuildingService _buildingService;
    private readonly PreviewService _previewService;
    private readonly InputService _inputManager;

    public BuildState(
        StateManager stateManager
    ) : base(stateManager)
    {
        _buildingService = stateManager.BuildingService;
        _previewService = stateManager.PreviewService;
        _inputManager = stateManager.InputManager;
    }

    public override void OnEnter()
    {
        if (Context.Object == null) return;

        _previewService.ShowPreview(Context.Object);
    }

    public override void OnExit()
    {
        _previewService.HidePreview();
    }

    public override void OnUpdate()
    {
        if (Context.Object == null) return;

        // Получаем позицию мыши на карте
        Vector3 mousePos = _inputManager.GetSelectedMapPosition();

        // Преобразуем в координаты сетки
        Vector3Int gridPos = _buildingService.WorldToCell(mousePos);

        // Обновляем позицию объекта в контексте
        if (Context.Object is IPlacable placable)
        {
            placable.Position = gridPos;
        }

        // Обновляем превью: позиция и цвет (в зависимости от возможности размещения)
        _previewService.UpdatePreviewPosition();
    }

    public override void OnClick()
    {
        if (_inputManager.IsPointerOverUI()) return;

        // Получаем позицию мыши и преобразуем в координаты сетки
        Vector3 mousePos = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = _buildingService.WorldToCell(mousePos);

        // Пытаемся разместить объект
        if (_buildingService.TryPlace(Context.Object))
        {
            if (Context.Object is IPlacable placable)
            {
                placable.Position = gridPos;
            }

            _stateManager.AudioSourceSuccess.Play();
        }
    }
}
