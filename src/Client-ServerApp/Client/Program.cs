using Client;
using Server.Contracts;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = Encoding.UTF8;

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var url = config["Server:Url"];

        var gameClient = new GameClient(url);
        await gameClient.RunAsync();
    }

    public static void OnGameStart() => Console.WriteLine("Игра началась!");
    public static void OnConnected() => Console.WriteLine("Вы подключились к серверу!");
    public static void OnGameOver() => Console.WriteLine("Игра окончена!");
    public static void OnError() => Console.WriteLine("Произошла ошибка.");
}