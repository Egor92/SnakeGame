namespace Snake;

public class Pixel
{
    public int X { get; set; }
    public int Y { get; set; }

    protected bool Equals(Pixel other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Pixel)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(Pixel? left, Pixel? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Pixel? left, Pixel? right)
    {
        return !Equals(left, right);
    }

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