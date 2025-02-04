namespace Snake;

public class Pixel
{
    public int X { get; set; }
    public int Y { get; set; }

    public Pixel(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"({X};{Y})";
    }
}