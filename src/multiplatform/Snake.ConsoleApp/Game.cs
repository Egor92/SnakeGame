using Snake.Logic;

namespace Snake.ConsoleApp;

public class Game(GameData gameData, GameLogic gameLogic, int timeBetweenSteps)
{
    private readonly GameRenderer _gameRenderer = new();

    public void Start()
    {
        //   _gameRenderer.RenderGame(gameData);

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
            Bufferer bufferer = new Bufferer();
            CellObject[,] cellObjects = bufferer.GetGameValue(gameData);

            _gameRenderer.RenderGame(cellObjects);
            if (gameData.IsGameOver)
            {
                break;
            }

            Thread.Sleep(timeBetweenSteps);
        }
    }
}