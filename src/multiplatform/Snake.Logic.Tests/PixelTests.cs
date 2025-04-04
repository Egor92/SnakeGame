namespace Snake.Logic.Tests;

public class PixelTests
{
    [Test]
    public void ComparisonViaDoubleEqualSing_PixelsHaveSameXAndY_ReturnsTrue()
    {
        // Arrange 
        Pixel pixel1 = new Pixel(3, 7);
        Pixel pixel2 = new Pixel(3, 7);

        // Act
        bool areEqual = pixel1 == pixel2;

        // Assert
        Assert.That(areEqual, Is.True);
    }

    [Test]
    public void ListContains_ListContainsPixelAndPixelPassedToContainsHasTheSameXAndY_ReturnsTrue()
    {
        // Arrange 
        List<Pixel> list = new List<Pixel>();
        list.Add(new Pixel(3, 7));

        // Act
        bool contains = list.Contains(new Pixel(3, 7));

        // Assert
        Assert.That(contains, Is.True);
    }
}