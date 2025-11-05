public interface IStateWithContext<TContext> : IState
    where TContext : IStateContext
{
    void SetContext(TContext context);
}
