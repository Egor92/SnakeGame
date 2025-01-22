using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    internal class Snake
    {
        private List<Pixel> snake;
        private Direction CurrentDirection;

        public Snake(int startX, int startY)
        {
            snake = new List<Pixel>
            {
                new Pixel(startX, startY),
                new Pixel(startX - 1, startY),
                new Pixel(startX - 2, startY)
            };

            CurrentDirection = Snake_Game.Direction.Up;
        }

        public void Draw()
        {
            foreach (var pixel in snake)
            {
                pixel.Draw('*');
            }
        }

        public void Direction()
        {
            var head = snake[0];
            int newX = head.X;
            int newY = head.Y;

            switch (CurrentDirection)
            {
                case Snake_Game.Direction.Up:
                    newY--;
                    break;
                case Snake_Game.Direction.Down:
                    newY++;
                    break;
                case Snake_Game.Direction.Left:
                    newX--;
                    break;
                case Snake_Game.Direction.Right:
                    newX++;
                    break;
            }

            var newHead = new Pixel(newX, newY);
            snake.Insert(0, newHead);
            snake[snake.Count - 1].Clear();
            snake.RemoveAt(snake.Count - 1);

            Draw();
        }

        public void ChangeDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow when CurrentDirection != Snake_Game.Direction.Down:
                    CurrentDirection = Snake_Game.Direction.Up;
                    break;
                case ConsoleKey.DownArrow when CurrentDirection != Snake_Game.Direction.Up:
                    CurrentDirection = Snake_Game.Direction.Down;
                    break;
                case ConsoleKey.LeftArrow when CurrentDirection != Snake_Game.Direction.Right:
                    CurrentDirection = Snake_Game.Direction.Left;
                    break;
                case ConsoleKey.RightArrow when CurrentDirection != Snake_Game.Direction.Left:
                    CurrentDirection = Snake_Game.Direction.Right;
                    break;
            }
        }

        public bool CheckWalls(int width, int height)
        {
            var head = snake[0];

            if (head.X <= 0 || head.X >= width - 1 || head.Y <= 0 || head.Y >= height - 1)
            {
                return true;

            }

            for (int i = 1; i < snake.Count; i++)
            {
                if (head.X == snake[i].X && head.Y == snake[i].Y)
                    return true;
            }

            return false;
        }
        public void Grow()
        {
            var tail = snake[snake.Count - 1];
            snake.Add(new Pixel(tail.X, tail.Y));
        }
        public bool CheckCollisionWithFood(Pixel foodPosition)
        {
            var head = snake[0];
            return head.X == foodPosition.X && head.Y == foodPosition.Y;
        }

        public List<Pixel> GetBody()
        {
            return snake;
        }


    }
}
