using Snake.Logic;

namespace Snake.ConsoleApp;

public class GameRenderer(GameData gameData)
{
    private char[,]? _previousBuffer;

    public void RenderGame(CellObject[,] cellObjects)
    {
        int width = cellObjects.GetLength(0);
        int height = cellObjects.GetLength(1);

        char[,] currentBuffer = new char[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                currentBuffer[x, y] = GetSymbol(cellObjects[x, y]);
            }
        }

        DrawElements(currentBuffer, width, height);

        _previousBuffer = currentBuffer;
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

    private void DrawElements(char[,] currentBuffer, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (_previousBuffer == null || currentBuffer[x, y] != _previousBuffer[x, y])
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(currentBuffer[x, y]);
                }
            }
        }

        Console.SetCursorPosition(0, gameData.BoardHeight + 1);
        Console.WriteLine("Игра 'Змейка");
        Console.WriteLine($"Количество ходов: {gameData.StepCount}");
        Console.WriteLine($"Количество очков: {gameData.PointCount}");

        if (gameData.IsGameOver)
        {
            DrawGameOver(width, height);
        }
    }

    private static void DrawGameOver(int width, int height)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(width / 2 - 4, height);
        Console.WriteLine("Game Over!");
    }
}