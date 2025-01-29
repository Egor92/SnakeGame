namespace Snake;

public class Pixel
{
    public int X;
    public int Y;

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