namespace Snake;

public static class FoodGenerator
{
    public static Pixel GenerateFood(GameData gameData)
    {
        Random random = new Random();

        while (true)
        {
            int x = random.Next(1, gameData.BoardWidth - 1);
            int y = random.Next(1, gameData.BoardHeight - 1);

            Pixel newFood = new Pixel(x, y);

            if (!gameData.Walls.Contains(newFood) &&
                !gameData.Snake.Body.Contains(newFood))
            {
                return newFood;
            }
        }
    }
}