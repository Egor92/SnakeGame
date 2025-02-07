using System.ComponentModel;

namespace Snake.Tests;

public class SnakeBuilder
{
    private Queue<Pixel> _body = new();
    private Direction _direction;
    private Pixel _head;
    private Snake _snake;
    private int _length;

    private SnakeBuilder()
    {
    }

    public static SnakeBuilder CreateSnake(int x, int y, Direction direction, int snakeLength)
    {
        var builder = new SnakeBuilder
        {
            _head = new Pixel(x, y),
            _direction = direction,
            _length = snakeLength
        };

        builder.AddBody();
        return builder;
    }

    private void AddBody()
    {
        _body.Enqueue(_head);
        for (int i = 1; i < _length; i++)
        {
            Grow(_direction);
        }
    }

    public SnakeBuilder Grow(Direction direction)
    {
        if (direction == Direction.Right)
        {
            _body.Enqueue(new Pixel(_body.Last().X - 1, _body.Last().Y));
        }
        else if (direction == Direction.Left)
        {
            _body.Enqueue(new Pixel(_body.Last().X + 1, _body.Last().Y));
        }
        else if (direction == Direction.Up)
        {
            _body.Enqueue(new Pixel(_body.Last().X, _body.Last().Y + 1));
        }
        else if (direction == Direction.Down)
        {
            _body.Enqueue(new Pixel(_body.Last().X, _body.Last().Y - 1));
        }

        return this;
    }

    public Snake Build()
    {
        return new Snake(_body, _direction, _head);
    }
}