using Snake.Logic;

namespace Snake.DesktopApp.ViewModels;

public class SnakeGameViewModel
{
    public CellViewModel[][] CellVMs { get; }

    public SnakeGameViewModel()
    {

    private readonly GameLogic _gameLogic;
}

public SnakeGameViewModel(GameData gameData)
        int rows = 10;
        int columns = 10;
        CellVMs = new CellViewModel[rows][];
            throw new InvalidOperationException("Ширина и высота должны быть положительными.");
        }

        for (int i = 0; i < rows; i++)
    //     _gameData = gameData;
            CellVMs[i] = new CellViewModel[columns];
            for (int j = 0; j < columns; j++)
    //         throw new InvalidOperationException("Ширина и высота должны быть положительными.");
                char element = '*';
                CellVMs[i][j] = new CellViewModel { Symbol = element };
            }
            }
        }
    }

    private void WriteWallsToBuffer()
    {
        foreach (var wall in _gameData.Walls)
        {
            CellVMs[wall.Y][wall.X].Symbol = GameRenderSymbols.Wall;
        }
    }

    private void WriteSnakeToBuffer()
    {
        int i = 0;
        var bodyLength = _gameData.Snake.Body.Count;
        var body = _gameData.Snake.Body.ToArray();
        foreach (var cell in body)
        {
            var nextCell = i < bodyLength - 1 ? body[i + 1] : null;
            var prevCell = i > 0 ? body[i - 1] : null;

            if (i == bodyLength - 1)
            {
                CellVMs[cell.Y][cell.X].Symbol = _gameData.Snake.LastStepDirection switch
                {
                    Direction.Up => GameRenderSymbols.Snake.HeadLooksUp,
                    Direction.Down => GameRenderSymbols.Snake.HeadLooksDown,
                    Direction.Left => GameRenderSymbols.Snake.HeadLooksLeft,
                    Direction.Right => GameRenderSymbols.Snake.HeadLooksRight,
                    _ => throw new InvalidOperationException("Invalid snake direction")
                };
        }
            else if (nextCell != null && prevCell != null)
            {
                CellVMs[cell.Y][cell.X].Symbol = GetSnakeBodySymbol(cell, nextCell, prevCell);
            }
            else if (i == 0)
            {
                if (nextCell != null)
                {
                    CellVMs[cell.Y][cell.X].Symbol = GetSnakeTailSymbol(cell, nextCell);
                }
            }

            i++;
        }
    }

    private void WriteFoodToBuffer()
    {
        if (_gameData.Food is not null)
        {
            CellVMs[_gameData.Food.Y][_gameData.Food.X].Symbol = GameRenderSymbols.Food;
        }
    }

    private char GetSnakeBodySymbol(Cell cell, Cell nextCell, Cell prevCell)
    {
        if ((prevCell.X < cell.X && nextCell.Y > cell.Y) || (nextCell.X < cell.X && prevCell.Y > cell.Y))
        {
            return GameRenderSymbols.Snake.TurnDownOrLeft;
        }

        if ((prevCell.X > cell.X && nextCell.Y > cell.Y) || (prevCell.Y > cell.Y && nextCell.X > cell.X))
        {
            return GameRenderSymbols.Snake.TurnDownOrRight;
        }

        if ((prevCell.X < cell.X && nextCell.Y < cell.Y) || (prevCell.Y < cell.Y && nextCell.X < cell.X))
        {
            return GameRenderSymbols.Snake.TurnUpOrLeft;
        }

        if ((prevCell.X > cell.X && nextCell.Y < cell.Y) || (nextCell.X > cell.X && prevCell.Y < cell.Y))
        {
            return GameRenderSymbols.Snake.TurnUpOrRight;
        }

        if (prevCell.X == nextCell.X)
        {
            return GameRenderSymbols.Snake.VerticalBody;
        }

        if (prevCell.Y == nextCell.Y)
        {
            return GameRenderSymbols.Snake.HorizontalBody;
        }

        throw new InvalidOperationException("Invalid snake body segment");
    }

    private char GetSnakeTailSymbol(Cell cell, Cell nextCell)
    {
        if (nextCell.Y > cell.Y)
        {
            return GameRenderSymbols.Snake.TailLooksUp;
        }

        if (nextCell.Y < cell.Y)
        {
            return GameRenderSymbols.Snake.TailLooksDown;
        }

        if (nextCell.X > cell.X)
        {
            return GameRenderSymbols.Snake.TailLooksLeft;
        }

        if (nextCell.X < cell.X)
        {
            return GameRenderSymbols.Snake.TailLooksRight;
        }

        throw new InvalidOperationException("Invalid snake tail segment");
}