using Snake;
using Microsoft.Extensions.Configuration;

namespace Snake_Game;

public class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        var config = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json").Build();
        
        int timeBetweenSteps = int.Parse(config["GameSettings:TimeBetweenSteps"]);

        var gameData = GameDataBuilder.Create()
                                      .SetPlayingFieldSize(width: 45, height: 15)
                                      .CreateWallAroundPlayingField(width: 45, height: 15)
                                      .AddSnake(x: 5, y: 5, Direction.Right, 3)
                                      .AddFood()
                                      .SetPointCount(0)
                                      .SetStepCount(0)
                                      .Build();
        var gameLogic = new GameLogic(gameData);

        // Создать Game и запустить
        var game = new Game(gameData, gameLogic, timeBetweenSteps);
        game.Start();
        Console.ReadLine();
    }
}