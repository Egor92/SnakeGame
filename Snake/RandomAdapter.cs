namespace Snake;

public abstract class RandomAdapter
{
    private static readonly Random _random;

    public static int Next(int min, int max)
    {
        return _random.Next(min, max);
    }

    public static int Next(int max)
    {
        return _random.Next(max);
    }
}