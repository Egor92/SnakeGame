namespace Snake.Tests;

public class GameLogicTests
{
    private GameData _gameData;
    private GameLogic _gameLogic;

    [SetUp]
    public void Setup()
    {
        _gameData = GameDataBuilder.Create()
            .SetPlayingFieldSize(width: 10, height: 10)
            .CreateWallAroundPlayingField(width: 10, height: 10)
            .Build();
        _gameLogic = new GameLogic(_gameData);
    }

    [Test]
    public void DoStep_SnakeDirectionIsRight_SnakeMovedToRight()
    {
        // Arrange  
        _gameData.Snake = SnakeFactory.Create(5, 5, Direction.Right, 3);

        // Act
        _gameLogic.DoStep();

        // Assert
        var snakeBody = _gameData.Snake.Body.ToArray();
        Assert.That(snakeBody[0], Is.EqualTo(new Pixel(4, 5)));
        Assert.That(snakeBody[1], Is.EqualTo(new Pixel(5, 5)));
        Assert.That(snakeBody[2], Is.EqualTo(new Pixel(6, 5)));
    }

    [Test]
    public void DoStep_SnakeDirectionIsLeft_SnakeMovedToLeft()
    {
        // Arrange  
        _gameData.Snake = SnakeFactory.Create(5, 5, Direction.Left, 3);

        // Act
        _gameLogic.DoStep();

        // Assert
        var snakeBody = _gameData.Snake.Body.ToArray();
        Assert.That(snakeBody[0], Is.EqualTo(new Pixel(6, 5)));
        Assert.That(snakeBody[1], Is.EqualTo(new Pixel(5, 5)));
        Assert.That(snakeBody[2], Is.EqualTo(new Pixel(4, 5)));
    }

    [Test]
    public void DoStep_SnakeDirectionIsUp_SnakeMovedToUp()
    {
        // Arrange  
        _gameData.Snake = SnakeFactory.Create(5, 5, Direction.Up, 3);

        // Act
        _gameLogic.DoStep();

        // Assert
        var snakeBody = _gameData.Snake.Body.ToArray();
        Assert.That(snakeBody[0], Is.EqualTo(new Pixel(5, 6)));
        Assert.That(snakeBody[1], Is.EqualTo(new Pixel(5, 5)));
        Assert.That(snakeBody[2], Is.EqualTo(new Pixel(5, 4)));
    }

    [Test]
    public void DoStep_SnakeDirectionIsDown_SnakeMovedToDown()
    {
        // Arrange  
        _gameData.Snake = SnakeFactory.Create(5, 5, Direction.Down, 3);

        // Act
        _gameLogic.DoStep();

        // Assert
        var snakeBody = _gameData.Snake.Body.ToArray();
        Assert.That(snakeBody[0], Is.EqualTo(new Pixel(5, 4)));
        Assert.That(snakeBody[1], Is.EqualTo(new Pixel(5, 5)));
        Assert.That(snakeBody[2], Is.EqualTo(new Pixel(5, 6)));
    }
}