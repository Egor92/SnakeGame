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
        snakeBody.Enqueue(newHead);
        gameData.IsGameOver = CheckCollisions();
        snakeBody.Dequeue();
        gameData.Snake.Head = newHead;

        if (gameData.Food != null && CheckFoodCollision())
        {
            GrowSnake();
            GenerateFood();
        }
    }

    public bool CheckCollisions()
    {
        var snakeBody = gameData.Snake.Body.ToArray();
        var head = snakeBody.Last();

        foreach (var snakePixel in snakeBody[0..^1])
        {
            if (snakePixel.X == head.X && snakePixel.Y == head.Y)
            {
                return true;
            }
        }

        foreach (var wallPixel in gameData.Walls)
        {
            if (wallPixel.X == head.X && wallPixel.Y == head.Y)
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckFoodCollision()
    {
        var head = gameData.Snake.Head;
        return head.X == gameData.Food.X && head.Y == gameData.Food.Y;
    }

    private void GrowSnake()
    {
        var newElement = gameData.Snake.Body.Last();
        gameData.Snake.Body.Enqueue(new Pixel(newElement.X, newElement.Y));
    }

    private void GenerateFood()
    {
        Random random = new Random();

        while (true)
        {
            int x = random.Next(1, gameData.BoardWidth - 1);
            int y = random.Next(1, gameData.BoardHeight - 1);

            Pixel newFood = new Pixel(x, y);

            if (!gameData.Walls.Contains(newFood) &&
                !gameData.Snake.Body.Contains(newFood))
            {
                gameData.Food = newFood;
                break;
            }
        }
    }

    public void ChangeDirection(Direction direction)
    {
        if (gameData.Snake.Direction != Direction.Down && direction == Direction.Up)
            gameData.Snake.Direction = Direction.Up;


        if (gameData.Snake.Direction != Direction.Up && direction == Direction.Down)
            gameData.Snake.Direction = Direction.Down;

        if (gameData.Snake.Direction != Direction.Right && direction == Direction.Left)
            gameData.Snake.Direction = Direction.Left;


        if (gameData.Snake.Direction != Direction.Left && direction == Direction.Right)
            gameData.Snake.Direction = Direction.Right;
    }
}