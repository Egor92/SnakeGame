using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Timers;
using client_serverApp.Snake.Logic;
using Timer = System.Timers.Timer;

namespace client_serverApp;

public class GameService
{
    private readonly ConcurrentDictionary<string, WebSocket> _clients = new();
    private readonly GameLogic _gameLogic;
    private readonly GameData _gameData;
    private readonly Timer _gameTimer;
    private bool _gameIsRunning;

    public GameService()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();

        int timeBetweenSteps = Convert.ToInt32(config["GameSettings:TimeBetweenSteps"]);

        _gameData = GameDataBuilder.Create()
            .SetPlayingFieldSize(width: 45, height: 15)
            .CreateWallAroundPlayingField(width: 45, height: 15)
            .AddSnake(x: 5, y: 5, Direction.Right, 3)
            .AddFood()
            .SetPointCount(0)
            .SetStepCount(0)
            .Build();

        _gameLogic = new GameLogic(_gameData);
        
        _gameTimer = new Timer(timeBetweenSteps);
        _gameTimer.Elapsed += GameStep;
        _gameTimer.AutoReset = true;
    }

    public  void StartGame()
    {
        if (!_gameIsRunning)
        {
            _gameIsRunning = true;
            _gameTimer.Start();
             _ = TransferringGameState();
        }
    }

    private async void GameStep(object? sender, ElapsedEventArgs e)
    {
        try
        {
            if (!_gameIsRunning || _gameData.IsGameOver) return;

            _gameLogic.DoStep();
            await TransferringGameState();

            if (_gameData.IsGameOver)
            {
                _gameTimer.Stop();
                _gameIsRunning = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в шаге: {ex.Message}");
        }
    }

    public void TurnSnake(Direction direction)
    {
        _gameLogic.ChangeDirection(direction);
    }

    public async Task ConnectClient(WebSocket webSocket)
    {
        var client = webSocket.GetHashCode().ToString();

        _clients.TryAdd(client, webSocket);

        await SendGameStateToClient(webSocket);

        while (_gameIsRunning && webSocket.State == WebSocketState.Open)
        {
            await Task.Delay(50);
        }

        _clients.TryRemove(client, out _);
    }

    private async Task TransferringGameState()
    {
        var gameState = BuildGameState();
        var json = JsonSerializer.Serialize(gameState);

        foreach (var client in _clients.Values)
        {
            if (client.State == WebSocketState.Open)
            {
                await client.SendAsync(Encoding.UTF8.GetBytes(json), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }

    private async Task SendGameStateToClient(WebSocket webSocket)
    {
        var gameState = BuildGameState();
        var json = JsonSerializer.Serialize(gameState);
        await webSocket.SendAsync(Encoding.UTF8.GetBytes(json), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    private GameElements BuildGameState()
    {
        return new GameElements
        {
            Width = _gameData.BoardWidth,
            Height = _gameData.BoardHeight,
            Snake = _gameData.Snake.Body.Select(coord => new Coord(coord.X, coord.Y)).ToArray(),
            Food = _gameData.Food != null ? new Coord(_gameData.Food.X, _gameData.Food.Y) : null,
            PointCount = _gameData.PointCount,
            StepCount = _gameData.StepCount,
            IsGameOver = _gameData.IsGameOver,
            HeadDirection = _gameData.Snake.LastStepDirection.ToString()
        };
    }
}