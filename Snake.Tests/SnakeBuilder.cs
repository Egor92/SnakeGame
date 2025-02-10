using System.ComponentModel;

namespace Snake.Tests;

public class SnakeBuilder
{
    private readonly Queue<Pixel> _body = new();
    private readonly Direction _direction;
    private readonly Pixel _head;


    public SnakeBuilder(int x, int y, Direction direction, int length)
    {
        _head = new Pixel(x, y);
        _direction = direction;
        Grow(_direction, length);
    }

    private SnakeBuilder Grow(Direction direction, int length)
    {
        Pixel tail = _head;
        if (direction == Direction.Right)
        {
            for (int i = 1; i < length; i++)
            {
                _body.Enqueue(new Pixel(tail.X - 1, tail.Y));
            }
        }
        else if (direction == Direction.Left)
        {
            for (int i = 1; i < length; i++)
            {
                _body.Enqueue(new Pixel(tail.X + 1, tail.Y));
            }
        }
        else if (direction == Direction.Up)
        {
            for (int i = 1; i < length; i++)
            {
                _body.Enqueue(new Pixel(tail.X, tail.Y + 1));
            }
        }
        else if (direction == Direction.Down)
        {
            for (int i = 1; i < length; i++)
            {
                _body.Enqueue(new Pixel(tail.X, tail.Y - 1));
            }
        }

        _body.Enqueue(_head);
        return this;
    }

    public Snake Build()
    {
        return new Snake(_body, _direction, _head);
    }
}