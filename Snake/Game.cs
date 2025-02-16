namespace Snake;

public class Game(GameData gameData, GameLogic gameLogic)
{
    private readonly GameRenderer _gameRenderer = new();

    public void Start()
    {
        while (!gameData.IsGameOver)
        {
            _gameRenderer.RenderGame(gameData);

            while (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {
                    gameLogic.ChangeDirection(Direction.Up);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    gameLogic.ChangeDirection(Direction.Down);
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    gameLogic.ChangeDirection(Direction.Left);
                }
                else
                {
                    gameLogic.ChangeDirection(Direction.Right);
                }
            }

            gameLogic.DoStep();

            if (gameData.IsGameOver)
            {
                break;
            }

            Thread.Sleep(200);
        }

        _gameRenderer.RenderGame(gameData);
    }
}