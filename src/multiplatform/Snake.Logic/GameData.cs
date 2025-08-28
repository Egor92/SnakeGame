namespace Snake.Logic;

public class GameData
{
    public int BoardWidth { get; set; }

    public int BoardHeight { get; set; }

    public Cell? Food { get; set; }

    public required List<Cell> Walls { get; set; }

    public required Snake Snake { get; set; }

    public bool IsGameOver { get; set; }

    public int StepCount { get; set; }

    public int PointCount { get; set; }
}