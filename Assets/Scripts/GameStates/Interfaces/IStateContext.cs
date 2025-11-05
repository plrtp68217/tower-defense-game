public interface IStateContext
{
    T Default<T>() where T : IStateContext , new();
}
