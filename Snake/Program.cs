using Snake;

namespace Snake_Game;

public class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;

        var gameData = GameDataBuilder.Create()
            .SetPlayingFieldSize(width: 45, height: 15)
            .CreateWallAroundPlayingField(width: 45, height: 15)
            .AddSnake(10, 10, Direction.Right, 3)
            .AddFood()
            .Build();
        var gameLogic = new GameLogic(gameData);
        // Создать Game и запустить
        var game = new Game(gameData, gameLogic);
        game.Start();
        Console.ReadLine();
    }
}