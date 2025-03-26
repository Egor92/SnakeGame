namespace Snake;

public class Game(GameData gameData, GameLogic gameLogic)
{
    private readonly GameRenderer _gameRenderer = new();

    public void Start()
    {
        _gameRenderer.RenderGame(gameData);

        while (!gameData.IsGameOver)
        {
            Direction lastStepDirection = gameData.Snake.LastStepDirection;
            ConsoleKey key = ConsoleKey.None;
            Direction? nextStepDirection;
            while (Console.KeyAvailable)
            {
                key = Console.ReadKey(true).Key;
            }

            if (key != ConsoleKey.None)
            {
                Direction? requestedDirection = key switch
                {
                    ConsoleKey.UpArrow => Direction.Up,
                    ConsoleKey.DownArrow => Direction.Down,
                    ConsoleKey.LeftArrow => Direction.Left,
                    ConsoleKey.RightArrow => Direction.Right,
                    _ => lastStepDirection
                };

                if (requestedDirection != null && requestedDirection != lastStepDirection.GetOpposite())
                {
                    gameLogic.ChangeDirection(gameData.Snake.RequestedDirection);
                }
            }

            gameLogic.DoStep();
            gameData.Snake.LastStepDirection = lastStepDirection;
            _gameRenderer.RenderGame(gameData);

            if (gameData.IsGameOver)
            {
                break;
            }

            Thread.Sleep(100);
        }
    }
}