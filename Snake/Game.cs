namespace Snake;

public class Game(GameData gameData, GameLogic gameLogic)
{
    private readonly GameRenderer _gameRenderer = new();

    public void Start()
    {
        Console.SetWindowSize(gameData.BoardWidth + 6, gameData.BoardHeight + 6);

        while (!gameData.IsGameOver)
        {
            _gameRenderer.RenderGame(gameData);

            while (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true)
                                        .Key;
                Direction newDirection = key switch
                {
                    ConsoleKey.UpArrow => Direction.Up,
                    ConsoleKey.DownArrow => Direction.Down,
                    ConsoleKey.LeftArrow => Direction.Left,
                    ConsoleKey.RightArrow => Direction.Right,
                    _ => throw new ArgumentOutOfRangeException(nameof(key), key, $"Unexpected key: {key}")
                };
                gameLogic.ChangeDirection(newDirection);
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