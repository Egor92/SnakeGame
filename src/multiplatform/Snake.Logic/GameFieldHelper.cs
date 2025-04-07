namespace Snake.Logic;

public class GameFieldHelper
{
    public Cell[] GetFreeCells(GameData gameData)
    {
        List<Cell> fields = new List<Cell>();
        for (int x = 0; x < gameData.BoardWidth; x++)
        {
            for (int y = 0; y < gameData.BoardHeight; y++)
            {
                Cell pixel = new Cell(x, y);
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