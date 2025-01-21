using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Console;
namespace Snake_Game
{
    public class Program
    {
        static void Main(string[] args)
        {
            CursorVisible = false;
            int width = 45;
            int height = 15;

            Walls walls = new Walls();
            walls.DrawWalls(width, height);
            Snake snake = new Snake(width / 2, height / 2);
            snake.Draw();

            Foods food = new Foods();
            int foodCount = 5;
            food.Spawn(width, height, snake.GetBody(), foodCount);

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    snake.ChangeDirection(key);
                }

                snake.Direction();

                if (snake.CheckWalls(width, height))
                {
                    string message = "Вы врезались в стенку. Игра закончена!";
                    int x = (width - message.Length) / 2;
                    int y = height / 2;
                    SetCursorPosition(x, y);
                    WriteLine(message);
                    break;
                }

                Pixel eatenFood = null;
                foreach (var foodItem in food.FoodItems)
                {
                    if (snake.CheckCollisionWithFood(foodItem))
                    {
                        eatenFood = foodItem;
                        break;
                    }
                }
                if (eatenFood != null)
                {
                    snake.Grow();
                    food.Remove(eatenFood);

                    if (food.FoodItems.Count < foodCount)
                    {
                        food.Spawn(width, height, snake.GetBody(), foodCount - food.FoodItems.Count);
                    }
                }

                Thread.Sleep(200);
            }
            Console.ReadLine();
        }

    }
}
