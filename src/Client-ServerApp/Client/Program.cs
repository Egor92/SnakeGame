namespace Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var gameClient = new GameClient();
        await gameClient.RunAsync();
    }
}