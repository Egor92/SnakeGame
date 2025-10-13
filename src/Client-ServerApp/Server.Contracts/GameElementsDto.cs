namespace Client;

public class GameElementsDto
{
    public int Width { get; set; }
    public int Height { get; set; }
    public CoordDto[] Snake { get; set; } = [];
    public string? HeadDirection { get; set; }
    public CoordDto? Food { get; set; }
    public int PointCount { get; set; }
    public int StepCount { get; set; }
    public bool IsGameOver { get; set; }
}