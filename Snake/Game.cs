namespace Snake;
public class Game
{
    private GameData _gameData;
    private GameRenderer _gameRenderer;

    public Game(GameData gameData)
    {
        _gameData = gameData;
        _gameRenderer = new GameRenderer();
    }

    public void Start()
    {
        while (!_gameData.IsGameOver)
        {
            _gameRenderer.RenderGame(_gameData);

            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                ChangeDirection(key);
            }

            Move();

            if (CheckCollisions())
            {
                _gameData.IsGameOver = true;
                break;
            }

            if (CheckFoodCollision())
            {
                GrowSnake();
                GenerateFood();
            }

            Thread.Sleep(200);
        }

        Console.Clear();
        Console.SetCursorPosition(10, 10);
        Console.WriteLine("Game Over!");
    }

    private void ChangeDirection(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (_gameData.Snake.Direction != Direction.Down)
                    _gameData.Snake.Direction = Direction.Up;
                break;
            case ConsoleKey.DownArrow:
                if (_gameData.Snake.Direction != Direction.Up)
                    _gameData.Snake.Direction = Direction.Down;
                break;
            case ConsoleKey.LeftArrow:
                if (_gameData.Snake.Direction != Direction.Right)
                    _gameData.Snake.Direction = Direction.Left;
                break;
            case ConsoleKey.RightArrow:
                if (_gameData.Snake.Direction != Direction.Left)
                    _gameData.Snake.Direction = Direction.Right;
                break;
        }
    }

    private void Move()
    {
        var snakeBody = _gameData.Snake.Body;
        var head = snakeBody.Last();

        int newX = head.X;
        int newY = head.Y;

        switch (_gameData.Snake.Direction)
        {
            case Direction.Up:
                newY--;
                break;
            case Direction.Down:
                newY++;
                break;
            case Direction.Left:
                newX--;
                break;
            case Direction.Right:
                newX++;
                break;
        }

        var newHead = new Pixel(newX, newY);
        snakeBody.Enqueue(newHead);

        snakeBody.Dequeue();
        
    }

    private bool CheckCollisions()
    {
        var snakeBody = _gameData.Snake.Body.ToArray();
        var head = snakeBody.Last();

        for (int i = 0; i < snakeBody.Length - 1; i++)
        {
            if (snakeBody[i].X == head.X && snakeBody[i].Y == head.Y)
            {
                return true;
            }
        }

        for (int i = 0; i < _gameData.Walls.Count; i++)
        {
            if (_gameData.Walls[i].X == head.X && _gameData.Walls[i].Y == head.Y)
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckFoodCollision()
    {
        var head = _gameData.Snake.Body.Last();
        return head.X == _gameData.Food.X && head.Y == _gameData.Food.Y;
    }

    private void GrowSnake()
    {
        var tail = _gameData.Snake.Body.Last();
        _gameData.Snake.Body.Enqueue(new Pixel(tail.X, tail.Y));
    }

    private void GenerateFood()
    {
        Random random = new Random();

        while (true)
        {
            int x = random.Next(1, _gameData.BoardWidth - 1);
            int y = random.Next(1, _gameData.BoardHeight - 1);

            Pixel newFood = new Pixel(x, y);

            if (!_gameData.Walls.Contains(newFood) &&
                !_gameData.Snake.Body.Contains(newFood))
            {
                _gameData.Food = newFood;
                break;
            }
        }
    }
}
