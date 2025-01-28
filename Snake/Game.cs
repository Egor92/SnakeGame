using System.Threading;

namespace Snake
{
    public class Game
    {
        private GameData _gameData;
        private GameRenderer _gameRenderer;
        private bool _gameOver;
        private Direction _currentDirection;

        public Game(GameData gameData)
        {
            _gameData = gameData;
            _gameRenderer = new GameRenderer();
            _gameOver = false;
            _currentDirection = gameData.Snake.Direction; 
        }

        public void Start()
        {
            while (!_gameOver)
            {
                _gameRenderer.RenderGame(_gameData);
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    ChangeDirection(key);
                }

                Move();

                if (CheckCollisions())
                {
                    _gameOver = true;
                    return;
                }
                if (CheckFoodCollision())
                {
                    GrowSnake();
                    GenerateFood();
                }
                Thread.Sleep(00);
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
                        _currentDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    if (_currentDirection != Direction.Up)
                        _currentDirection = Direction.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    if (_currentDirection != Direction.Right)
                        _currentDirection = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    if (_currentDirection != Direction.Left)
                        _currentDirection = Direction.Right;
                    break;
            }
        }
        

        private void Move()
        {
            var snakeBody = _gameData.Snake.Body;
            var head = snakeBody.Last();

            var tail = snakeBody.First();

            //_gameRenderer.Draw(' ', tail.X, tail.Y);  

            int newX = head.X;
            int newY = head.Y;

            switch (_currentDirection)
            {
                case Direction.Up: newY--; break;
                case Direction.Down: newY++; break;
                case Direction.Left: newX--; break;
                case Direction.Right: newX++; break;
            }

            var newHead = new Pixel(newX, newY);
            snakeBody.Enqueue(newHead);  

            snakeBody.Dequeue();  
        }

        private bool CheckCollisions()
        {
            var snakeBody = _gameData.Snake.Body.ToArray();
            var head = snakeBody.Last();

            if (head.X <= 0 || head.X >= _gameData.BoardWidth - 1 || head.Y <= 0 || head.Y >= _gameData.BoardHeight - 1)
            {
                return true; 
            }

            for (int i = 0; i < snakeBody.Length; i++)
            {
                var pixel = snakeBody[i];
                if (pixel != head && pixel.X == head.X && pixel.Y == head.Y)
                {
                    return true; 
                }
            }

            return false;
        }

        private bool CheckFoodCollision()
        {
            var head = _gameData.Snake.Body.Last();
            return head.X == _gameData.Food.X && head.Y == _gameData.Food.Y;
        }

        private void GrowSnake()
        {
            var tail = _gameData.Snake.Body.First();
            _gameData.Snake.Body.Enqueue(new Pixel(tail.X, tail.Y));
        }

        private void GenerateFood()
        {
            Random random = new Random();

            while (true)
            {
                int x = random.Next(1, _gameData.BoardWidth - 1);
                int y = random.Next(1, _gameData.BoardHeight - 1);

                Pixel newFood = new Pixel(x, y);

                if (!_gameData.Walls.Contains(newFood) &&
                    !_gameData.Snake.Body.Contains(newFood))
                {
                    _gameData.Food = newFood;
                    break;
                }
            }
        }
    }
}
