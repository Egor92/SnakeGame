using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    internal class Foods
    {
        private Random random = new Random();
        public List<Pixel> FoodItems
        {
            get;
            private set;
        }

        public Foods()
        {
            FoodItems = new List<Pixel>();
        }

        public void Spawn(int width, int height, List<Pixel> snake, int count)
        {
            FoodItems.Clear();
            for (int i = 0; i < count; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(1, width - 1);
                    y = random.Next(1, height - 1);
                } while (snake.Any(segment => segment.X == x && segment.Y == y) || FoodItems.Any(food => food.X == x && food.Y == y));

                var foodPixel = new Pixel(x, y);
                FoodItems.Add(foodPixel);
                foodPixel.Draw('*');
            }
        }

        public void Remove(Pixel food)
        {
            FoodItems.Remove(food);
            food.Clear();
        }
    }
}
