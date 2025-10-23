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
    public static void OnGameOver() => Console.WriteLine("Игра окончена! Нажмите Enter");
    public static void OnError(Exception exception) => Console.WriteLine($"Произошла ошибка. {exception}");

    public static string? NewGame()
    {
        Console.WriteLine("Хотите начать игру? \n1/ Введите Y, если да \n2/ Введите всё что угодно, если нет ");
        return Console.ReadLine();
    }

    public static void GoodBye() => Console.WriteLine("До новых встреч!");
}