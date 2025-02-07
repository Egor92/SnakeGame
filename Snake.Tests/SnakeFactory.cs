namespace Snake.Tests;

public  class SnakeFactory
{
    public static Snake Create(int x, int y, Direction direction, int snakeLength)
    {
        var snake = SnakeBuilder.CreateSnake(5, 5, Direction.Down, snakeLength) // Положение головы и направление движения
            .Grow(Direction.Right) // Пусть змейка вырастит на 1 пиксель вправо
            .Grow(Direction.Down) // Пусть змейка вырастит на 1 пиксель вниз
            .Grow(Direction.Left) // Пусть змейка вырастит на 1 пиксель влево
            .Grow(Direction.Left) // Пусть змейка вырастит на 1 пиксель влево
            .Build();
        return snake;
    }
}