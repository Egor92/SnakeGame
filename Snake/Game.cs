namespace Snake;

public class Game
{
    private GameData _gameData;
    private GameRenderer _gameRenderer;
    private readonly GameLogic _gameLogic;

    public Game(GameData gameData, GameLogic gameLogic)
    {
        _gameData = gameData;
        _gameRenderer = new GameRenderer();
        _gameLogic = gameLogic;
    }

    public void Start()
    {
        while (!_gameData.IsGameOver)
        {
            _gameRenderer.RenderGame(_gameData);

            while (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {
                    _gameLogic.ChangeDirection(Direction.Up);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    _gameLogic.ChangeDirection(Direction.Down);
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    _gameLogic.ChangeDirection(Direction.Left);
                }
                else
                {
                    _gameLogic.ChangeDirection(Direction.Right);
                }
            }


            _gameLogic.DoStep();
            //Move();

            if (_gameLogic.CheckCollisions())
            {
                _gameData.IsGameOver = true;
                break;
            }

            // if (_gameLogic.CheckFoodCollision())
            // {
            //     _gameLogic.GrowSnake();
            //     GenerateFood();
            // }

            Thread.Sleep(200);
        }

        _gameRenderer.RenderGame(_gameData);
    }

    // private void ChangeDirection(ConsoleKey key)
    // {
    //     switch (key)
    //     {
    //         case ConsoleKey.UpArrow:
    //             if (_gameData.Snake.Direction != Direction.Down)
    //                 _gameData.Snake.Direction = Direction.Up;
    //             break;
    //         case ConsoleKey.DownArrow:
    //             if (_gameData.Snake.Direction != Direction.Up)
    //                 _gameData.Snake.Direction = Direction.Down;
    //             break;
    //         case ConsoleKey.LeftArrow:
    //             if (_gameData.Snake.Direction != Direction.Right)
    //                 _gameData.Snake.Direction = Direction.Left;
    //             break;
    //         case ConsoleKey.RightArrow:
    //             if (_gameData.Snake.Direction != Direction.Left)
    //                 _gameData.Snake.Direction = Direction.Right;
    //             break;
    //     }
    // }

    /*
    private void Move()
    {
        var snakeBody = _gameData.Snake.Body;
        var head = _gameData.Snake.Head;

        int newX = head.X;
        int newY = head.Y;

        switch (_gameData.Snake.Direction)
        {
            case Direction.Up:
                newY--;
                break;
            case Direction.Down:
                newY++;
                break;
            case Direction.Left:
                newX--;
                break;
            case Direction.Right:
                newX++;
                break;
        }
        */

    //     var newHead = new Pixel(newX, newY);
    //     snakeBody.Enqueue(newHead);
    //     snakeBody.Dequeue();
    //     _gameData.Snake.Head = newHead;
    // }

    // private bool CheckCollisions()
    // {
    //     var snakeBody = _gameData.Snake.Body.ToArray();
    //     var head = snakeBody.Last();
    //
    //     foreach (var snakePixel in snakeBody[0..^1])
    //     {
    //         if (snakePixel.X == head.X && snakePixel.Y == head.Y)
    //         {
    //             return true;
    //         }
    //     }
    //
    //     foreach (var wallPixel in _gameData.Walls)
    //     {
    //         if (wallPixel.X == head.X && wallPixel.Y == head.Y)
    //         {
    //             return true;
    //         }
    //     }
    //
    //     return false;
    // }
    // public bool CheckCollisions()
    // {
    //     var snakeBody = _gameData.Snake.Body.ToArray();
    //     var head = snakeBody.Last();
    //
    //     foreach (var snakePixel in snakeBody[0..^1])
    //     {
    //         if (snakePixel.X == head.X && snakePixel.Y == head.Y)
    //         {
    //             return true;
    //         }
    //     }
    //
    //     foreach (var wallPixel in _gameData.Walls)
    //     {
    //         if (wallPixel.X == head.X && wallPixel.Y == head.Y)
    //         {
    //             return true;
    //         }
    //     }
    //
    //     return false;
    // }
    // private bool CheckFoodCollision()
    // {
    //     var head = _gameData.Snake.Head;
    //     return head.X == _gameData.Food.X && head.Y == _gameData.Food.Y;
    // }
    //
    // private void GrowSnake()
    // {
    //     var newElement = _gameData.Snake.Body.Last();
    //     _gameData.Snake.Body.Enqueue(new Pixel(newElement.X, newElement.Y));
    // }

    
}