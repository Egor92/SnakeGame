using System.ComponentModel;

namespace Snake.Tests;

public class SnakeBuilder
{
    private readonly Queue<Pixel> _body = new();
    private Direction _direction;
    private Pixel _head;

    private SnakeBuilder()
    {
    }

    public SnakeBuilder CreateSnakeBuilder(int x, int y, Direction direction)
    {
        var builder = new SnakeBuilder()
        {
            _head = new Pixel(x, y),
            _direction = direction,
        };
      
        return builder;
    }

    public SnakeBuilder Grow(int length)
    {
        Pixel lastPixel = _head;
        if (_direction == Direction.Right)
        {
            for (int i = 1; i < length; i++)
            {
                _body.Enqueue(new Pixel(lastPixel.X - 1, lastPixel.Y));
            }
        }
        else if (_direction == Direction.Left)
        {
            for (int i = 1; i < length; i++)
            {
                _body.Enqueue(new Pixel(lastPixel.X + 1, lastPixel.Y));
            }
        }
        else if (_direction == Direction.Up)
        {
            for (int i = 1; i < length; i++)
            {
                _body.Enqueue(new Pixel(lastPixel.X, lastPixel.Y + 1));
            }
        }
        else if (_direction == Direction.Down)
        {
            for (int i = 1; i < length; i++)
            {
                _body.Enqueue(new Pixel(lastPixel.X, lastPixel.Y - 1));
            }
        }

        _body.Enqueue(_head);
        return this;
    }

    public Snake Build()
    {
        return new Snake(_body, _direction, _head)
        {
            Body = _body,
            Direction = _direction,
            Head = _head
        };
    }
}