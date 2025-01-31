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

        if (gameData.Food != null)
        {
            Draw('O', gameData.Food.X, gameData.Food.Y);
        }

    }// двумерн массив чаров. задавать чары в этом массиве
    public void Draw(char symbol, int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(symbol);
    }
    public void Game(bool game)
    {
        if (game == true)
        {
            Console.Clear();
            Console.SetCursorPosition(10, 10);
            Console.WriteLine("Game Over!");
        }
    }
}
