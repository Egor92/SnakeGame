namespace Snake;

public class Game(GameData gameData, GameLogic gameLogic, int timeBetweenSteps)
{
    private readonly GameRenderer _gameRenderer = new();

    public void Start()
    {
        _gameRenderer.RenderGame(gameData);

        while (!gameData.IsGameOver)
        {
            ConsoleKey key = ConsoleKey.None;
            while (Console.KeyAvailable)
            {
                key = Console.ReadKey(true).Key;
            }

            Direction? requestedDirection = key switch
            {
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                _ => null
            };

            if (requestedDirection != null)
            {
                gameLogic.ChangeDirection(requestedDirection.Value);
            }

            gameLogic.DoStep();
            _gameRenderer.RenderGame(gameData);

            if (gameData.IsGameOver)
            {
                break;
            }

            Thread.Sleep(timeBetweenSteps);
        }
    }
}