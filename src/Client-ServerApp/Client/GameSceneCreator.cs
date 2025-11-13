namespace Client;

public class GameSceneCreator
{
    private CellObject[,]? _cellObjects;

    public CellObject[,]? GetSceneCellObjects(GameElementsDto gameElementDto)
    {
        _cellObjects = new CellObject[gameElementDto.Height, gameElementDto.Width];

        for (int y = 1; y < gameElementDto.Height - 1; y++)
        {
            for (int x = 1; x < gameElementDto.Width - 1; x++)
            {
                _cellObjects[y, x] = CellObject.Empty;
            }
        }

        WriteWalls(gameElementDto);

        if (gameElementDto.Food != null)
        {
            WriteFood(gameElementDto);
        }

        WriteSnake(gameElementDto);
        return _cellObjects;
    }

    private void WriteWalls(GameElementsDto gameElementDto)
    {
        int width = gameElementDto.Width;
        int height = gameElementDto.Height;

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

    private void WriteFood(GameElementsDto gameElementDto)
    {
        if (gameElementDto.Food is { X: >= 0 } && gameElementDto.Food.X < gameElementDto.Width && gameElementDto.Food.Y >= 0 &&
            gameElementDto.Food.Y < gameElementDto.Height)
        {
            if (_cellObjects != null) _cellObjects[gameElementDto.Food.Y, gameElementDto.Food.X] = CellObject.Food;
        }
    }

    private void WriteSnake(GameElementsDto gameElementDto)
    {
        var snake = gameElementDto.Snake;
        int bodyLength = snake.Length;

        for (int i = 0; i < bodyLength; i++)
        {
            var body = snake[i];

            if (i == bodyLength - 1)
            {
                var headSymbol = gameElementDto.HeadDirection switch
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


    private static CellObject GetSnakeTailSymbol(CoordDto cell, CoordDto nextCell)
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

    private static CellObject GetSnakeBodySymbol(CoordDto cell, CoordDto nextCell, CoordDto prevCell)
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