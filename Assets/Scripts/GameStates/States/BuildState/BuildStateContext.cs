public sealed class BuildStateContext : IStateContext
{
    public TowerData Data { get; set; }
    public T Default<T>() where T : IStateContext, new() => new();
}
