using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Server.Contracts;

namespace Client;

public class GameClient(string? url)
{
    private readonly ClientWebSocket _webSocket = new();
    private bool _gameIsRunning = true;
    private readonly GameRenderer _gameRenderer = new();
    private readonly GameSceneCreator _gameSceneCreator = new();
    private string? _clientId;

    public async Task RunAsync()
    {
        try
        {
            await ConnectAsync();
            Program.OnConnected();

            _clientId = await GetClientIdFromServerAsync();
            await StartGameAsync(_clientId);
            Program.OnGameStart();

            var inputTask = Task.Run(ChangeDirectionAsync);
            var receiveTask = GameStateAsync();

            await receiveTask;
            _gameIsRunning = false;
            await inputTask;

            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Disconnected", CancellationToken.None);
        }
        catch (Exception)
        {
            Program.OnError();
        }
    }
    
    private async Task StartGameAsync(string clientId)
    {
        using var httpClient = new HttpClient();
        await httpClient.PostAsync($"{url}/start/{clientId}", null);
    }

    private async Task ConnectAsync()
    {
        string newUrl = url.Replace("http://", "ws://");
        var uri = new Uri($"{newUrl}/websockets");
        await _webSocket.ConnectAsync(uri, CancellationToken.None);
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
        await httpClient.PostAsync($"{url}/turn/{_clientId}", content);
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
                    }

                    var scene = _gameSceneCreator.GetSceneCellObjects(gameElement);
                    _gameRenderer.RenderGame(scene, gameElement.Width, gameElement.Height, gameElement.StepCount, gameElement.PointCount,
                        gameElement.IsGameOver);
                }
            }
            catch (Exception)
            {
                Program.OnError();
                break;
            }
        }
    }
}