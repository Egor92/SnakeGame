using client_serverApp;
using client_serverApp.Snake.Logic;


var builder = WebApplication.CreateBuilder(args); //создания объекта

builder.Services.AddOpenApi();
builder.Services.AddSingleton<GameService>();

var app = builder.Build();

app.UseWebSockets(); // подключение WebSokets

//подключение клиентов
app.Map("/websockets", async (HttpContext context, GameService gameService) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        await gameService.ConnectClient(webSocket);
    }
    else
    {
        context.Response.StatusCode = 400;
    }
});

//запуск игры
app.MapPost("/start/{clientId}", (string clientId, GameService gameService) =>
{
    gameService.StartGame(clientId);
    return Results.Ok();
});

//поворот змейки
app.MapPost("/turn/{clientId}", async (string clientId, HttpContext context, GameService gameService) =>
{
    using var reader = new StreamReader(context.Request.Body);
    string body = await reader.ReadToEndAsync();
    string directionStr = body.Trim('"');

    if (Enum.TryParse<Direction>(directionStr, true, out Direction direction))
    {
        gameService.TurnSnake(clientId, direction);
        return Results.Ok();
    }

    return Results.BadRequest("Неправильное направление");
});

app.Run();