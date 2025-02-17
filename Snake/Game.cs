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
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        gameLogic.ChangeDirection(Direction.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        gameLogic.ChangeDirection(Direction.Down);
                        break;
                    case ConsoleKey.LeftArrow:
                        gameLogic.ChangeDirection(Direction.Left);
                        break;
                    default:
                        gameLogic.ChangeDirection(Direction.Right);
                        break;
                }
            }

            gameLogic.DoStep();

            if (gameData.IsGameOver)
            {
                break;
            }

            Thread.Sleep(150);
        }

        _gameRenderer.RenderGame(gameData);
    }
}