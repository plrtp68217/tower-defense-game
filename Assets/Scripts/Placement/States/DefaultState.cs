public sealed class DefaultState : PlacementStateBase
{
    private PlacementSystemManager _context;

    public DefaultState(PlacementSystemManager context)
    {
        _context = context;
    }

    public override void OnEnter(int objectID = -1)
    {
        _context.HideVisual();
    }
}
