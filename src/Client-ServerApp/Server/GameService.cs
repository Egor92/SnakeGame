using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using client_serverApp.Snake.Logic;

namespace client_serverApp;

public class GameService
{
    private readonly Dictionary<string, SnakeGame> _games = new();
    private readonly int _timeBetweenSteps;

    public GameService(IConfiguration configuration)
    {
        configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
        _timeBetweenSteps = Convert.ToInt32(configuration["GameSettings:TimeBetweenSteps"]);
    }

    public async Task ConnectClient(WebSocket webSocket)
    {
        var clientId = Guid.NewGuid().ToString();
        var game = new SnakeGame(webSocket, _timeBetweenSteps);
        _games.TryAdd(clientId, game);

        var idMsg = JsonSerializer.Serialize(new { clientId });
        await webSocket.SendAsync(
            Encoding.UTF8.GetBytes(idMsg),
            WebSocketMessageType.Text,
            true,
            CancellationToken.None);

        while (webSocket.State == WebSocketState.Open)
        {
            await Task.Delay(50);
        }
    }

    public void StartGame(string clientId)
    {
        if (_games.TryGetValue(clientId, out var game))
        {
            game.Start();
        }
    }

    public void TurnSnake(string clientId, Direction direction)
    {
        if (_games.TryGetValue(clientId, out var game))
        {
            game.ChangeDirection(direction);
        }
    }
}