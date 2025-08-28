namespace Snake.Logic.Tests;

[TestFixture]
public class GameFieldHelperTests
{
    private GameData _gameData;
    private GameFieldHelper _gameFieldHelper;

    [SetUp]
    public void Setup()
    {
        _gameData = GameDataBuilder.Create()
                                   .SetPlayingFieldSize(width: 6, height: 3)
                                   .CreateWallAroundPlayingField(width: 6, height: 3)
                                   .Build();
        _gameFieldHelper = new GameFieldHelper();
    }

    [Test]
    public void GetFreeCells_GameDataContainsFreeCells_ReturnsFreeCells()
    {
        // Arrange  
        _gameData.Snake = SnakeBuilder.Create(2, 1, Direction.Right)
                                      .Grow(1)
                                      .Build();

        // Act
        var freeCells = _gameFieldHelper.GetFreeCells(_gameData);

        // Assert
        Assert.That(freeCells, Is.EquivalentTo(new[] { new Cell(3, 1), new Cell(4, 1) }));
    }

    [Test]
    public void GetFreeCells_GameDataDoesNotContainFreeCells_ReturnsEmptyArray()
    {
        // Arrange  
        _gameData.Snake = SnakeBuilder.Create(5, 1, Direction.Right)
                                      .Grow(4)
                                      .Build();

        // Act
        var freeCells = _gameFieldHelper.GetFreeCells(_gameData);

        // Assert
        Assert.That(freeCells, Is.Empty);
    }
}