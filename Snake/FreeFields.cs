namespace Snake;

public class FreeFields
{
    private readonly GameData _gameData;

    public FreeFields(GameData gameData)
    {
        _gameData = gameData;
    }

    public Pixel[] GetFreeField()
    {
        List<Pixel> fields = new List<Pixel>();
        for (int x = 1; x < _gameData.BoardWidth - 1; x++)
        {
            for (int y = 1; y < _gameData.BoardHeight - 1; y++)
            {
                Pixel pixel = new Pixel(x, y);
                if (!_gameData.Walls.Contains(pixel) &&
                    !_gameData.Snake.Body.Contains(pixel))
                {
                    fields.Add(pixel);
                }
            }
        }

        return fields.ToArray();
    }
}