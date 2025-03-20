using Snake;

namespace Snake_Game;

public class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.Unicode;
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
        var game = new Game(gameData, gameLogic);
        game.Start();
        Console.ReadLine();
    }
}