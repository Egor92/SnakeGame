namespace Snake;

public class GameLogic(GameData gameData)
{
    public void DoStep()
    {
        var snakeBody = gameData.Snake.Body;
        var head = gameData.Snake.Head;
        int newX = head.X;
        int newY = head.Y;

        switch (gameData.Snake.Direction)
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
        gameData.Snake.Head = newHead;

        if (gameData.Food != null && CheckFoodCollision())
        {
            gameData.Food = GetFreePixel(gameData);
            GrowSnake(out newHead);
            gameData.Snake.Head = newHead;
            gameData.PointCount += GameSettings.PointsForFood;
        }

        gameData.StepCount += 1;
        gameData.IsGameOver = CheckCollisions();
    }

    private bool CheckCollisions()
    {
        var snakeBody = gameData.Snake.Body.ToArray();
        var head = snakeBody.Last();

        bool isSnakeBumpedIntoItself = snakeBody[0..^1].Contains(head);
        bool isSnakeBumpedIntoWalls = gameData.Walls.Contains(head);

        return isSnakeBumpedIntoItself || isSnakeBumpedIntoWalls;
    }

    private bool CheckFoodCollision()
    {
        return gameData.Snake.Head == gameData.Food;
    }

    private Pixel GetFreePixel()
    {
        while (true)
        {
            GameFieldHelper gameFieldHelper = new GameFieldHelper();
            Pixel[] fields = gameFieldHelper.GetFreePixels(gameData);
            Pixel newPixelFood = fields[RandomAdapter.Next(fields.Length)];
            return newPixelFood;
        }
    }

    private void GrowSnake(out Pixel newHead)
    {
        var element = gameData.Snake.Body.Last();
        var direction = gameData.Snake.Direction;
        var newPixel = gameData.Snake.Direction switch
        {
            Direction.Up => new Pixel(element.X, element.Y - 1),
            Direction.Down => new Pixel(element.X, element.Y + 1),
            Direction.Left => new Pixel(element.X - 1, element.Y),
            Direction.Right => new Pixel(element.X + 1, element.Y),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, $"Unexpected direction: {direction}")
        };

        gameData.Snake.Body.Enqueue(newPixel);
        newHead = newPixel;
    }

    public void ChangeDirection(Direction direction)
    {
        if (gameData.Snake.Direction != direction.GetOpposite())
        {
            gameData.Snake.Direction = direction;
        }
    }
}