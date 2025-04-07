using Snake.Logic;

namespace Snake.ConsoleApp;

public class GameRenderer
{
    private char[,]? _previousBuffer;

    public void RenderGame(GameData gameData)
    {
        _previousBuffer = new char[gameData.BoardWidth, gameData.BoardHeight];
        char[,] currentBuffer = new char[gameData.BoardWidth, gameData.BoardHeight];
        ClearBuffer(currentBuffer, gameData.BoardWidth, gameData.BoardHeight);
        WriteWallsToBuffer(gameData.Walls, currentBuffer);
        WriteSnakeToBuffer(gameData.Snake, currentBuffer);
        if (gameData.Food != null)
        {
            WriteFoodToBuffer(gameData.Food, currentBuffer);
        }

        DrawElements(gameData, currentBuffer);
        _previousBuffer = currentBuffer;
        if (gameData.IsGameOver)
        {
            DrawGameOver(gameData);
        }
    }

    private static void WriteWallsToBuffer(List<Cell> walls, char[,] currentBuffer)
    {
        foreach (var wall in walls)
        {
            currentBuffer[wall.X, wall.Y] = GameRenderSymbols.Wall;
        }
    }

    private static void WriteSnakeToBuffer(Logic.Snake snake, char[,] currentBuffer)
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
                currentBuffer[cell.X, cell.Y] = snake.LastStepDirection switch
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
                currentBuffer[cell.X, cell.Y] = GetSnakeBodySymbol(cell, nextCell, prevCell);
            }
            else if (i == 0)
            {
                if (nextCell != null)
                {
                    currentBuffer[cell.X, cell.Y] = GetSnakeTailSymbol(cell, nextCell);
                }
            }

            i++;
        }
    }

    private static char GetSnakeBodySymbol(Cell cell, Cell nextCell, Cell prevCell)
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

    private static char GetSnakeTailSymbol(Cell cell, Cell nextCell)
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

    private static void WriteFoodToBuffer(Cell food, char[,] currentBuffer)
    {
        currentBuffer[food.X, food.Y] = GameRenderSymbols.Food;
    }

    private void DrawElements(GameData gameData, char[,] currentBuffer)
    {
        for (int i = 0; i < gameData.BoardWidth; i++)
        {
            for (int j = 0; j < gameData.BoardHeight; j++)
            {
                if (_previousBuffer == null || currentBuffer[i, j] != _previousBuffer[i, j])
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(currentBuffer[i, j]);
                }
            }
        }

        Console.SetCursorPosition(0, gameData.BoardHeight + 1);
        Console.WriteLine("Игра 'Змейка");
        Console.WriteLine($"Количество ходов: {gameData.StepCount}");
        Console.WriteLine($"Количество очков: {gameData.PointCount}");
    }

    private static void DrawGameOver(GameData gameData)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(gameData.BoardWidth / 2 - 4, gameData.BoardHeight);
        Console.WriteLine("Game Over!");
    }

    private static void ClearBuffer(char[,] buffer, int width, int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                buffer[i, j] = ' ';
            }
        }
    }
}