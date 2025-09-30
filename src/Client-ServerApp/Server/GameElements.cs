namespace client_serverApp;

public class GameElements
{
    public int Width { get; set; }
    public int Height { get; set; }
    public Coord[] Snake { get; set; } = [];
    public string? HeadDirection { get; set; }
    public Coord? Food { get; set; }
    public int PointCount { get; set; }
    public int StepCount { get; set; }
    public bool IsGameOver { get; set; }
}