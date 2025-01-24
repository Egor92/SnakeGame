using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Game
    {
        private GameData _gameData;
        private GameRenderer _gameRenderer;
        private bool gameOver;
        private Direction _currentDirection;

        public Game(GameData gameData, GameRenderer gameRenderer)
        {
            _gameData = gameData;
            _gameRenderer = gameRenderer;
            gameOver = false;
            _currentDirection = Direction.Right;
        }

        public void Start()
        {
            while (!gameOver)
            {
                _gameRenderer.RenderGame(_gameData);
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    ChangeDirection(key);
                }
                Move();
                CheckCollisions();
                System.Threading.Thread.Sleep(100); 
            }

            Console.Clear();
            Console.SetCursorPosition(10, 10);
            Console.WriteLine("Game Over!");
        }


        private void ChangeDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (_currentDirection != Direction.Down)
                        SetDirection(Direction.Up);
                    break;
                case ConsoleKey.DownArrow:
                    if (_currentDirection != Direction.Up)
                        SetDirection(Direction.Down);
                    break;
                case ConsoleKey.LeftArrow:
                    if (_currentDirection != Direction.Right)
                        SetDirection(Direction.Left);
                    break;
                case ConsoleKey.RightArrow:
                    if (_currentDirection != Direction.Left)
                        SetDirection(Direction.Right);
                    break;
            }
        }

        public void SetDirection(Direction newDirection)
        {
            _currentDirection = newDirection;
        }

        public Direction GetCurrentDirection()
        {
            return _currentDirection;
        }

        private void Move()
        {
            var head = _gameData._snake.GetBody().Peek(); 
            int newX = head.X;
            int newY = head.Y;
            switch (_currentDirection)
            {
                case global::Snake.Direction.Up:
                    newY--;
                    break;
                case global::Snake.Direction.Down:
                    newY++;
                    break;
                case global::Snake.Direction.Left:
                    newX--;
                    break;
                case global::Snake.Direction.Right:
                    newX++;
                    break;
            }

            var newHead = new Pixel(newX, newY);
            _gameData._snake.GetBody().Enqueue(newHead);
            _gameData._snake.GetBody().Dequeue();
        }

        private void CheckCollisions()
        {
            if (CheckCollisionWithWalls(45, 15) || CheckCollisionWithItself())
            {
                gameOver = true;
            }
        }

        private bool CheckCollisionWithItself()
        {
            var snakeBody = _gameData._snake.GetBody();
            var head = snakeBody.Peek();

            foreach (var pixel in snakeBody)
            {
                if (head != pixel && head.X == pixel.X && head.Y == pixel.Y)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckCollisionWithWalls(int width, int height)
        {
            var head = _gameData._snake.GetHead();
            return head.X <= 0 || head.X >= width || head.Y <= 0 || head.Y >= height;
        }
    }
}
