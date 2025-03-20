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
                Direction newDirection = key switch
                {
                    ConsoleKey.UpArrow => Direction.Up,
                    ConsoleKey.DownArrow => Direction.Down,
                    ConsoleKey.LeftArrow => Direction.Left,
                    ConsoleKey.RightArrow => Direction.Right,
                    _ => gameData.Snake.Direction
                };
                gameLogic.ChangeDirection(newDirection);
            }

            gameLogic.DoStep();

            if (gameData.IsGameOver)
            {
                break;
            }

            Thread.Sleep(100);
        }

        _gameRenderer.RenderGame(gameData);
    }
}