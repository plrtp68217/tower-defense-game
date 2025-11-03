using System;

public sealed class PlacementStateFactory : IPlacementStateFactory
{
    private readonly PlacementSystemManager _context;

    public PlacementStateFactory(PlacementSystemManager context)
    {
        _context = context;
    }

    public TState CreateState<TState>()
        where TState : PlacementStateBase
    {
        var stateType = typeof(TState);

        return stateType switch
        {
            Type t when t == typeof(DefaultState)
                => (TState)(PlacementStateBase)new DefaultState(_context),
            Type t when t == typeof(BuildState)
                => (TState)(PlacementStateBase)new BuildState(_context),
            _
                => throw new InvalidOperationException(
                    $"Invalid {nameof(TState)} provided: {stateType.Name}"
                )
        };
    }
}
