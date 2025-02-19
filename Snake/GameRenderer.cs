namespace Snake;

public class GameRenderer
{
    private char[,] _previousBuffer;


    public void RenderGame(GameData gameData)
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        _previousBuffer = new char[gameData.BoardWidth, gameData.BoardHeight];
        char[,] currentState = new char[gameData.BoardWidth, gameData.BoardHeight];
        ClearBuffer(currentState, gameData.BoardWidth, gameData.BoardHeight);

        foreach (var wall in gameData.Walls)
        {
            currentState[wall.X, wall.Y] = '#';
        }

        int n = 0;
        while (n < gameData.Snake.Body.Count)
        {
            var pixel = gameData.Snake.Body.ElementAt(n);
            Pixel nextPixel;
            Pixel prevPixel;
            if (n < gameData.Snake.Body.Count - 1)
            {
                nextPixel = gameData.Snake.Body.ElementAt(n + 1);
            }
            else
            {
                nextPixel = null;
            }

            if (n > 0)
            {
                prevPixel = gameData.Snake.Body.ElementAt(n - 1);
            }
            else
            {
                prevPixel = null;
            }

            if (n == gameData.Snake.Body.Count - 1)
            {
                currentState[pixel.X, pixel.Y] = gameData.Snake.Direction switch
                {
                    Direction.Up => (char)708,
                    Direction.Down => (char)709,
                    Direction.Left => (char)706,
                    Direction.Right => (char)707,
                    _ => currentState[pixel.X, pixel.Y]
                };
            }
            else if (nextPixel != null && prevPixel != null)
            {
                if ((prevPixel.X < pixel.X && nextPixel.Y > pixel.Y))
                {
                    currentState[pixel.X, pixel.Y] = '┐';
                }
                else if ((prevPixel.X > pixel.X && nextPixel.Y > pixel.Y))
                {
                    currentState[pixel.X, pixel.Y] = '┌';
                }
                else if ((prevPixel.X < pixel.X && nextPixel.Y < pixel.Y))
                {
                    currentState[pixel.X, pixel.Y] = '┘';
                }
                else if ((prevPixel.X > pixel.X && nextPixel.Y < pixel.Y))
                {
                    currentState[pixel.X, pixel.Y] = '└';
                }
                else if (prevPixel.X == nextPixel.X)
                {
                    currentState[pixel.X, pixel.Y] = '│';
                }
                else if (prevPixel.Y == nextPixel.Y)
                {
                    currentState[pixel.X, pixel.Y] = '─';
                }
            }
            // else if (nextPixel != null && prevPixel == null &&
            //          (gameData.Snake.Direction == Direction.Right || gameData.Snake.Direction == Direction.Left))
            // {
            //     currentState[pixel.X, pixel.Y] = '│';
            // }
            // else if (nextPixel != null && prevPixel == null && (gameData.Snake.Direction == Direction.Up ||
            //                                                     gameData.Snake.Direction == Direction.Down))
            // {
            //     currentState[pixel.X, pixel.Y] = '─';
            // }
            n++;
        }


        currentState[gameData.Food.X, gameData.Food.Y] = 'ó';
        for (int i = 0;
             i < gameData.BoardWidth;
             i++)
        {
            for (int j = 0; j < gameData.BoardHeight; j++)
            {
                if (_previousBuffer == null || currentState[i, j] != _previousBuffer[i, j])
                {
                    Console.SetCursorPosition(i, j);
                    Console.WriteLine(currentState[i, j]);
                }
            }
        }

        _previousBuffer = currentState;
        if (gameData.IsGameOver)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(gameData.BoardWidth / 2 - 4, gameData.BoardHeight + 1);
            Console.WriteLine("Game Over!");
        }
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