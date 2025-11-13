using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Server.Contracts;
using Microsoft.Extensions.Options;

namespace Client;

public class GameClient
{
    private GameSettings _settings;
    private ClientWebSocket? _webSocket;
    private bool _gameIsRunning = true;
    private GameRenderer _gameRenderer;
    private readonly GameSceneCreator _gameSceneCreator = new();
    private string? _clientId;

    public GameClient(IOptions<GameSettings> options)
    {
        _settings = options.Value;
    }

    private string BaseUrl => $"http://{_settings.Host}:{_settings.Port}";
    private string WebSocketUrl => $"ws://{_settings.Host}:{_settings.Port}/websockets";

    public async Task RunAsync()
    {
        try
        {
            while (true)
            {
                Console.Clear();
                var answer = Program.NewGame();
                if (answer == "1")
                {
                    await StartNewGameAsync();
                }
                else
                {
                    Program.ShowStatistics();
                    Program.GoodBye();
                    await CloseWebSocketAsync();
                    return;
                }
            }
        }
        catch (Exception exception)
        {
            Program.OnError(exception);
        }
    }

    private async Task StartServerGameAsync(string clientId)
    {
        using var httpClient = new HttpClient();
        await httpClient.PostAsync($"{BaseUrl}/start/{clientId}", null);
    }

    private async Task ConnectAsync()
    {
        var uri = new Uri(WebSocketUrl);
        await _webSocket.ConnectAsync(uri, CancellationToken.None);
    }

    private async Task StartNewGameAsync()
    {
        _gameIsRunning = true;
        _gameRenderer = new GameRenderer();
        _webSocket = new ClientWebSocket();

        await ConnectAsync();
        Program.OnConnected();

        _clientId = await GetClientIdFromServerAsync();
        await StartServerGameAsync(_clientId);
        Program.OnGameStart();

        var inputTask = Task.Run(ChangeDirectionAsync);
        var receiveTask = GameStateAsync();

        await receiveTask;
        _gameIsRunning = false;
        await inputTask;
    }

    private async Task<string> GetClientIdFromServerAsync()
    {
        var buffer = new byte[8192];
        while (_webSocket.State == WebSocketState.Open)
        {
            var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("clientId", out var idProp))
            {
                return idProp.GetString()!;
            }
        }

        throw new Exception("Не удалось получить clientId от сервера.");
    }

    private async Task ChangeDirectionAsync()
    {
        while (_gameIsRunning)
        {
            var key = Console.ReadKey(true);
            DirectionDto? direction = key.Key switch
            {
                ConsoleKey.UpArrow => DirectionDto.Up,
                ConsoleKey.DownArrow => DirectionDto.Down,
                ConsoleKey.LeftArrow => DirectionDto.Left,
                ConsoleKey.RightArrow => DirectionDto.Right,
                _ => null
            };

            if (direction != null)
            {
                await SendNewDirectionAsync(direction.Value);
            }
        }
    }

    private async Task SendNewDirectionAsync(DirectionDto direction)
    {
        using var httpClient = new HttpClient();
        var json = JsonSerializer.Serialize(direction);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await httpClient.PostAsync($"{BaseUrl}/turn/{_clientId}", content);
    }

    private async Task GameStateAsync()
    {
        var buffer = new byte[8192];
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
                var gameElement = JsonSerializer.Deserialize<GameElementsDto>(json);

                if (gameElement != null)
                {
                    if (gameElement.IsGameOver)
                    {
                        _gameIsRunning = false;
                        Program.OnGameOver();
                        Program.AddGameResult(gameElement.PointCount, gameElement.StepCount);
                    }

                    var scene = _gameSceneCreator.GetSceneCellObjects(gameElement);
                    _gameRenderer.RenderGame(scene, gameElement.Width, gameElement.Height, gameElement.StepCount, gameElement.PointCount,
                        gameElement.IsGameOver);
                }
            }
            catch (Exception e)
            {
                Program.OnError(e);
                break;
            }
        }
    }

    private async Task CloseWebSocketAsync()
    {
        if (_webSocket is { State: WebSocketState.Open })
        {
            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
        }
    }
}