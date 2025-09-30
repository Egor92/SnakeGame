
namespace client_serverApp.Snake.Logic;

public class GameDataBuilder
{
    private int _width;
    private int _height;
    private List<Cell> _walls = new();
    private Queue<Cell> _body = new();
    private Direction _direction;
    private Cell? _food;
    private Snake _snake;
    private int _pointCount;
    private int _stepCount;

    private GameDataBuilder()
    {
        var body = new Queue<Cell>();
        body.Enqueue(new Cell(0, 0));
        _snake = new Snake(body, Direction.Right, new Cell(0, 0));
    }

    public static GameDataBuilder Create()
    {
        return new GameDataBuilder();
    }

    public GameDataBuilder SetPlayingFieldSize(int width, int height)
    {
        // проверка длины и ширины на не отрицательность
        if (width < 0 || height < 0)
        {
            throw new ArgumentException("Ширина и высота должны быть положительными.");
        }

        _width = width;
        _height = height;
        return this;
    }

    public GameDataBuilder CreateWallAroundPlayingField(int width, int height)
    {
        // Логика для создания стены по краю игрового поля
        _walls = new List<Cell>();

        for (int i = 0; i < width; i++)
        {
            _walls.Add(new Cell(i, 0));
            _walls.Add(new Cell(i, height - 1));
        }

        for (int i = 0; i < height; i++)
        {
            _walls.Add(new Cell(0, i));
            _walls.Add(new Cell(width - 1, i));
        }

        return this;
    }

    public GameDataBuilder AddSnake(int x, int y, Direction direction, int snakeLength)
    {
        // создание змейки
        var head = new Cell(x, y);
        _body = new Queue<Cell>();

        for (int i = snakeLength - 1; i >= 1; i--)
        {
            Cell bodyCell = direction switch
            {
                Direction.Right => new Cell(x - i, y),
                Direction.Left => new Cell(x + i, y),
                Direction.Up => new Cell(x, y + i),
                Direction.Down => new Cell(x, y - i),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, $"Unexpected direction: {direction}")
            };

            _body.Enqueue(bodyCell);
        }

        _body.Enqueue(head);

        _direction = direction;
        _snake = new Snake(_body, _direction, head);
        return this;
    }

    public GameDataBuilder AddFood()
    {
        while (true)
        {
            int x = RandomAdapter.Next(1, _width - 1);
            int y = RandomAdapter.Next(1, _height - 1);

            Cell food = new Cell(x, y);

            if (!_walls.Contains(food) && (!_snake.Body.Contains(food)))
            {
                _food = food;
                break;
            }
        }

        return this;
    }

    public GameDataBuilder SetStepCount(int stepCount)
    {
        _stepCount = stepCount;
        return this;
    }

    public GameDataBuilder SetPointCount(int pointCount)
    {
        _pointCount = pointCount;
        return this;
    }

    public GameData Build()
    {
        return new GameData()
        {
            BoardWidth = _width,
            BoardHeight = _height,
            Walls = _walls,
            Snake = _snake,
            Food = _food,
            IsGameOver = false,
            PointCount = _pointCount,
            StepCount = _stepCount
        };
    }
}