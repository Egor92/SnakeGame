namespace Snake;

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
        WriteFoodToBuffer(gameData.Food, currentBuffer);
        DrawElements(gameData, currentBuffer);
        _previousBuffer = currentBuffer;
        if (gameData.IsGameOver)
        {
            DrawGameOver(gameData);
        }
    }

    private static void WriteWallsToBuffer(List<Pixel> walls, char[,] currentBuffer)
    {
        foreach (var wall in walls)
        {
            currentBuffer[wall.X, wall.Y] = GameRenderSymbols.Wall;
        }
    }

    private static void WriteSnakeToBuffer(Snake snake, char[,] currentBuffer)
    {
        int i = 0;
        var bodyLength = snake.Body.Count;
        var body = snake.Body.ToArray();
        foreach (var pixel in body)
        {
            var nextPixel = i < bodyLength - 1 ? body[i + 1] : null;
            var prevPixel = i > 0 ? body[i - 1] : null;

            if (i == bodyLength - 1)
            {
                currentBuffer[pixel.X, pixel.Y] = snake.LastStepDirection switch
                {
                    Direction.Up => GameRenderSymbols.Snake.HeadLooksUp,
                    Direction.Down => GameRenderSymbols.Snake.HeadLooksDown,
                    Direction.Left => GameRenderSymbols.Snake.HeadLooksLeft,
                    Direction.Right => GameRenderSymbols.Snake.HeadLooksRight,
                    _ => throw new InvalidOperationException("Invalid snake direction")
                };
            }
            else if (nextPixel != null && prevPixel != null)
            {
                currentBuffer[pixel.X, pixel.Y] = GetSnakeBodySymbol(pixel, nextPixel, prevPixel);
            }
            else if (i == 0)
            {
                if (nextPixel != null)
                {
                    currentBuffer[pixel.X, pixel.Y] = GetSnakeTailSymbol(pixel, nextPixel);
                }
            }

            i++;
        }
    }

    private static char GetSnakeBodySymbol(Pixel pixel, Pixel nextPixel, Pixel prevPixel)
    {
        if ((prevPixel.X < pixel.X && nextPixel.Y > pixel.Y) || (nextPixel.X < pixel.X && prevPixel.Y > pixel.Y))
        {
            return GameRenderSymbols.Snake.TurnDownOrLeft;
        }

        if ((prevPixel.X > pixel.X && nextPixel.Y > pixel.Y) || (prevPixel.Y > pixel.Y && nextPixel.X > pixel.X))
        {
            return GameRenderSymbols.Snake.TurnDownOrRight;
        }

        if ((prevPixel.X < pixel.X && nextPixel.Y < pixel.Y) || (prevPixel.Y < pixel.Y && nextPixel.X < pixel.X))
        {
            return GameRenderSymbols.Snake.TurnUpOrLeft;
        }

        if ((prevPixel.X > pixel.X && nextPixel.Y < pixel.Y) || (nextPixel.X > pixel.X && prevPixel.Y < pixel.Y))
        {
            return GameRenderSymbols.Snake.TurnUpOrRight;
        }

        if (prevPixel.X == nextPixel.X)
        {
            return GameRenderSymbols.Snake.VerticalBody;
        }

        if (prevPixel.Y == nextPixel.Y)
        {
            return GameRenderSymbols.Snake.HorizontalBody;
        }

        throw new InvalidOperationException("Invalid snake body segment");
    }

    private static char GetSnakeTailSymbol(Pixel pixel, Pixel nextPixel)
    {
        if (nextPixel.Y > pixel.Y)
        {
            return GameRenderSymbols.Snake.TailLooksUp;
        }

        if (nextPixel.Y < pixel.Y)
        {
            return GameRenderSymbols.Snake.TailLooksDown;
        }

        if (nextPixel.X > pixel.X)
        {
            return GameRenderSymbols.Snake.TailLooksLeft;
        }

        if (nextPixel.X < pixel.X)
        {
            return GameRenderSymbols.Snake.TailLooksRight;
        }

        throw new InvalidOperationException("Invalid snake tail segment");
    }

    private static void WriteFoodToBuffer(Pixel food, char[,] currentBuffer)
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