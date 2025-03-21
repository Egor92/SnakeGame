namespace Snake;

public class GameLogic
{
    private readonly GameFieldHelper _gameFieldHelper = new();
    private readonly GameData _gameData;

    private Direction _currentDirection;
    private Direction _previousDirection;
    private Direction _nextDirection;

    public GameLogic(GameData gameData)
    {
        _gameData = gameData;
        _currentDirection = gameData.Snake.Direction;
        _previousDirection = _currentDirection;
        _nextDirection = _currentDirection;
    }

    public void DoStep()
    {
        _previousDirection = _currentDirection;
        _currentDirection = _nextDirection;
        _gameData.Snake.Direction = _currentDirection;

        var snakeBody = _gameData.Snake.Body;
        var head = _gameData.Snake.Head;
        int newX = head.X;
        int newY = head.Y;

        switch (_currentDirection)
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
        snakeBody.Dequeue();
        snakeBody.Enqueue(newHead);
        _gameData.Snake.Head = newHead;

        if (_gameData.Food != null && CheckFoodCollision())
        {
            _gameData.Food = GetFreePixel();
            GrowSnake(out newHead);
            _gameData.Snake.Head = newHead;
            _gameData.PointCount += GameSettings.PointsForFood;
        }

        _gameData.StepCount += 1;
        _gameData.IsGameOver = CheckCollisions();
    }

    private bool CheckCollisions()
    {
        var snakeBody = _gameData.Snake.Body.ToArray();
        var head = snakeBody.Last();

        bool isSnakeBumpedIntoItself = snakeBody[0..^1].Contains(head);
        bool isSnakeBumpedIntoWalls = _gameData.Walls.Contains(head);

        return isSnakeBumpedIntoItself || isSnakeBumpedIntoWalls;
    }

    private bool CheckFoodCollision()
    {
        return _gameData.Snake.Head == _gameData.Food;
    }

    private Pixel GetFreePixel()
    {
        while (true)
        {
            Pixel[] fields = _gameFieldHelper.GetFreePixels(_gameData);
            Pixel freePixel = fields[RandomAdapter.Next(fields.Length)];
            return freePixel;
        }
    }

    private void GrowSnake(out Pixel newHead)
    {
        var element = _gameData.Snake.Body.Last();
        var direction = _currentDirection;
        var newPixel = direction switch
        {
            Direction.Up => new Pixel(element.X, element.Y - 1),
            Direction.Down => new Pixel(element.X, element.Y + 1),
            Direction.Left => new Pixel(element.X - 1, element.Y),
            Direction.Right => new Pixel(element.X + 1, element.Y),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, $"Unexpected direction: {direction}")
        };

        _gameData.Snake.Body.Enqueue(newPixel);
        newHead = newPixel;
    }

    public void ChangeDirection(Direction direction)
    {
        if (direction != _currentDirection.GetOpposite() || direction != _previousDirection.GetOpposite())
        {
            _nextDirection = direction;
        }
    }
}