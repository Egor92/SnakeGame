using Snake;

namespace Snake_Game;

public class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        var gameData = GameDataBuilder.Create()
                                      .SetPlayingFieldSize(width: 32, height: 3)
                                      .CreateWallAroundPlayingField(width: 32, height: 3)
                                      .AddSnake(x: 16, y: 1, Direction.Right, 15)
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