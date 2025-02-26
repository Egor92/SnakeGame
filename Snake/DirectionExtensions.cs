namespace Snake;

public static class DirectionExtensions
{
    public static Direction GetOpposite(this Direction direction)
    {
        return direction switch
        {
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            Direction.Up => Direction.Down,
            _ => throw new ArgumentOutOfRangeException(nameof(direction),
                                                       direction,
                                                       $"Unexpected direction: {direction}")
        };
    }
}