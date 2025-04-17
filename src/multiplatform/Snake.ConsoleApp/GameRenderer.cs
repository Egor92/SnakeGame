using Snake.Logic;

namespace Snake.ConsoleApp;

public class GameRenderer
{
    private char[,]? _previousBuffer;
    private GameData _gameData;

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
            CellObject.Empty => ' ',
            CellObject.Wall => GameRenderSymbols.Wall,
            CellObject.Food => GameRenderSymbols.Food,
            CellObject.SnakeHeadLooksUp => GameRenderSymbols.Snake.HeadLooksUp,
            CellObject.SnakeHeadLooksDown => GameRenderSymbols.Snake.HeadLooksDown,
            CellObject.SnakeHeadLooksLeft => GameRenderSymbols.Snake.HeadLooksLeft,
            CellObject.SnakeHeadLooksRight => GameRenderSymbols.Snake.HeadLooksRight,
            CellObject.SnakeBodyHorizontal => GameRenderSymbols.Snake.HorizontalBody,
            CellObject.SnakeBodyVertical => GameRenderSymbols.Snake.VerticalBody,
            CellObject.SnakeTurnDownOrLeft => GameRenderSymbols.Snake.TurnDownOrLeft,
            CellObject.SnakeTurnDownOrRight => GameRenderSymbols.Snake.TurnDownOrRight,
            CellObject.SnakeTurnUpOrLeft => GameRenderSymbols.Snake.TurnUpOrLeft,
            CellObject.SnakeTurnUpOrRight => GameRenderSymbols.Snake.TurnUpOrRight,
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

        Console.SetCursorPosition(0, _gameData.BoardHeight + 1);
        Console.WriteLine("Игра 'Змейка");
        Console.WriteLine($"Количество ходов: {_gameData.StepCount}");
        Console.WriteLine($"Количество очков: {_gameData.PointCount}");

        if (_gameData.IsGameOver)
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