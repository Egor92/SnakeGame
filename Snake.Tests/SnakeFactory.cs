namespace Snake.Tests;

public static class SnakeFactory
{
    public static Snake Create(int x, int y, Direction direction, int snakeLength)
    {
        // создание змейки
        var head = new Pixel(x, y);
        var body = new Queue<Pixel>();
        if (direction == Direction.Right)
        {
            for (int i = snakeLength - 1; i >= 1; i--)
            {
                body.Enqueue(new Pixel(x - i, y));
            }
        }
        else if (direction == Direction.Left)
        {
            for (int i = snakeLength - 1; i >= 1; i--)
            {
                body.Enqueue(new Pixel(x + i, y));
            }
        }
        else if (direction == Direction.Up)
        {
            for (int i = snakeLength - 1; i >= 1; i--)
            {
                body.Enqueue(new Pixel(x, y + i));
            }
        }
        else if (direction == Direction.Down)
        {
            for (int i = snakeLength - 1; i >= 1; i--)
            {
                body.Enqueue(new Pixel(x, y - i));
            }
        }

        body.Enqueue(head);

        var directionSnake = direction;
        var snake = new Snake(body, directionSnake, head);
        return snake;
    }
}