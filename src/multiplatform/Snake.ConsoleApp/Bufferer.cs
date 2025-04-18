using Snake.Logic;

namespace Snake.ConsoleApp;

public class Bufferer
{
    public CellObject[,] GetGameValue(GameData gameData)
    {
        int width = gameData.BoardWidth;
        int height = gameData.BoardHeight;

        CellObject[,] cellObjects = new CellObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cellObjects[x, y] = CellObject.Empty;
            }
        }

        WriteWallsToBuffer(gameData.Walls, cellObjects);

        WriteSnakeToBuffer(gameData.Snake, cellObjects);

        if (gameData.Food != null)
        {
            WriteFoodToBuffer(gameData.Food, cellObjects);
        }

        return cellObjects;
    }

    private void WriteWallsToBuffer(List<Cell> walls, CellObject[,] cellObjects)
    {
        foreach (var wall in walls)
        {
            cellObjects[wall.X, wall.Y] = CellObject.Wall;
        }
    }

    private void WriteSnakeToBuffer(Logic.Snake snake, CellObject[,] cellObjects)
    {
        int i = 0;
        var bodyLength = snake.Body.Count;
        var body = snake.Body.ToArray();
        foreach (var cell in body)
        {
            var nextCell = i < bodyLength - 1 ? body[i + 1] : null;
            var prevCell = i > 0 ? body[i - 1] : null;

            if (i == bodyLength - 1)
            {
                cellObjects[cell.X, cell.Y] = snake.LastStepDirection switch
                {
                    Direction.Up => CellObject.SnakeHeadLooksUp,
                    Direction.Down => CellObject.SnakeHeadLooksDown,
                    Direction.Left => CellObject.SnakeHeadLooksLeft,
                    Direction.Right => CellObject.SnakeHeadLooksRight,
                    _ => throw new InvalidOperationException("Invalid snake direction")
                };
            }
            else if (nextCell != null && prevCell != null)
            {
                cellObjects[cell.X, cell.Y] = GetSnakeBodySymbol(cell, nextCell, prevCell);
            }
            else if (i == 0)
            {
                if (nextCell != null)
                {
                    cellObjects[cell.X, cell.Y] = GetSnakeTailSymbol(cell, nextCell);
                }
            }

            i++;
        }
    }

    private static CellObject GetSnakeBodySymbol(Cell cell, Cell nextCell, Cell prevCell)
    {
        if ((prevCell.X < cell.X && nextCell.Y > cell.Y) || (nextCell.X < cell.X && prevCell.Y > cell.Y))
        {
            return CellObject.SnakeBodyDownOrLeft;
        }

        if ((prevCell.X > cell.X && nextCell.Y > cell.Y) || (prevCell.Y > cell.Y && nextCell.X > cell.X))
        {
            return CellObject.SnakeBodyDownOrRight;
        }

        if ((prevCell.X < cell.X && nextCell.Y < cell.Y) || (prevCell.Y < cell.Y && nextCell.X < cell.X))
        {
            return CellObject.SnakeBodyUpOrLeft;
        }

        if ((prevCell.X > cell.X && nextCell.Y < cell.Y) || (nextCell.X > cell.X && prevCell.Y < cell.Y))
        {
            return CellObject.SnakeBodyUpOrRight;
        }

        if (prevCell.X == nextCell.X)
        {
            return CellObject.SnakeBodyVertical;
        }

        if (prevCell.Y == nextCell.Y)
        {
            return CellObject.SnakeBodyHorizontal;
        }

        throw new InvalidOperationException("Invalid snake body segment");
    }

    private static CellObject GetSnakeTailSymbol(Cell cell, Cell nextCell)
    {
        if (nextCell.Y > cell.Y)
        {
            return CellObject.SnakeTailLooksUp;
        }

        if (nextCell.Y < cell.Y)
        {
            return CellObject.SnakeTailLooksDown;
        }

        if (nextCell.X > cell.X)
        {
            return CellObject.SnakeTailLooksLeft;
        }

        if (nextCell.X < cell.X)
        {
            return CellObject.SnakeTailLooksRight;
        }

        throw new InvalidOperationException("Invalid snake tail segment");
    }

    private void WriteFoodToBuffer(Cell food, CellObject[,] cellObjects)
    {
        cellObjects[food.X, food.Y] = CellObject.Food;
    }
}