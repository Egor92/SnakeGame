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

    [TestCase(Direction.Right, Direction.Left, Direction.Right)]
    [TestCase(Direction.Left, Direction.Right, Direction.Left)]
    [TestCase(Direction.Down, Direction.Up, Direction.Down)]
    [TestCase(Direction.Up, Direction.Down, Direction.Up)]
    public void ChangeDirection_NewDirectionIsOpposite_DirectionIsNotChanged(Direction initialDirection,
        Direction newDirection, Direction expectedDirection)
    {
        // Arrange  
        _gameData.Snake = SnakeFactory.Create(5, 5, initialDirection, 3);

        // Act
        _gameLogic.ChangeDirection(newDirection);

        // Assert
        Assert.That(_gameData.Snake.Direction, Is.EqualTo(expectedDirection));
    }

    [TestCase(Direction.Right, Direction.Up, Direction.Up)]
    [TestCase(Direction.Right, Direction.Down, Direction.Down)]
    [TestCase(Direction.Left, Direction.Up, Direction.Up)]
    [TestCase(Direction.Left, Direction.Down, Direction.Down)]
    [TestCase(Direction.Up, Direction.Left, Direction.Left)]
    [TestCase(Direction.Up, Direction.Right, Direction.Right)]
    [TestCase(Direction.Down, Direction.Left, Direction.Left)]
    [TestCase(Direction.Down, Direction.Right, Direction.Right)]
    public void ChangeDirection_NewDirectionIsDifferent_DirectionIsChanged(Direction initialDirection,
        Direction newDirection, Direction expectedDirection)
    {
        // Arrange  
        _gameData.Snake = SnakeFactory.Create(5, 5, initialDirection, 3);

        // Act
        _gameLogic.ChangeDirection(newDirection);

        // Assert
        Assert.That(_gameData.Snake.Direction, Is.EqualTo(expectedDirection));
    }

    [TestCase(Direction.Right, Direction.Right, Direction.Right)]
    [TestCase(Direction.Left, Direction.Left, Direction.Left)]
    [TestCase(Direction.Up, Direction.Up, Direction.Up)]
    [TestCase(Direction.Down, Direction.Down, Direction.Down)]
    public void ChangeDirection_NewDirectionIsSame_DirectionIsNotChanged(Direction initialDirection,
        Direction newDirection, Direction expectedDirection)
    {
        // Arrange  
        _gameData.Snake = SnakeFactory.Create(5, 5, initialDirection, 3);

        // Act
        _gameLogic.ChangeDirection(newDirection);

        // Assert
        Assert.That(_gameData.Snake.Direction, Is.EqualTo(expectedDirection));
    }
    
    [Test]
    public void DoStep_WallInFront_GameOver()
    {
        // Arrange  
        _gameData.Snake = SnakeFactory.Create(1, 2, Direction.Left, 3);

        // Act
        _gameLogic.DoStep();
        if (_gameLogic.CheckCollisions())
        {
            _gameData.IsGameOver = true;
        }
        // Assert
        Assert.That(_gameData.IsGameOver, Is.True);
    }
}