using System;

public static class RandomUtils
{
    private readonly static Random _random = new();

    public static int GetRandomValue(int minValue, int maxValue)
    {
        return _random.Next(minValue, maxValue + 1);
    }
}
