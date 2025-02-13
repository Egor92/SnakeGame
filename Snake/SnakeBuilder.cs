using System.Collections;

namespace Snake;

public class SnakeBuilder
{
    private List<Pixel> _body = new();
    private Direction _headDirection;
    private Pixel _head;
    private Direction _tailDirection;
    private Pixel _tail;

    private SnakeBuilder()
    {
    }

    public static SnakeBuilder Create(int x, int y, Direction snakeDirection)
    {
        return new SnakeBuilder()
        {
            _head = new Pixel(x, y),
            _tail = new Pixel(x, y),
            _headDirection = snakeDirection,
            _tailDirection = snakeDirection
        };
    }

    private Direction GetOppositeDirection(Direction direction)
    {
        return direction switch
        {
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            Direction.Up => Direction.Down,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction,
                $"Unexpected direction: {direction}")
        };
    }

    public SnakeBuilder Grow(int length)
    {
        if (_tailDirection == _headDirection)
        {
            _tailDirection = GetOppositeDirection(_headDirection);
        }

        Grow(_tailDirection, length);
        return this;
    }

    public SnakeBuilder Grow(Direction direction, int length)
    {
        for (int i = 1; i <= length; i++)
        {
            Pixel bodyPixel = direction switch
            {
                Direction.Up => new Pixel(_tail.X, _tail.Y - 1),
                Direction.Down => new Pixel(_tail.X, _tail.Y + 1),
                Direction.Left => new Pixel(_tail.X - 1, _tail.Y),
                Direction.Right => new Pixel(_tail.X + 1, _tail.Y),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction,
                    $"Unexpected direction: {direction}")
            };
            _body.Add(bodyPixel);
            _tail = bodyPixel;
        }

        _tailDirection = direction;
        return this;
    }

    public Snake Build()
    {
        var reversedBody = ((IEnumerable<Pixel>)_body).Reverse();
        Queue<Pixel> snakeBody = new Queue<Pixel>(reversedBody);
        snakeBody.Enqueue(_head);
        return new Snake(snakeBody, _headDirection, _head);
    }
}