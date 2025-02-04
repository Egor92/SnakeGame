namespace Snake;

public class GameData
{
    public int BoardWidth { get; set; }
    public int BoardHeight { get; set; }
    public Pixel Food { get; set; }
    public List<Pixel> Walls { get; set; }
    public Snake Snake { get; set; }
    public bool IsGameOver { get; set; }
}