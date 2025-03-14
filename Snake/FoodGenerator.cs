namespace Snake;

public static class FoodGenerator
{
    public static Pixel GenerateFood(GameData gameData)
    {
        Random random = new Random();

        while (true)
        {
            FreeFields freeFields = new FreeFields(gameData);
            Pixel[] fields = freeFields.GetFreeField();
            Pixel newFood = fields[random.Next(fields.Length)];
            return newFood;
        }
    }
}