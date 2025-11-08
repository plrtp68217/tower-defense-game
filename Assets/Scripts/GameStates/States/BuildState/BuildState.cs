using UnityEngine;

/// <summary>
/// Состояние строительства: отображает превью объекта и обрабатывает его размещение.
/// </summary>
public sealed class BuildState : StateBase<BuildStateContext>
{
    private readonly BuildingService _buildingService;
    private readonly InputService _inputManager;

    private Tower _currentTower;

    private readonly UIState _ui;

    public BuildState(
        StateManager stateManager
    ) : base(stateManager)
    {
        _buildingService    = stateManager.BuildingService;
        _inputManager       = stateManager.InputManager;


        _ui = stateManager.BuildUI;
    }

    public override void OnEnter()
    {
        _ui.Show();
        if (Context.Data == null) return;

        _currentTower = Tower.FromData(Context.Data);

        Vector3 mousePos    = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPos  = _buildingService.WorldToCell(mousePos);
        _currentTower.WorldPosition = _buildingService.CellToWorld(gridPos);
        _currentTower.GridPosition  = gridPos;

        _currentTower.SetPreviewMaterial();
        _buildingService.ShowGrid();

    }

    public override void OnExit()
    {
        _buildingService.HideGrid();
        _currentTower?.Dispose();

        _ui.Hide();
    }

    public override void OnUpdate()
    {
        if (Context.Data == null) return;

        Vector3 mousePos    = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPos  = _buildingService.WorldToCell(mousePos);
        _currentTower.WorldPosition = _buildingService.CellToWorld(gridPos);
        _currentTower.GridPosition  = gridPos;

        bool isPlaceFree = _buildingService.CanPlace(_currentTower);
        _currentTower.SetPreviewMaterial();
        _currentTower.SetPreviewValidity(isPlaceFree);
    }

    public override void OnClick()
    {
        if (_inputManager.IsPointerOverUI()) return;

        bool towerIsPlaced = _buildingService.TryPlace(_currentTower);

        if (towerIsPlaced)
        {
            _currentTower.RestoreMaterial();
            _currentTower = Tower.FromData(Context.Data);

            Vector3 mousePos    = _inputManager.GetSelectedMapPosition();
            Vector3Int gridPos = _buildingService.WorldToCell(mousePos);
            _currentTower.WorldPosition = _buildingService.CellToWorld(gridPos);
            _currentTower.GridPosition = gridPos;
            
            _stateManager.AudioSourceSuccess.Play();
        }
    }
}
