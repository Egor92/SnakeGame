namespace Client;

public class GameSceneCreator
{
    private CellObject[,]? _cellObjects;

    public CellObject[,]? GetSceneCellObjects(GameElements gameElement)
    {
        _cellObjects = new CellObject[gameElement.Height, gameElement.Width];

        for (int y = 1; y < gameElement.Height - 1; y++)
        {
            for (int x = 1; x < gameElement.Width - 1; x++)
            {
                _cellObjects[y, x] = CellObject.Empty;
            }
        }

        WriteWalls(gameElement);

        if (gameElement.Food != null)
        {
            WriteFood(gameElement);
        }

        WriteSnake(gameElement);
        return _cellObjects;
    }

    private void WriteWalls(GameElements gameElement)
    {
        int width = gameElement.Width;
        int height = gameElement.Height;

        for (int x = 0; x < width; x++)
        {
            if (_cellObjects != null)
            {
                _cellObjects[0, x] = CellObject.Wall;
                _cellObjects[height - 1, x] = CellObject.Wall;
            }
        }

        for (int y = 0; y < height; y++)
        {
            if (_cellObjects != null)
            {
                _cellObjects[y, 0] = CellObject.Wall;
                _cellObjects[y, width - 1] = CellObject.Wall;
            }
        }
    }

    private void WriteFood(GameElements gameElement)
    {
        if (gameElement.Food is { X: >= 0 } && gameElement.Food.X < gameElement.Width && gameElement.Food.Y >= 0 &&
            gameElement.Food.Y < gameElement.Height)
        {
            if (_cellObjects != null) _cellObjects[gameElement.Food.Y, gameElement.Food.X] = CellObject.Food;
        }
    }

    private void WriteSnake(GameElements gameElement)
    {
        var snake = gameElement.Snake;
        int bodyLength = snake.Length;

        for (int i = 0; i < bodyLength; i++)
        {
            var body = snake[i];

            if (i == bodyLength - 1)
            {
                var headSymbol = gameElement.HeadDirection switch
                {
                    "Up" => CellObject.SnakeHeadLooksUp,
                    "Down" => CellObject.SnakeHeadLooksDown,
                    "Left" => CellObject.SnakeHeadLooksLeft,
                    "Right" => CellObject.SnakeHeadLooksRight,
                    _ => CellObject.SnakeHeadLooksRight
                };
                if (_cellObjects != null) _cellObjects[body.Y, body.X] = headSymbol;
            }
            else if (i == 0)
            {
                var nextCell = snake[i + 1];
                if (_cellObjects != null) _cellObjects[body.Y, body.X] = GetSnakeTailSymbol(body, nextCell);
            }
            else
            {
                var prevCell = snake[i - 1];
                var nextCell = snake[i + 1];
                if (_cellObjects != null) _cellObjects[body.Y, body.X] = GetSnakeBodySymbol(body, nextCell, prevCell);
            }
        }
    }


    private static CellObject GetSnakeTailSymbol(Coord cell, Coord nextCell)
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

        return CellObject.SnakeTailLooksRight;
    }

    private static CellObject GetSnakeBodySymbol(Coord cell, Coord nextCell, Coord prevCell)
    {
        if (prevCell.Y == cell.Y && nextCell.Y == cell.Y)
        {
            return CellObject.SnakeBodyHorizontal;
        }

        if (prevCell.X == cell.X && nextCell.X == cell.X)
        {
            return CellObject.SnakeBodyVertical;
        }

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

        return CellObject.SnakeBodyHorizontal;
    }
}