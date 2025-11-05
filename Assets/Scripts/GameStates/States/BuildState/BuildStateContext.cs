public sealed class BuildStateContext : IStateContext
{
    public IPlacable Object { get; set; }
    public T Default<T>() where T : IStateContext, new() => new();
    
}
