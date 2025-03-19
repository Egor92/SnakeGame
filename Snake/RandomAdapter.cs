namespace Snake;

public static class RandomAdapter
{
    private static readonly Random _random = new Random();

    public static int Next(int min, int max)
    {
        return _random.Next(min, max);
    }

    public static int Next(int max)
    {
        return _random.Next(max);
    }
}