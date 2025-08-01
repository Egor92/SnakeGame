using Snake.Logic;
using Snake.DesktopApp.ViewModels;

namespace Snake.DesktopApp.ViewModels;

public class SnakeGameViewModel
{
    public CellViewModel[][] CellVMs { get; }

    public SnakeGameViewModel(GameData gameData)
    {
        int width = gameData.BoardWidth;
        int height = gameData.BoardHeight;
        
        CellVMs = new CellViewModel[height][];
        for (int y = 0; y < height; y++)
        {
            CellVMs[y] = new CellViewModel[width];
            for (int x = 0; x < width; x++)
            {
                CellVMs[y][x] = new CellViewModel { Symbol = ' ' };
            }
        }
        
        WriteWallsToBuffer(gameData.Walls);
        WriteSnakeToBuffer(gameData.Snake);
        if (gameData.Food != null)
        {
            WriteFoodToBuffer(gameData.Food);
        }
    }

    private void WriteWallsToBuffer(List<Cell> walls)
    {
        foreach (var wall in walls)
        {
            CellVMs[wall.Y][wall.X].Symbol = SnakeGameSymbolsViewModel.Wall;
        }
    }

    private void WriteSnakeToBuffer(Logic.Snake snake)
    {
        int i = 0;
        var body = snake.Body.ToArray();
        var bodyLength = body.Length;

        foreach (var cell in body)
        {
            var nextCell = i < bodyLength - 1 ? body[i + 1] : null;
            var prevCell = i > 0 ? body[i - 1] : null;

            if (i == bodyLength - 1) // Голова
            {
                CellVMs[cell.Y][cell.X].Symbol = snake.LastStepDirection switch
                {
                    Direction.Up => SnakeGameSymbolsViewModel.Snake.HeadLooksUp,
                    Direction.Down => SnakeGameSymbolsViewModel.Snake.HeadLooksDown,
                    Direction.Left => SnakeGameSymbolsViewModel.Snake.HeadLooksLeft,
                    Direction.Right => SnakeGameSymbolsViewModel.Snake.HeadLooksRight,
                    _ => throw new InvalidOperationException("Invalid snake direction")
                };
            }
            else if (nextCell != null && prevCell != null) // Тело
            {
                CellVMs[cell.Y][cell.X].Symbol = GetSnakeBodySymbol(cell, nextCell, prevCell);
            }
            else if (i == 0 && nextCell != null) // Хвост
            {
                CellVMs[cell.Y][cell.X].Symbol = GetSnakeTailSymbol(cell, nextCell);
            }

            i++;
        }
    }

    private static char GetSnakeBodySymbol(Cell cell, Cell nextCell, Cell prevCell)
    {
        if ((prevCell.X < cell.X && nextCell.Y > cell.Y) || (nextCell.X < cell.X && prevCell.Y > cell.Y))
        {
            return SnakeGameSymbolsViewModel.Snake.TurnDownOrLeft;
        }

        if ((prevCell.X > cell.X && nextCell.Y > cell.Y) || (prevCell.Y > cell.Y && nextCell.X > cell.X))
        {
            return SnakeGameSymbolsViewModel.Snake.TurnDownOrRight;
        }

        if ((prevCell.X < cell.X && nextCell.Y < cell.Y) || (prevCell.Y < cell.Y && nextCell.X < cell.X))
        {
            return SnakeGameSymbolsViewModel.Snake.TurnUpOrLeft;
        }

        if ((prevCell.X > cell.X && nextCell.Y < cell.Y) || (nextCell.X > cell.X && prevCell.Y < cell.Y))
        {
            return SnakeGameSymbolsViewModel.Snake.TurnUpOrRight;
        }

        if (prevCell.X == nextCell.X)
        {
            return SnakeGameSymbolsViewModel.Snake.VerticalBody;
        }

        if (prevCell.Y == nextCell.Y)
        {
            return SnakeGameSymbolsViewModel.Snake.HorizontalBody;
        }

        throw new InvalidOperationException("Invalid snake body segment");
    }

    private static char GetSnakeTailSymbol(Cell cell, Cell nextCell)
    {
        if (nextCell.Y > cell.Y)
        {
            return SnakeGameSymbolsViewModel.Snake.TailLooksUp;
        }

        if (nextCell.Y < cell.Y)
        {
            return SnakeGameSymbolsViewModel.Snake.TailLooksDown;
        }

        if (nextCell.X > cell.X)
        {
            return SnakeGameSymbolsViewModel.Snake.TailLooksLeft;
        }

        if (nextCell.X < cell.X)
        {
            return SnakeGameSymbolsViewModel.Snake.TailLooksRight;
        }

        throw new InvalidOperationException("Invalid snake tail segment");
    }

    private void WriteFoodToBuffer(Cell food)
    {
        CellVMs[food.Y][food.X].Symbol = SnakeGameSymbolsViewModel.Food;
    }
}