using Snake.Logic;

namespace Snake.ConsoleApp;

public class Game(GameData gameData, GameLogic gameLogic, int timeBetweenSteps)
{
    private readonly GameRenderer _gameRenderer = new(gameData);

    public void Start()
    {
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
            GameSceneCreator gameSceneCreator = new GameSceneCreator();
            CellObject[,] cellObjects = gameSceneCreator.GetSceneCellObjects(gameData);

            _gameRenderer.RenderGame(cellObjects);
            if (gameData.IsGameOver)
            {
                break;
            }

            Thread.Sleep(timeBetweenSteps);
        }
    }
}