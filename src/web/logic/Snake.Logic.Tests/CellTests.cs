namespace Snake.Logic.Tests;

public class CellTests
{
    [Test]
    public void ComparisonViaDoubleEqualSing_CellsHaveSameXAndY_ReturnsTrue()
    {
        // Arrange 
        Cell cell1 = new Cell(3, 7);
        Cell cell2 = new Cell(3, 7);

        // Act
        bool areEqual = cell1 == cell2;

        // Assert
        Assert.That(areEqual, Is.True);
    }

    [Test]
    public void ListContains_ListContainsCellAndCellPassedToContainsHasTheSameXAndY_ReturnsTrue()
    {
        // Arrange 
        List<Cell> list = new List<Cell>();
        list.Add(new Cell(3, 7));

        // Act
        bool contains = list.Contains(new Cell(3, 7));

        // Assert
        Assert.That(contains, Is.True);
    }
}