public interface IStateFactory
{
    TState CreateState<TState, TContext>(TContext context)
        where TState : StateBase<TContext>
        where TContext : IStateContext;
}
