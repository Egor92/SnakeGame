namespace Snake.Tests;

public class SnakeBuilderTest
{
    [Test]
    public void Build_SnakeIsConfiguredInFormAsFive_SnakeBuiltInFormAsFive()
    {
        // Arrange  
        int headX = 5;
        int headY = 5;
        var snakeBuilder = SnakeBuilder.Create(headX, headY, Direction.Right)
                                       .Grow(Direction.Left, 2)
                                       .Grow(Direction.Down, 2)
                                       .Grow(Direction.Right, 2)
                                       .Grow(Direction.Down, 2)
                                       .Grow(Direction.Left, 2);

        // Act
        var snake = snakeBuilder.Build();

        // Assert
        var snakeBody = snake.Body.Reverse().ToArray();
        int x = headX;
        int y = headY;
        int index = 0;
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(headX, headY)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(--x, y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(--x, y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(x, ++y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(x, ++y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(++x, y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(++x, y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(x, ++y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(x, ++y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(--x, y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(--x, y)));
    }

    [Test]
    public void Build_GrowAsideAndJustGrow_SnakeBuiltInALine()
    {
        // Arrange
        int headX = 6;
        int headY = 6;
        var snakeBuilder = SnakeBuilder.Create(headX, headY, Direction.Down)
                                       .Grow(Direction.Left, 2)
                                       .Grow(2);

        // Act
        var snake = snakeBuilder.Build();

        // Assert
        var snakeBody = snake.Body.Reverse().ToArray();
        int x = headX;
        int y = headY;
        int index = 0;
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(headX, headY)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(--x, y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(--x, y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(--x, y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(--x, y)));
    }

    [Test]
    public void Build_SnakeIsConfiguredInFormAsCAndGrowElse_SnakeIsBuiltInFormAsC()
    {
        // Arrange
        int headX = 5;
        int headY = 5;
        var snakeBuilder = SnakeBuilder.Create(headX, headY, Direction.Right)
                                       .Grow(Direction.Left, 2)
                                       .Grow(Direction.Down, 2)
                                       .Grow(Direction.Right, 2)
                                       .Grow(1);

        // Act
        var snake = snakeBuilder.Build();

        // Assert
        var snakeBody = snake.Body.Reverse().ToArray();
        int x = headX;
        int y = headY;
        int index = 0;
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(headX, headY)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(--x, y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(--x, y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(x, ++y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(x, ++y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(++x, y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(++x, y)));
        Assert.That(snakeBody[index++], Is.EqualTo(new Pixel(++x, y)));
    }
}