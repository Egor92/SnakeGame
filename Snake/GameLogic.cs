namespace Snake;

public class GameLogic
{
    private readonly GameData _gameData;

    public GameLogic(GameData gameData)
    {
        _gameData = gameData;
    }

    public void DoStep()
    {
        var snakeBody = _gameData.Snake.Body;
        var head = _gameData.Snake.Head;

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
        _gameData.IsGameOver = CheckCollisions();
        snakeBody.Dequeue();
        _gameData.Snake.Head = newHead;

        if (_gameData.Food != null && CheckFoodCollision())
        {
            GrowSnake();
            GenerateFood();
        }
    }

    public bool CheckCollisions()
    {
        var snakeBody = _gameData.Snake.Body.ToArray();
        var head = snakeBody.Last();

        foreach (var snakePixel in snakeBody[0..^1])
        {
            if (snakePixel.X == head.X && snakePixel.Y == head.Y)
            {
                return true;
            }
        }

        foreach (var wallPixel in _gameData.Walls)
        {
            if (wallPixel.X == head.X && wallPixel.Y == head.Y)
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckFoodCollision()
    {
        var head = _gameData.Snake.Head;
        if (head.X == _gameData.Food.X && head.Y == _gameData.Food.Y)
        {
            return true;
        }

        return false;
    }

    public void GrowSnake()
    {
        var newElement = _gameData.Snake.Body.Last();
        _gameData.Snake.Body.Enqueue(new Pixel(newElement.X, newElement.Y));
    }

    public void GenerateFood()
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

    public void ChangeDirection(Direction direction)
    {
        if (_gameData.Snake.Direction != Direction.Down && direction == Direction.Up)
            _gameData.Snake.Direction = Direction.Up;


        if (_gameData.Snake.Direction != Direction.Up && direction == Direction.Down)
            _gameData.Snake.Direction = Direction.Down;

        if (_gameData.Snake.Direction != Direction.Right && direction == Direction.Left)
            _gameData.Snake.Direction = Direction.Left;


        if (_gameData.Snake.Direction != Direction.Left && direction == Direction.Right)
            _gameData.Snake.Direction = Direction.Right;
    }
}