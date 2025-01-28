namespace Snake
{
    public class GameRenderer
    {
        public Game game;
        public void RenderGame(GameData gameData)
        {
            
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
           
         }
        public void Draw(char symbol, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
    }
}
