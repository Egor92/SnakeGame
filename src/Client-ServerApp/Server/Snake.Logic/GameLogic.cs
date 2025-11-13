
namespace client_serverApp.Snake.Logic;

public class GameLogic(GameData gameData)
{
    private readonly GameFieldHelper _gameFieldHelper = new GameFieldHelper();

    public void DoStep()
    {
        var snakeBody = gameData.Snake.Body;
        var head = gameData.Snake.Head;
        int newX = head.X;
        int newY = head.Y;

        Direction nextDirection = gameData.Snake.NextStepDirection;

        switch (nextDirection)
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

        var newHead = new Cell(newX, newY);
        snakeBody.Dequeue();
        snakeBody.Enqueue(newHead);
        gameData.Snake.Head = newHead;

        if (gameData.Food != null && CheckFoodCollision())
        {
            gameData.Food = GetFreeCell();
            GrowSnake(out newHead);
            gameData.Snake.Head = newHead;
            gameData.PointCount += GameSettings.PointsForFood;
        }

        gameData.StepCount += 1;
        gameData.IsGameOver = CheckCollisions();
        gameData.Snake.LastStepDirection = nextDirection;
        gameData.Snake.RequestedDirection = null;
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

    private Cell GetFreeCell()
    {
        while (true)
        {
            Cell[] fields = _gameFieldHelper.GetFreeCells(gameData);
            Cell freeCell = fields[RandomAdapter.Next(fields.Length)];
            return freeCell;
        }
    }

    private void GrowSnake(out Cell newHead)
    {
        var element = gameData.Snake.Body.Last();
        var direction = gameData.Snake.LastStepDirection;
        var newCell = gameData.Snake.LastStepDirection switch
        {
            Direction.Up => new Cell(element.X, element.Y - 1),
            Direction.Down => new Cell(element.X, element.Y + 1),
            Direction.Left => new Cell(element.X - 1, element.Y),
            Direction.Right => new Cell(element.X + 1, element.Y),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, $"Unexpected direction: {direction}")
        };

        gameData.Snake.Body.Enqueue(newCell);
        newHead = newCell;
    }

    public void ChangeDirection(Direction direction)
    {
        if (direction != gameData.Snake.LastStepDirection.GetOpposite())
        {
            gameData.Snake.RequestedDirection = direction;
        }
        else
        {
            gameData.Snake.RequestedDirection = null;
        }
    }
}