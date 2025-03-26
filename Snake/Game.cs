namespace Snake;

public class Game(GameData gameData, GameLogic gameLogic)
{
    private readonly GameRenderer _gameRenderer = new();

    public void Start()
    {
        _gameRenderer.RenderGame(gameData);

        while (!gameData.IsGameOver)
        {
            Direction currentDirection = gameData.Snake.Direction;
            ConsoleKey key = ConsoleKey.None;
            while (Console.KeyAvailable)
            {
                key = Console.ReadKey(true).Key;
            }

            if (key != ConsoleKey.None)
            {
                Direction newDirection = key switch
                {
                    ConsoleKey.UpArrow => Direction.Up,
                    ConsoleKey.DownArrow => Direction.Down,
                    ConsoleKey.LeftArrow => Direction.Left,
                    ConsoleKey.RightArrow => Direction.Right,
                    _ => currentDirection
                };

                if (newDirection != currentDirection.GetOpposite())
                {
                    currentDirection = newDirection;
                }

                if (currentDirection != gameData.Snake.Direction)
                {
                    gameLogic.ChangeDirection(currentDirection);
                }
            }

            gameLogic.DoStep();
            _gameRenderer.RenderGame(gameData);

            if (gameData.IsGameOver)
            {
                break;
            }

            Thread.Sleep(100);
        }
    }
}