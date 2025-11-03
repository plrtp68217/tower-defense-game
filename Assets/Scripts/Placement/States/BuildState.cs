using UnityEngine;

public sealed class BuildState : PlacementStateBase
{
    private readonly PlacementSystemManager _context;
    private int _selectedObjectIndex;

    public BuildState(PlacementSystemManager context)
    {
        _context = context;
    }

    public override void OnEnter(int objectID = -1)
    {
        _context.ShowVisual();

        _selectedObjectIndex = _context.Database.objectsData.FindIndex(data => data.ID == objectID);

        _context.PreviewSystem.StopShowingPlacementPreview();

        _context.PreviewSystem.StartShowingPlacementPreview(
            _context.Database.objectsData[_selectedObjectIndex].Prefab,
            _context.Database.objectsData[_selectedObjectIndex].Size
        );
    }

    public override void OnUpdate()
    {
        Vector3 mousePosition = _context.InputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = _context.Grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, _selectedObjectIndex);

        _context.PreviewSystem.UpdatePosition(
            _context.Grid.CellToWorld(gridPosition),
            placementValidity
        );
    }

    public override void OnClick()
    {
        if (_context.InputManager.IsPointerOverUI())
        {
            return;
        }

        Vector3 mousePosition = _context.InputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = _context.Grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, _selectedObjectIndex);

        if (placementValidity)
        {
            _context.PlaceStructure(gridPosition, _selectedObjectIndex);
        }
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        Vector2Int objectSize = _context.Database.objectsData[selectedObjectIndex].Size;
        bool placementValidity = _context.GridData.CanPlaceObject(gridPosition, objectSize);
        return placementValidity;
    }
}
