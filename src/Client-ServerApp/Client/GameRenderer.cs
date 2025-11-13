namespace Client;

public class GameRenderer
{
    private char[,]? _previousBuffer;

    public void RenderGame(CellObject[,]? cellObjects, int boardWidth, int boardHeight, int stepCount, int pointCount, bool isGameOver)
    {
        if (cellObjects != null)
        {
            int width = cellObjects.GetLength(1);
            int height = cellObjects.GetLength(0);

            char[,] currentBuffer = new char[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    currentBuffer[y, x] = GetSymbol(cellObjects[y, x]);
                }
            }

            DrawElements(currentBuffer, boardWidth, boardHeight, stepCount, pointCount, isGameOver);
            _previousBuffer = currentBuffer;
        }
    }

    private char GetSymbol(CellObject cellObject)
    {
        return cellObject switch
        {
            CellObject.Empty => GameRenderSymbols.Empty,
            CellObject.Wall => GameRenderSymbols.Wall,
            CellObject.Food => GameRenderSymbols.Food,
            CellObject.SnakeHeadLooksUp => GameRenderSymbols.Snake.HeadLooksUp,
            CellObject.SnakeHeadLooksDown => GameRenderSymbols.Snake.HeadLooksDown,
            CellObject.SnakeHeadLooksLeft => GameRenderSymbols.Snake.HeadLooksLeft,
            CellObject.SnakeHeadLooksRight => GameRenderSymbols.Snake.HeadLooksRight,
            CellObject.SnakeBodyHorizontal => GameRenderSymbols.Snake.HorizontalBody,
            CellObject.SnakeBodyVertical => GameRenderSymbols.Snake.VerticalBody,
            CellObject.SnakeBodyDownOrLeft => GameRenderSymbols.Snake.TurnDownOrLeft,
            CellObject.SnakeBodyDownOrRight => GameRenderSymbols.Snake.TurnDownOrRight,
            CellObject.SnakeBodyUpOrLeft => GameRenderSymbols.Snake.TurnUpOrLeft,
            CellObject.SnakeBodyUpOrRight => GameRenderSymbols.Snake.TurnUpOrRight,
            CellObject.SnakeTailLooksUp => GameRenderSymbols.Snake.TailLooksUp,
            CellObject.SnakeTailLooksDown => GameRenderSymbols.Snake.TailLooksDown,
            CellObject.SnakeTailLooksLeft => GameRenderSymbols.Snake.TailLooksLeft,
            CellObject.SnakeTailLooksRight => GameRenderSymbols.Snake.TailLooksRight,
            _ => throw new InvalidOperationException("Unknown CellObject")
        };
    }

    private void DrawElements(char[,]? currentBuffer, int boardWidth, int boardHeight, int stepCount, int pointCount, bool isGameOver)
    {
        if (currentBuffer != null)
        {
            int height = currentBuffer.GetLength(0);
            int width = currentBuffer.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (_previousBuffer == null || currentBuffer[y, x] != _previousBuffer[y, x])
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(currentBuffer[y, x]);
                    }
                }
            }
        }

        Console.SetCursorPosition(0, boardHeight + 1);
        Console.WriteLine("Игра 'Змейка'");
        Console.WriteLine($"Количество ходов: {stepCount}");
        Console.WriteLine($"Количество очков: {pointCount}");

        if (isGameOver)
        {
            DrawGameOver(boardWidth, boardHeight);
        }
    }

    private static void DrawGameOver(int width, int height)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(width / 2 - 4, height);
        Console.WriteLine("Game Over!");
        Console.ResetColor();
    }
}