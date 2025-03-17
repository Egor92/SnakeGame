namespace Snake.Tests;

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
        _gameFieldHelper = new GameFieldHelper(_gameData);
    }

    [Test]
    public void GetFreeField_FieldFree_WriteIntoArray()
    {
        // Arrange  
        _gameData.Snake = SnakeBuilder.Create(2, 1, Direction.Right)
                                      .Grow(1)
                                      .Build();

        // Act
        var fieldsForFood = _gameFieldHelper.GetFreeField();

        // Assert
        Assert.That(fieldsForFood, Is.EquivalentTo(new[] { new Pixel(3, 1), new Pixel(4, 1) }));
    }

    [Test]
    public void GetFreeField_FieldsOccupied_ArrayEmpty()
    {
        // Arrange  
        _gameData.Snake = SnakeBuilder.Create(5, 1, Direction.Right)
                                      .Grow(4)
                                      .Build();

        // Act
        var fieldsForFood = _gameFieldHelper.GetFreeField();

        // Assert
        Assert.That(fieldsForFood, Is.Empty);
    }
}