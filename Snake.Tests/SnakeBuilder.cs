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
        if (snakeDirection == Direction.Down)
            _tailDirection = Direction.Up;

        if (snakeDirection == Direction.Left)
            _tailDirection = Direction.Right;

        if (snakeDirection == Direction.Right)
            _tailDirection = Direction.Left;

        if (snakeDirection == Direction.Up)
            _tailDirection = Direction.Down;

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
        if (direction == Direction.Right)
        {
            for (int i = 0; i < length; i++)
            {
                _body.Enqueue(new Pixel(lastPixel.X + 1, lastPixel.Y));
            }
        }
        else if (direction == Direction.Left)
        {
            for (int i = 0; i < length; i++)
            {
                _body.Enqueue(new Pixel(lastPixel.X - 1, lastPixel.Y));
            }
        }
        else if (direction == Direction.Up)
        {
            for (int i = 0; i < length; i++)
            {
                _body.Enqueue(new Pixel(lastPixel.X, lastPixel.Y - 1));
            }
        }
        else if (direction == Direction.Down)
        {
            for (int i = 0; i < length; i++)
            {
                _body.Enqueue(new Pixel(lastPixel.X, lastPixel.Y + 1));
            }
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