namespace Snake.Tests;

[TestFixture]
public class FreeFieldTest
{
    [Test]
    public void GetFreeField_FieldFree_WriteIntoArray()
    {
        // Arrange  
        GameData _gameData = GameDataBuilder.Create()
                                            .SetPlayingFieldSize(width: 6, height: 3)
                                            .CreateWallAroundPlayingField(width: 6, height: 3)
                                            .Build();

        _gameData.Snake = SnakeBuilder.Create(2, 1, Direction.Right)
                                      .Grow(1)
                                      .Build();

        GameFieldHelper freeFields = new GameFieldHelper(_gameData);

        // Act
        var foodPixel = freeFields.GetFreeField();

        // Assert
        Assert.That(foodPixel, Is.EquivalentTo(new[] { new Pixel(3, 1), new Pixel(4, 1) }));
    }
}