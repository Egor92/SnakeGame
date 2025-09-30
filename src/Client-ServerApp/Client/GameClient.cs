using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace Client;

public class GameClient
{
    private readonly ClientWebSocket _webSocket = new();
    private bool _gameIsRunning = true;

    public async Task RunAsync()
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = Encoding.UTF8;
        try
        {
            await StartGameAsync();
            await ConnectAsync();

            var inputTask = Task.Run(ChangeDirectionAsync);
            var receiveTask = GameStateAsync();

            await receiveTask;
            _gameIsRunning = false;
            await inputTask;

            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Disconnected", CancellationToken.None);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private async Task StartGameAsync()
    {
        var httpClient = new HttpClient();
        await httpClient.PostAsync("http://localhost:5033/start", null);
        Console.WriteLine("Игра началась!");
    }

    private async Task ConnectAsync()
    {
        var uri = new Uri("ws://localhost:5033/websockets");
        await _webSocket.ConnectAsync(uri, CancellationToken.None);
        Console.WriteLine("Вы подключились к серверу!");
    }

    private async Task ChangeDirectionAsync()
    {
        while (_gameIsRunning)
        {
            var key = Console.ReadKey(true);
            string? direction = key.Key switch
            {
                ConsoleKey.UpArrow => "Up",
                ConsoleKey.DownArrow => "Down",
                ConsoleKey.LeftArrow => "Left",
                ConsoleKey.RightArrow => "Right",
                _ => null
            };

            if (direction != null)
            {
                await SendNewDirectionAsync(direction);
            }
        }
    }

    private async Task SendNewDirectionAsync(string? direction)
    {
        var httpClient = new HttpClient();
        var content = new StringContent($"\"{direction}\"", Encoding.UTF8, "application/json");
        await httpClient.PostAsync("http://localhost:5033/turn", content);
    }

    private async Task GameStateAsync()
    {
        var buffer = new byte[8192];
        var gameRenderer = new GameRenderer();

        while (_gameIsRunning && _webSocket.State == WebSocketState.Open)
        {
            try
            {
                var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    break;
                }

                var json = Encoding.UTF8.GetString(buffer, 0, result.Count);
                var gameElement = JsonSerializer.Deserialize<GameElements>(json);

                if (gameElement is { IsGameOver: true })
                {
                    _gameIsRunning = false;
                }

                if (gameElement != null)
                {
                    GameSceneCreator gameSceneCreator = new();
                    var scene = gameSceneCreator.GetSceneCellObjects(gameElement);
                    gameRenderer.RenderGame(scene, gameElement.Width, gameElement.Height, gameElement.StepCount, gameElement.PointCount,
                        gameElement.IsGameOver);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
                break;
            }
        }
    }
}