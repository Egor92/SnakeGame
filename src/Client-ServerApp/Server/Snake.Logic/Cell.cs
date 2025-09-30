namespace client_serverApp.Snake.Logic;

public class Cell
{
    public int X { get; set; }

    public int Y { get; set; }

    protected bool Equals(Cell other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Cell)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(Cell? left, Cell? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Cell? left, Cell? right)
    {
        return !Equals(left, right);
    }

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"({X};{Y})";
    }
}