using System.ComponentModel;

namespace Snake.Tests;

public class SnakeBuilder
{
    private Queue<Pixel> _body = new();
    private Direction _direction;
    private Pixel _head;
    private Snake _snake;
    private int _length;

    public SnakeBuilder(int x, int y, Direction direction, int length)
    {
        _head = new Pixel(x, y);
        _direction = direction;
        _length = length;
        Grow(_direction, length);
    }

    public SnakeBuilder Grow(Direction direction, int length)
    {
        Pixel _tail = _head;
        if (direction == Direction.Right)
        {
            for (int i = 1; i < length; i++)
            {
                _body.Enqueue(new Pixel(_tail.X - 1, _tail.Y));
            }
        }
        else if (direction == Direction.Left)
        {
            for (int i = 1; i < length; i++)
            {
                _body.Enqueue(new Pixel(_tail.X + 1, _tail.Y));
            }
        }
        else if (direction == Direction.Up)
        {
            for (int i = 1; i < length; i++)
            {
                _body.Enqueue(new Pixel(_tail.X, _tail.Y + 1));
            }
        }
        else if (direction == Direction.Down)
        {
            for (int i = 1; i < length; i++)
            {
                _body.Enqueue(new Pixel(_tail.X, _tail.Y - 1));
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