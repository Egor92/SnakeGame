using Snake.Logic;

namespace Snake.DesktopApp.ViewModels;

public class GameSceneCreatorViewModel
{
    public CellObjectViewModel[,] GetSceneCellObjects(GameData gameData)
    {
        int width = gameData.BoardWidth;
        int height = gameData.BoardHeight;

        CellObjectViewModel[,] cellObjects = new CellObjectViewModel[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cellObjects[x, y] = CellObjectViewModel.Empty;
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

    private void WriteWallsToBuffer(List<Cell> walls, CellObjectViewModel[,] cellObjects)
    {
        foreach (var wall in walls)
        {
            cellObjects[wall.X, wall.Y] = CellObjectViewModel.Wall;
        }
    }

    private void WriteSnakeToBuffer(Logic.Snake snake, CellObjectViewModel[,] cellObjects)
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
                    Direction.Up => CellObjectViewModel.SnakeHeadLooksUp,
                    Direction.Down => CellObjectViewModel.SnakeHeadLooksDown,
                    Direction.Left => CellObjectViewModel.SnakeHeadLooksLeft,
                    Direction.Right => CellObjectViewModel.SnakeHeadLooksRight,
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

    private static CellObjectViewModel GetSnakeBodySymbol(Cell cell, Cell nextCell, Cell prevCell)
    {
        if ((prevCell.X < cell.X && nextCell.Y > cell.Y) || (nextCell.X < cell.X && prevCell.Y > cell.Y))
        {
            return CellObjectViewModel.SnakeBodyDownOrLeft;
        }

        if ((prevCell.X > cell.X && nextCell.Y > cell.Y) || (prevCell.Y > cell.Y && nextCell.X > cell.X))
        {
            return CellObjectViewModel.SnakeBodyDownOrRight;
        }

        if ((prevCell.X < cell.X && nextCell.Y < cell.Y) || (prevCell.Y < cell.Y && nextCell.X < cell.X))
        {
            return CellObjectViewModel.SnakeBodyUpOrLeft;
        }

        if ((prevCell.X > cell.X && nextCell.Y < cell.Y) || (nextCell.X > cell.X && prevCell.Y < cell.Y))
        {
            return CellObjectViewModel.SnakeBodyUpOrRight;
        }

        if (prevCell.X == nextCell.X)
        {
            return CellObjectViewModel.SnakeBodyVertical;
        }

        if (prevCell.Y == nextCell.Y)
        {
            return CellObjectViewModel.SnakeBodyHorizontal;
        }

        throw new InvalidOperationException("Invalid snake body segment");
    }

    private static CellObjectViewModel GetSnakeTailSymbol(Cell cell, Cell nextCell)
    {
        if (nextCell.Y > cell.Y)
        {
            return CellObjectViewModel.SnakeTailLooksUp;
        }

        if (nextCell.Y < cell.Y)
        {
            return CellObjectViewModel.SnakeTailLooksDown;
        }

        if (nextCell.X > cell.X)
        {
            return CellObjectViewModel.SnakeTailLooksLeft;
        }

        if (nextCell.X < cell.X)
        {
            return CellObjectViewModel.SnakeTailLooksRight;
        }

        throw new InvalidOperationException("Invalid snake tail segment");
    }

    private void WriteFoodToBuffer(Cell food, CellObjectViewModel[,] cellObjects)
    {
        cellObjects[food.X, food.Y] = CellObjectViewModel.Food;
    }
}