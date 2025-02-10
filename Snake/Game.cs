namespace Snake;

public class Game
{
    private GameData _gameData;
    private GameRenderer _gameRenderer;
    private readonly GameLogic _gameLogic;

    public Game(GameData gameData, GameLogic gameLogic)
    {
        _gameData = gameData;
        _gameRenderer = new GameRenderer();
        _gameLogic = gameLogic;
    }

    public void Start()
    {
        while (!_gameData.IsGameOver)
        {
            _gameRenderer.RenderGame(_gameData);

            while (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {
                    _gameLogic.ChangeDirection(Direction.Up);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    _gameLogic.ChangeDirection(Direction.Down);
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    _gameLogic.ChangeDirection(Direction.Left);
                }
                else
                {
                    _gameLogic.ChangeDirection(Direction.Right);
                }
            }

            _gameLogic.DoStep();

            if (_gameLogic.CheckCollisions())
            {
                _gameData.IsGameOver = true;
                break;
            }

            Thread.Sleep(200);
        }

        _gameRenderer.RenderGame(_gameData);
    }
}