using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Timers;
using client_serverApp.Snake.Logic;
using Client;
using Timer = System.Timers.Timer;

namespace client_serverApp;

public class SnakeGame
{
    private readonly GameData _gameData;
    private readonly GameLogic _gameLogic;
    private readonly Timer _gameTimer;
    private readonly WebSocket _webSocket;
    private bool _isRunning;

    public SnakeGame(WebSocket webSocket, int timeBetweenSteps)
    {
        _webSocket = webSocket;

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
        _gameTimer.Elapsed += OnGameStep;
        _gameTimer.AutoReset = true;
    }

    public void Start()
    {
        if (!_isRunning && !_gameData.IsGameOver)
        {
            _isRunning = true;
            _gameTimer.Start();
            _ = SendState();
        }
    }

    private async void OnGameStep(object? sender, ElapsedEventArgs e)
    {
        if (!_isRunning || _gameData.IsGameOver) return;

        try
        {
            _gameLogic.DoStep();
            await SendState();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в игре: {ex.Message}");
        }
    }

    public void ChangeDirection(Direction direction)
    {
        if (_isRunning && !_gameData.IsGameOver)
        {
            _gameLogic.ChangeDirection(direction);
        }
    }

    private async Task SendState()
    {
        if (_webSocket.State != WebSocketState.Open) return;

        var state = new GameElementsDto
        {
            Width = _gameData.BoardWidth,
            Height = _gameData.BoardHeight,
            Snake = _gameData.Snake.Body.Select(c => new CoordDto(c.X, c.Y)).ToArray(),
            Food = _gameData.Food != null ? new CoordDto(_gameData.Food.X, _gameData.Food.Y) : null,
            PointCount = _gameData.PointCount,
            StepCount = _gameData.StepCount,
            IsGameOver = _gameData.IsGameOver,
            HeadDirection = _gameData.Snake.LastStepDirection.ToString()
        };

        var json = JsonSerializer.Serialize(state);
        await _webSocket.SendAsync(
            Encoding.UTF8.GetBytes(json),
            WebSocketMessageType.Text,
            true,
            CancellationToken.None);
    }
}