namespace Snake
{
    public class GameRenderer
    {
        public void RenderGame(GameData gameData)
        {
            Console.Clear();

            
            for (int i = 0; i < gameData._walls.Count; i++)
            {
                Pixel wall = gameData._walls[i];
                gameData.Draw('#', wall.X, wall.Y);
            }

            Pixel[] snakeBody = gameData._snake.GetBody().ToArray(); 

            for (int i = 0; i < snakeBody.Length; i++)
            {
                Pixel pixel = snakeBody[i];
                gameData.Draw('*', pixel.X, pixel.Y);
            }
        }
    }
}
