namespace Snake;

public class GameRenderer
{
    private char[,] _pastState;
    private readonly char[,] _currentState;
    private readonly int _width;
    private readonly int _height;

    public GameRenderer(int width, int height)
    {
        _width = width;
        _height = height;
        _pastState = new char[_width, _height];
        _currentState = new char[_width, _height];
        ClearBuffer(_pastState);
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
        ClearBuffer(_currentState);

        foreach (var wall in gameData.Walls)
        {
            // Draw('#', wall.X, wall.Y);
            _currentState[wall.X, wall.Y] = '#';
        }

        int n = 0;
        while (n < gameData.Snake.Body.Count)
        {
            var pixel = gameData.Snake.Body.ElementAt(n);
            _currentState[pixel.X, pixel.Y] = '*';
            //Draw('*', pixel.X, pixel.Y);
            n++;
        }

        _currentState[gameData.Food.X, gameData.Food.Y] = '0';

        //Draw('O', gameData.Food.X, gameData.Food.Y);
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (_currentState[i, j] != _pastState[i, j])
                {
                    Console.SetCursorPosition(i, j);
                    if (_currentState[i, j] == ' ')
                    {
                        Console.WriteLine(" ");
                    }
                    else
                    {
                        Console.WriteLine(_currentState[i, j]);
                    }
                }
            }
        }

        DataSaving(_currentState);
        
        if (gameData.IsGameOver)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(gameData.BoardWidth / 2 - 4, gameData.BoardHeight + 1);
            Console.WriteLine("Game Over!");
        }
    }

    private void DataSaving(char[,] currentState)
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                _pastState[i, j] = currentState[i, j];
            }
        }
    }
    // public void Draw(char symbol, int x, int y)
    // {
    //     Console.SetCursorPosition(x, y);
    //     Console.Write(symbol);
    // }
}