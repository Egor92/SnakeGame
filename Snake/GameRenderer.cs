namespace Snake;

public class GameRenderer
{
    private char[,] _previousBuffer;
    private readonly GameData _gameData;
    private readonly int _width;
    private readonly int _height;

    public GameRenderer(GameData gameData)
    {
        _gameData = gameData;
        _width = _gameData._width;
        _height = _gameData.BoardHeight;
        _previousBuffer = new char[_width, _height];
    }

    public void RenderGame()
    {
        char[,] currentState = new char[_width, _height];
        ClearBuffer(currentState);

        foreach (var wall in _gameData.Walls)
        {
            currentState[wall.X, wall.Y] = '#';
        }

        int n = 0;
        while (n < _gameData.Snake.Body.Count)
        {
            var pixel = _gameData.Snake.Body.ElementAt(n);
            currentState[pixel.X, pixel.Y] = '*';
            n++;
        }

        currentState[_gameData.Food.X, _gameData.Food.Y] = '0';

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
        
        if (_gameData.IsGameOver)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(_gameData._width / 2 - 4, _gameData.BoardHeight + 1);
            Console.WriteLine("Game Over!");
        }
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
}