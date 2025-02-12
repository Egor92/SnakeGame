using System.ComponentModel;

namespace Snake.Tests;

public class SnakeBuilder
{
    private readonly Queue<Pixel> _body = new();
    private Direction _headDirection;
    private Pixel _head;
    private Direction _tailDirection;

    private SnakeBuilder()
    {
    }

    public static SnakeBuilder Create(int x, int y, Direction snakeDirection)
    {
        return new SnakeBuilder()
        {
            _head = new Pixel(x, y),
            _headDirection = snakeDirection,
        };
    }

    private SnakeBuilder GetTailDirection(Direction snakeDirection)
    {
        _tailDirection = snakeDirection switch
        {
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            Direction.Up => Direction.Down,
            _ => throw new ArgumentOutOfRangeException(nameof(snakeDirection), snakeDirection,
                $"Unexpected direction: {snakeDirection}")
        };

        return this;
    }

    public SnakeBuilder Grow(int length)
    {
        GetTailDirection(_headDirection);
        Grow(_tailDirection, length);
        return this;
    }

    public SnakeBuilder Grow(Direction direction, int length)
    {
        Pixel lastPixel = _head;
        for (int i = 0; i < length; i++)
        {
            Pixel bodyPixel = direction switch
            {
                Direction.Up => new Pixel(lastPixel.X, lastPixel.Y - 1),
                Direction.Down => new Pixel(lastPixel.X, lastPixel.Y + 1),
                Direction.Left => new Pixel(lastPixel.X - 1, lastPixel.Y),
                Direction.Right => new Pixel(lastPixel.X + 1, lastPixel.Y),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction,
                    $"Unexpected direction: {direction}")
            };
            _body.Enqueue(bodyPixel);
        }

        _body.Enqueue(_head);
        return this;
    }

    public Snake Build()
    {
        return new Snake(_body, _headDirection, _head)
        {
            Body = _body,
            Direction = _headDirection,
            Head = _head
        };
    }
}