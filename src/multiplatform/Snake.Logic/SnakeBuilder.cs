namespace Snake.Logic;

public class SnakeBuilder
{
    private readonly List<Cell> _body = new();
    private Direction _headDirection;
    private Cell _head = null!;
    private Direction _tailDirection;
    private Cell _tail = null!;

    private SnakeBuilder()
    {
    }

    public static SnakeBuilder Create(int x, int y, Direction snakeDirection)
    {
        return new SnakeBuilder()
        {
            _head = new Cell(x, y),
            _tail = new Cell(x, y),
            _headDirection = snakeDirection,
            _tailDirection = snakeDirection.GetOpposite(),
        };
    }

    public SnakeBuilder Grow(int length)
    {
        Grow(_tailDirection, length);
        return this;
    }

    public SnakeBuilder Grow(Direction direction, int length)
    {
        for (int i = 1; i <= length; i++)
        {
            Cell bodyCell = direction switch
            {
                Direction.Up => new Cell(_tail.X, _tail.Y - 1),
                Direction.Down => new Cell(_tail.X, _tail.Y + 1),
                Direction.Left => new Cell(_tail.X - 1, _tail.Y),
                Direction.Right => new Cell(_tail.X + 1, _tail.Y),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, $"Unexpected direction: {direction}")
            };
            _body.Add(bodyCell);
            _tail = bodyCell;
        }

        _tailDirection = direction;
        return this;
    }

    public Snake Build()
    {
        var reversedBody = ((IEnumerable<Cell>)_body).Reverse();
        Queue<Cell> snakeBody = new Queue<Cell>(reversedBody);
        snakeBody.Enqueue(_head);
        return new Snake(snakeBody, _headDirection, _head);
    }
}