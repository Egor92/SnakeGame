namespace Snake;

public class GameRenderer
{
    public void RenderGame(GameData gameData)
    {
        Console.Clear();

        for (int i = 0; i < gameData.Walls.Count; i++)
        {
            Pixel wall = gameData.Walls[i];
            Draw('#', wall.X, wall.Y);
        }

        int n = 0;
        while (n < gameData.Snake.Body.Count)
        {
            var pixel = gameData.Snake.Body.ElementAt(n);
            Draw('*', pixel.X, pixel.Y);
            n++;
        }

        Draw('O', gameData.Food.X, gameData.Food.Y);

        if (gameData.IsGameOver)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(gameData.BoardWidth / 2 - 4, gameData.BoardHeight + 1);
            Console.WriteLine("Game Over!");
        }
    }

    public void Draw(char symbol, int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(symbol);
    }
}