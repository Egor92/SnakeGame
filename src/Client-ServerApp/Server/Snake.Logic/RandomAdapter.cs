namespace client_serverApp.Snake.Logic;

public static class RandomAdapter
{
    private static readonly Random _random = new ();

    public static int Next(int min, int max)
    {
        return _random.Next(min, max);
    }

    public static int Next(int max)
    {
        return _random.Next(max);
    }
}