using UnityEngine;

public sealed class BuildState : StateBase<BuildStateContext>
{
    public BuildState(StateManager stateManager)
        : base(stateManager) { }

    public override void OnEnter()
    {   
        if (Context.Object == null)
        {
            return;
        }

        _stateManager.ShowVisual();

        var placableObj = Context.Object;

        _stateManager.PreviewSystem.StopShowingPlacementPreview();

        _stateManager.PreviewSystem.StartShowingPlacementPreview(
            placableObj.Prefab,
            placableObj.Size
        );
    }

    public override void OnExit()
    {
        _stateManager.PreviewSystem.StopShowingPlacementPreview();
    }

    public override void OnUpdate()
    {
        if (Context.Object == null)
        {
            return;
        }

        Vector3 mousePosition = _stateManager.InputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = _stateManager.Grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition);

        _stateManager.PreviewSystem.UpdatePosition(
            _stateManager.Grid.CellToWorld(gridPosition),
            placementValidity
        );
    }

    public override void OnClick()
    {
        if (_stateManager.InputManager.IsPointerOverUI())
        {
            return;
        }

        Vector3 mousePosition = _stateManager.InputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = _stateManager.Grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition);

        if (placementValidity)
        {
            PlaceStructure(gridPosition, Context.Object);
        }
    }

    public void PlaceStructure(Vector3Int gridPosition, IPlacable obj)
    {
        GameObject newObject = _stateManager.ObjectFactory.Instantiate(obj.Prefab);
        newObject.transform.position = _stateManager.Grid.CellToWorld(gridPosition);

        _stateManager.PlacedGameObjects.Add(newObject);

        _stateManager.GridData.AddObject(gridPosition, obj.Size, _stateManager.PlacedGameObjects.Count - 1);

        _stateManager.AudioSourceSuccess.Play();
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition)
    {
        if (Context.Object == null)
        {
            return false;
        }

        Vector2Int objectSize = Context.Object.Size;
        bool placementValidity = _stateManager.GridData.CanPlaceObject(gridPosition, objectSize);
        return placementValidity;
    }
}
