namespace Snake;

public class GameFieldHelper
{
    private readonly GameData _gameData;

    public GameFieldHelper(GameData gameData)
    {
        _gameData = gameData;
    }

    public Pixel[] GetFreeField()
    {
        List<Pixel> fields = new List<Pixel>();
        for (int x = 0; x < _gameData.BoardWidth; x++)
        {
            for (int y = 0; y < _gameData.BoardHeight; y++)
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