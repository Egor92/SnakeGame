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
        _gameData.Snake = SnakeBuilder.Create(5, 5, Direction.Right)
                                      .Grow(2)
                                      .Build();

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
        _gameData.Snake = SnakeBuilder.Create(5, 5, Direction.Left)
                                      .Grow(2)
                                      .Build();

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
        _gameData.Snake = SnakeBuilder.Create(5, 5, Direction.Up)
                                      .Grow(2)
                                      .Build();

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
        _gameData.Snake = SnakeBuilder.Create(5, 5, Direction.Down)
                                      .Grow(2)
                                      .Build();

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
    public void ChangeDirection_NewDirectionIsOpposite_DirectionIsNotChanged(
        Direction initialDirection,
        Direction newDirection,
        Direction expectedDirection)
    {
        // Arrange  
        _gameData.Snake = SnakeBuilder.Create(5, 5, initialDirection)
                                      .Grow(2)
                                      .Build();

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
    public void ChangeDirection_NewDirectionIsDifferent_DirectionIsChanged(
        Direction initialDirection,
        Direction newDirection,
        Direction expectedDirection)
    {
        // Arrange  
        _gameData.Snake = SnakeBuilder.Create(5, 5, initialDirection)
                                      .Grow(2)
                                      .Build();

        // Act
        _gameLogic.ChangeDirection(newDirection);

        // Assert
        Assert.That(_gameData.Snake.Direction, Is.EqualTo(expectedDirection));
    }

    [TestCase(Direction.Right, Direction.Right, Direction.Right)]
    [TestCase(Direction.Left, Direction.Left, Direction.Left)]
    [TestCase(Direction.Up, Direction.Up, Direction.Up)]
    [TestCase(Direction.Down, Direction.Down, Direction.Down)]
    public void ChangeDirection_NewDirectionIsSame_DirectionIsNotChanged(
        Direction initialDirection,
        Direction newDirection,
        Direction expectedDirection)
    {
        // Arrange  
        _gameData.Snake = SnakeBuilder.Create(5, 5, initialDirection)
                                      .Grow(2)
                                      .Build();

        // Act
        _gameLogic.ChangeDirection(newDirection);

        // Assert
        Assert.That(_gameData.Snake.Direction, Is.EqualTo(expectedDirection));
    }

    [Test]
    public void ChangeDirection_СallTwiceAndLastDirectionIsOpposite_DoNotChangeDirection()
    {
        // Arrange
        _gameData.Snake = SnakeBuilder.Create(5, 5, Direction.Right)
                                      .Grow(2)
                                      .Build();

        // Act
        _gameLogic.ChangeDirection(Direction.Up);
        _gameLogic.ChangeDirection(Direction.Left);

        // Assert
        Assert.That(_gameData.Snake.Direction, Is.EqualTo(Direction.Right));
    }

  
    [TestCase(Direction.Right, new[] { Direction.Right, Direction.Down }, Direction.Down)]
    [TestCase(Direction.Right, new[] { Direction.Up, Direction.Down }, Direction.Down)]
    [TestCase(Direction.Right, new[] { Direction.Left, Direction.Down }, Direction.Down)]
    public void ChangeDirection_СallTwiceAndLastDirectionIsNotOpposite_ApplyLastDirection(
        Direction initialDirection,
        Direction[] direction,
        Direction expectedDirection)
    {
        // Arrange
        _gameData.Snake = SnakeBuilder.Create(5, 5, initialDirection)
                                      .Grow(2)
                                      .Build();

        // Act
        for (int i = 0; i < direction.Length; i++)
        {
            _gameLogic.ChangeDirection(direction[i]);
        }

        // Assert
        Assert.That(_gameData.Snake.Direction, Is.EqualTo(expectedDirection));
    }

    [Test]
    public void DoStep_WallIsAhead_GameOver()
    {
        // Arrange  
        _gameData.Snake = SnakeBuilder.Create(1, 2, Direction.Left)
                                      .Grow(2)
                                      .Build();
        _gameData.Walls = new List<Pixel>()
        {
            new Pixel(0, 2)
        };

        // Act
        _gameLogic.DoStep();

        // Assert
        Assert.That(_gameData.IsGameOver, Is.True);
    }

    [Test]
    public void DoStep_FoodIsAhead_SnakeGrowsOnePixel()
    {
        // Arrange  
        var initialSnakeLength = 3;

        _gameData.Snake = SnakeBuilder.Create(5, 5, Direction.Right)
                                      .Grow(initialSnakeLength - 1)
                                      .Build();
        _gameData.Food = new Pixel(6, 5);

        // Act
        _gameLogic.DoStep();

        // Assert
        var finalSnakeLength = _gameData.Snake.Body.Count;
        var snakeLengthDifference = finalSnakeLength - initialSnakeLength;
        Assert.That(snakeLengthDifference, Is.EqualTo(1));
    }

    [Test]
    public void DoStep_SnakeBodyIsAhead_GameOver()
    {
        // Arrange  
        _gameData.Snake = SnakeBuilder.Create(5, 5, Direction.Down)
                                      .Grow(Direction.Right, 1)
                                      .Grow(Direction.Down, 1)
                                      .Grow(Direction.Left, 2)
                                      .Build();

        // Act
        _gameLogic.DoStep();

        // Assert
        Assert.That(_gameData.IsGameOver, Is.True);
    }

    [Test]
    public void DoStep_FoodIsAhead_GameIsNotOver()
    {
        // Arrange  
        _gameData.Snake = SnakeBuilder.Create(5, 5, Direction.Right)
                                      .Grow(2)
                                      .Build();
        _gameData.Food = new Pixel(6, 5);

        // Act
        _gameLogic.DoStep();

        // Assert
        Assert.That(_gameData.IsGameOver, Is.False);
    }

    [TestCase(1, 0, 1)]
    [TestCase(2, 3, 5)]
    [TestCase(3, 6, 9)]
    public void DoStep_CallSeveralTimes_StepCountIncreasedByDoStepInvocations(
        int doStepInvocationCount,
        int initialStepCount,
        int expectedStepCount)
    {
        // Arrange  
        _gameData.Snake = SnakeBuilder.Create(5, 5, Direction.Right)
                                      .Grow(2)
                                      .Build();
        _gameData.StepCount = initialStepCount;

        // Act
        for (int i = 0; i < doStepInvocationCount; i++)
        {
            _gameLogic.DoStep();
        }

        // Assert
        var finiteStepCount = _gameData.StepCount;
        Assert.That(finiteStepCount, Is.EqualTo(expectedStepCount));
    }

    [Test]
    public void DoStep_FoodIsAhead_CountPointsChangedToOneHundred()
    {
        // Arrange  
        _gameData.Snake = SnakeBuilder.Create(5, 5, Direction.Right)
                                      .Grow(2)
                                      .Build();
        _gameData.Food = new Pixel(6, 5);
        var initialNumberOfPoints = 0;

        // Act
        _gameLogic.DoStep();

        // Assert
        var finiteNumberOfPoints = _gameData.PointCount;
        var pointsDifference = finiteNumberOfPoints - initialNumberOfPoints;
        Assert.That(pointsDifference, Is.EqualTo(100));
    }
}