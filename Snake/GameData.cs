namespace Snake;

public class GameData
{
    public int BoardWidth { get; set; }

    public int BoardHeight { get; set; }

    public Pixel? Food { get; set; }

    public required List<Pixel> Walls { get; set; }

    public required Snake Snake { get; set; }

    public bool IsGameOver { get; set; }

    public int StepCount { get; set; }

    public int PointCount { get; set; }
}