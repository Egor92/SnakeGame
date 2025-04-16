using Snake.Logic;

namespace Snake.ConsoleApp;

public class GameRenderer
{
    private char[,]? _previousBuffer;
    private Bufferer bufferer;

    public void RenderGame(GameData gameData)
    {
        _previousBuffer = new char[gameData.BoardWidth, gameData.BoardHeight];
        char[,] currentBuffer = new char[gameData.BoardWidth, gameData.BoardHeight];
        bufferer.ClearBuffer(currentBuffer, gameData.BoardWidth, gameData.BoardHeight);
        bufferer.WriteWallsToBuffer(gameData.Walls, currentBuffer);
        bufferer.WriteSnakeToBuffer(gameData.Snake, currentBuffer);
        if (gameData.Food != null)
        {
            bufferer.WriteFoodToBuffer(gameData.Food, currentBuffer);
        }

        DrawElements(gameData, currentBuffer);
        _previousBuffer = currentBuffer;
        if (gameData.IsGameOver)
        {
            DrawGameOver(gameData);
        }
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
}