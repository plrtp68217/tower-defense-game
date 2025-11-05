public sealed class IdleStateContext : IStateContext
{
    public T Default<T>() where T : IStateContext, new() => new();
}
