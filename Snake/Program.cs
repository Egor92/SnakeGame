using System.Text;
using Snake;

namespace Snake_Game;

public class Program
{
    private static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = Encoding.Unicode;
        var gameData = GameDataBuilder.Create()
                                      .SetPlayingFieldSize(45, 15)
                                      .CreateWallAroundPlayingField(45, 15)
                                      .AddSnake(10, 10, Direction.Right, 3)
                                      .AddFood()
                                      .SetPointCount(0)
                                      .SetStepCount(0)
                                      .Build();
        var gameLogic = new GameLogic(gameData);

        // Создать Game и запустить
        var game = new Game(gameData, gameLogic);
        game.Start();
        Console.ReadLine();
    }
}