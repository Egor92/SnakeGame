namespace Snake;

public class GameRenderer
{
    private char[,]? _previousBuffer;

    public void RenderGame(GameData gameData)
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        _previousBuffer = new char[gameData.BoardWidth, gameData.BoardHeight];
        char[,] currentBuffer = new char[gameData.BoardWidth, gameData.BoardHeight];
        ClearBuffer(currentBuffer, gameData.BoardWidth, gameData.BoardHeight);
        WriteWallsToBuffer(gameData, currentBuffer);
        WriteSnakeToBuffer(gameData, currentBuffer);
        WriteFoodToBuffer(gameData, currentBuffer);
        DrawElements(gameData, currentBuffer);
        _previousBuffer = currentBuffer;
        if (gameData.IsGameOver)
        {
            DrawGameOver(gameData);
        }
    }

    private void WriteWallsToBuffer(GameData gameData, char[,] currentBuffer)
    {
        foreach (var wall in gameData.Walls)
        {
            currentBuffer[wall.X, wall.Y] = GameRenderSymbols.Wall;
        }
    }

    private void WriteSnakeToBuffer(GameData gameData, char[,] currentBuffer)
    {
        int i = 0;
        var body = gameData.Snake.Body.Count;
        var bodyArray = gameData.Snake.Body.ToArray();
        foreach (var pixel in bodyArray)
        {
            var nextPixel = i < body - 1 ? bodyArray[i + 1] : null;
            var prevPixel = i > 0 ? bodyArray[i - 1] : null;

            if (i == body - 1)
            {
                currentBuffer[pixel.X, pixel.Y] = gameData.Snake.Direction switch
                {
                    Direction.Up => GameRenderSymbols.HeadLooksUp,
                    Direction.Down => GameRenderSymbols.HeadLooksDown,
                    Direction.Left => GameRenderSymbols.HeadLooksLeft,
                    Direction.Right => GameRenderSymbols.HeadLooksRight,
                    _ => throw new InvalidOperationException("Invalid snake direction")
                };
            }
            else if (nextPixel != null && prevPixel != null)
            {
                currentBuffer[pixel.X, pixel.Y] = GetSnakeBodyToBuffer(pixel, nextPixel, prevPixel);
            }
            else if (i == 0)
            {
                if (nextPixel != null)
                {
                    currentBuffer[pixel.X, pixel.Y] = GetSnakeTailToBuffer(pixel, nextPixel);
                }
            }

            i++;
        }
    }

    private char GetSnakeBodyToBuffer(Pixel pixel, Pixel nextPixel, Pixel prevPixel)
    {
        if ((prevPixel.X < pixel.X && nextPixel.Y > pixel.Y) || (nextPixel.X < pixel.X && prevPixel.Y > pixel.Y))
        {
            return GameRenderSymbols.TurnDownOrLeft;
        }

        if ((prevPixel.X > pixel.X && nextPixel.Y > pixel.Y) || (prevPixel.Y > pixel.Y && nextPixel.X > pixel.X))
        {
            return GameRenderSymbols.TurnDownOrRight;
        }

        if ((prevPixel.X < pixel.X && nextPixel.Y < pixel.Y) || (prevPixel.Y < pixel.Y && nextPixel.X < pixel.X))
        {
            return GameRenderSymbols.TurnUpOrLeft;
        }

        if ((prevPixel.X > pixel.X && nextPixel.Y < pixel.Y) || (nextPixel.X > pixel.X && prevPixel.Y < pixel.Y))
        {
            return GameRenderSymbols.TurnUpOrRight;
        }

        if (prevPixel.X == nextPixel.X)
        {
            return GameRenderSymbols.VerticalBody;
        }

        if (prevPixel.Y == nextPixel.Y)
        {
            return GameRenderSymbols.HorizontalBody;
        }

        throw new InvalidOperationException("Invalid snake body segment");
    }

    private char GetSnakeTailToBuffer(Pixel pixel, Pixel nextPixel)
    {
        if (nextPixel.Y > pixel.Y)
        {
            return GameRenderSymbols.TailLooksUp;
        }

        if (nextPixel.Y < pixel.Y)
        {
            return GameRenderSymbols.TailLooksDown;
        }

        if (nextPixel.X > pixel.X)
        {
            return GameRenderSymbols.TailLooksLeft;
        }

        if (nextPixel.X < pixel.X)
        {
            return GameRenderSymbols.TailLooksRight;
        }

        throw new InvalidOperationException("Invalid snake tail segment");
    }

    private void WriteFoodToBuffer(GameData gameData, char[,] currentBuffer)
    {
        currentBuffer[gameData.Food.X, gameData.Food.Y] = GameRenderSymbols.Food;
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
    }

    private void DrawGameOver(GameData gameData)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(gameData.BoardWidth / 2 - 4, gameData.BoardHeight + 1);
        Console.WriteLine("Game Over!");
    }

    private void ClearBuffer(char[,] buffer, int width, int height)
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