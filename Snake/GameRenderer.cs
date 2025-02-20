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
        DefineElementsWalls(gameData, currentBuffer);
        DefineElementsSnake(gameData, currentBuffer);
        DefineElementFood(gameData, currentBuffer);
        DrawElements(gameData, currentBuffer);
        _previousBuffer = currentBuffer;
        if (gameData.IsGameOver)
        {
            DrawGameOver(gameData);
        }
    }

    private void DefineElementsWalls(GameData gameData, char[,] currentState)
    {
        foreach (var wall in gameData.Walls)
        {
            currentState[wall.X, wall.Y] = SnakeSymbols._wall;
        }
    }

    private void DefineElementsSnake(GameData gameData, char[,] currentState)
    {
        int n = 0;
        var body = gameData.Snake.Body.Count;
        var bodyArray = gameData.Snake.Body.ToArray();
        while (n < body)
        {
            var pixel = bodyArray[n];
            var nextPixel = n < body - 1 ? bodyArray[n + 1] : null;
            var prevPixel = n > 0 ? bodyArray[n - 1] : null;

            if (n == body - 1)
            {
                currentState[pixel.X, pixel.Y] = gameData.Snake.Direction switch
                {
                    Direction.Up => SnakeSymbols._headLooksUp,
                    Direction.Down => SnakeSymbols._headLooksDown,
                    Direction.Left => SnakeSymbols._headLooksLeft,
                    Direction.Right => SnakeSymbols._headLooksRight,
                    _ => throw new InvalidOperationException("Invalid snake direction")
                };
            }
            else if (nextPixel != null && prevPixel != null)
            {
                DefineElementsSnakeBody(currentState, pixel, nextPixel, prevPixel);
            }
            else if (n == 0)
            {
                if (nextPixel != null)
                {
                    DefineElementsSnakeTail(currentState, pixel, nextPixel);
                }
            }

            n++;
        }
    }

    private void DefineElementsSnakeBody(char[,] currentState, Pixel pixel, Pixel nextPixel, Pixel prevPixel)
    {
        if ((prevPixel.X < pixel.X && nextPixel.Y > pixel.Y) || (nextPixel.X < pixel.X && prevPixel.Y > pixel.Y))
        {
            currentState[pixel.X, pixel.Y] = SnakeSymbols._turnDownOrLeft;
        }
        else if ((prevPixel.X > pixel.X && nextPixel.Y > pixel.Y) || (prevPixel.Y > pixel.Y && nextPixel.X > pixel.X))
        {
            currentState[pixel.X, pixel.Y] = SnakeSymbols._turnDownOrRight;
        }
        else if ((prevPixel.X < pixel.X && nextPixel.Y < pixel.Y) || (prevPixel.Y < pixel.Y && nextPixel.X < pixel.X))
        {
            currentState[pixel.X, pixel.Y] = SnakeSymbols._turnUpOrLeft;
        }
        else if ((prevPixel.X > pixel.X && nextPixel.Y < pixel.Y) || (nextPixel.X > pixel.X && prevPixel.Y < pixel.Y))
        {
            currentState[pixel.X, pixel.Y] = SnakeSymbols._turnUpOrRight;
        }
        else if (prevPixel.X == nextPixel.X)
        {
            currentState[pixel.X, pixel.Y] = SnakeSymbols._verticalBody;
        }
        else if (prevPixel.Y == nextPixel.Y)
        {
            currentState[pixel.X, pixel.Y] = SnakeSymbols._horizontalBody;
        }
    }

    private void DefineElementsSnakeTail(char[,] currentState, Pixel pixel, Pixel nextPixel)
    {
        if (nextPixel.Y > pixel.Y)
        {
            currentState[pixel.X, pixel.Y] = SnakeSymbols._tailLooksUp;
        }
        else if (nextPixel.Y < pixel.Y)
        {
            currentState[pixel.X, pixel.Y] = SnakeSymbols._tailLooksDown;
        }
        else if (nextPixel.X > pixel.X)
        {
            currentState[pixel.X, pixel.Y] = SnakeSymbols._tailLooksLeft;
        }
        else if (nextPixel.X < pixel.X)
        {
            currentState[pixel.X, pixel.Y] = SnakeSymbols._tailLooksRight;
        }
    }

    private void DefineElementFood(GameData gameData, char[,] currentState)
    {
        currentState[gameData.Food.X, gameData.Food.Y] = SnakeSymbols._food;
    }

    private void DrawElements(GameData gameData, char[,] currentState)
    {
        for (int i = 0; i < gameData.BoardWidth; i++)
        {
            for (int j = 0; j < gameData.BoardHeight; j++)
            {
                if (_previousBuffer == null || currentState[i, j] != _previousBuffer[i, j])
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(currentState[i, j]);
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