using System;

public sealed class StateFactory : IStateFactory
{
    private readonly StateManager _stateManager;

    public StateFactory(StateManager stateManager)
    {
        _stateManager = stateManager;
    }

    // INFO: Универсальный метод но с боксингом
    // public TState CreateState<TState, TContext>(TContext context)
    //     where TState : StateBase<TContext>
    //     where TContext : IContext

    // {
    //     /*
    //      *  WARNING (@perf): Происходит боксинг объекта
    //      *  TCurrentState -> object -> TState (CreateInstance -> return object)
    //      */
    //     var state = (TState)Activator.CreateInstance(typeof(TState), _stateManager)!;
    //     state.SetContext(context);
    //     return state;
    // }

    /*
     *  INFO: Тут нет боксинга, но приходится расширят
     */
    public TState CreateState<TState, TContext>(TContext context)
        where TState : StateBase<TContext>
        where TContext : IStateContext
    {
        StateBase<TContext> state =
            typeof(TState) switch
            {
                Type t when t == typeof(IdleState)
                    => new IdleState(_stateManager) as StateBase<TContext>,

                Type t when t == typeof(BuildState)
                    => new BuildState(_stateManager) as StateBase<TContext>,

                Type t when t == typeof(AttackTargetingState)
                    => new AttackTargetingState(_stateManager) as StateBase<TContext>,

                Type t when t == typeof(FightState)
                => new FightState(_stateManager) as StateBase<TContext>,

                _
                    => throw new InvalidOperationException(
                        $"Unsupported state type: {typeof(TState).Name}"
                    )
            }
            ?? throw new InvalidOperationException(
                $"Failed to create state '{typeof(TState).Name}' — context type '{typeof(TContext).Name}' may be incompatible."
            );
        state.SetContext(context);
        return (TState)state;
    }
}
