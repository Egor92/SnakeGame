using System.Collections;

namespace Snake;

public class SnakeBuilder
{
    private List<Pixel> _body = new();
    private Direction _headDirection;
    private Pixel _head;
    private Direction _tailDirection;
    private Pixel _lastPixel;
    private Direction _directionLastPixel;

    private SnakeBuilder()
    {
    }

    public static SnakeBuilder Create(int x, int y, Direction snakeDirection)
    {
        return new SnakeBuilder()
        {
            _head = new Pixel(x, y),
            _lastPixel = new Pixel(x, y),
            _headDirection = snakeDirection,
            _directionLastPixel = snakeDirection
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
        if (_directionLastPixel == _headDirection)
        {
            _tailDirection = GetOppositeDirection(_headDirection);
        }
        else
        {
            _tailDirection = _directionLastPixel;
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
                Direction.Up => new Pixel(_lastPixel.X, _lastPixel.Y - 1),
                Direction.Down => new Pixel(_lastPixel.X, _lastPixel.Y + 1),
                Direction.Left => new Pixel(_lastPixel.X - 1, _lastPixel.Y),
                Direction.Right => new Pixel(_lastPixel.X + 1, _lastPixel.Y),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction,
                    $"Unexpected direction: {direction}")
            };
            _body.Add(bodyPixel);
            _lastPixel = bodyPixel;
        }

        _directionLastPixel = direction;
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