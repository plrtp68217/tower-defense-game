public abstract class StateBase<TContext> : IStateWithContext<TContext>
    where TContext : IStateContext
{
    protected TContext Context { get; private set; }
    protected StateManager _stateManager;

    public StateBase(StateManager stateManager)
    {
        _stateManager = stateManager;
    }

    public void SetContext(TContext context)
    {
        Context = context;
    }

    public virtual void OnEnter() { }

    public virtual void OnUpdate() { }

    public virtual void OnClick() { }

    public virtual void OnPressed() { }

    public virtual void OnExit() { }
}
