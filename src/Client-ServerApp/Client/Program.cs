using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

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

        var gameSettings = config.GetSection("GameSettings").Get<GameSettings>();
        var options = Options.Create(gameSettings);
        var gameClient = new GameClient(options);
        await gameClient.RunAsync();
    }

    public static void OnGameOver() => Console.WriteLine("Игра окончена! Нажмите Enter");
    public static void OnGameStart() => Console.WriteLine("Игра началась!");
    public static void OnConnected() => Console.WriteLine("Вы подключились к серверу!");
    
    private static readonly GameStatistics _statistics = new();
    public static void AddGameResult(int points, int steps) => _statistics.AddResult(points, steps);
    public static void ShowStatistics()
    {
        var gameResults = _statistics.GetAllResults();
        if (gameResults.Count == 0)
        {
            Console.WriteLine("Вы не сыграли ни одной игры.");
            return;
        }

        Console.WriteLine("\n=== Статистика ===");
        for (int i = 0; i < gameResults.Count; i++)
        {
            var (points, steps) = gameResults[i];
            Console.WriteLine($"Игра {i + 1}: {points} очков, {steps} ходов");
        }

        int bestIndex = 0;
        for (int i = 1; i < gameResults.Count; i++)
        {
            if (gameResults[i].Points > gameResults[bestIndex].Points)
                bestIndex = i;
        }

        var best = _statistics.GetBestGame();
        if (best.HasValue)
        {
            Console.WriteLine("=============");
            var (bestPoints, bestSteps) = best.Value;
            Console.WriteLine($"\nЛучшая игра {bestIndex + 1}: {bestPoints} очков, {bestSteps} ходов");
            Console.WriteLine("=============");
        }
    }

    public static void OnError(Exception exception) => Console.WriteLine($"Произошла ошибка. {exception}");

    public static string? NewGame()
    {
        Console.WriteLine("Хотите начать игру?");
        string? answer;
        do
        {
            Console.WriteLine("1/ Введите 1, если да \n2/ Введите 2, если нет ");
            answer = Console.ReadLine();
        } while (answer != "1" && answer != "2");

        return answer;
    }

    public static void GoodBye() => Console.WriteLine("До новых встреч!");
}