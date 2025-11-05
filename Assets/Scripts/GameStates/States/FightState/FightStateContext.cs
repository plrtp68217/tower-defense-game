public sealed class FightStateContext : IStateContext
{
    public T Default<T>() where T : IStateContext, new() => new();
}