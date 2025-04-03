namespace Snake.Logic;

public class GameFieldHelper
{
    public Pixel[] GetFreePixels(GameData gameData)
    {
        List<Pixel> fields = new List<Pixel>();
        for (int x = 0; x < gameData.BoardWidth; x++)
        {
            for (int y = 0; y < gameData.BoardHeight; y++)
            {
                Pixel pixel = new Pixel(x, y);
                if (!gameData.Walls.Contains(pixel) &&
                    !gameData.Snake.Body.Contains(pixel))
                {
                    fields.Add(pixel);
                }
            }
        }

        return fields.ToArray();
    }
}