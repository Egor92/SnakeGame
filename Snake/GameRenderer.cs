namespace Snake;

public class GameRenderer
{
    private char[,] _previousBuffer;
    private readonly int _width;
    private readonly int _height;

    public GameRenderer(int width, int height)
    {
        _width = width;
        _height = height;
        _previousBuffer = new char[_width, _height];
        ClearBuffer(_previousBuffer);
    }

    private void ClearBuffer(char[,] buffer)
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                buffer[i, j] = ' ';
            }
        }
    }

    public void RenderGame(GameData gameData)
    {
        char[,] currentState = new char[_width, _height];
        ClearBuffer(currentState);

        foreach (var wall in gameData.Walls)
        {
            currentState[wall.X, wall.Y] = '#';
        }

        int n = 0;
        while (n < gameData.Snake.Body.Count)
        {
            var pixel = gameData.Snake.Body.ElementAt(n);
            currentState[pixel.X, pixel.Y] = '*';
            n++;
        }

        currentState[gameData.Food.X, gameData.Food.Y] = '0';

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (currentState[i, j] != _previousBuffer[i, j])
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
}