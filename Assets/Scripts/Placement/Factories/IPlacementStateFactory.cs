public interface IPlacementStateFactory
{
    TState CreateState<TState>()
        where TState : PlacementStateBase;
}
