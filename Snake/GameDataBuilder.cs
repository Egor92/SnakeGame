namespace Snake;

public class GameDataBuilder
{
    private int _width;
    private int _height;
    private List<Pixel> _walls = new();
    private Queue<Pixel> _body;
    private Direction _direction;
    private Pixel _head;
    private Pixel _food;
    private Snake _snake;
    private GameDataBuilder() { }
    public static GameDataBuilder Create()
    {
        return new GameDataBuilder();
    }
    public GameDataBuilder SetPlayingFieldSize(int width, int height)
    {
        // проверка длины и ширины на не отрицательность
        if (width < 0 || height < 0)
        {
            throw new ArgumentException(
                "Ширина и высота должны быть положительными.");
        }

        _width = width;
        _height = height;
        return this;
    }

    public GameDataBuilder CreateWallAroundPlayingField(int width, int height)
    {
        // Логика для создания стены по краю игрового поля
        _walls = new List<Pixel>();

        for (int i = 0; i < width; i++)
        {
            _walls.Add(new Pixel(i, 0));
            _walls.Add(new Pixel(i, height - 1));
        }
        for (int i = 0; i < height; i++)
        {
            _walls.Add(new Pixel(0, i));
            _walls.Add(new Pixel(width - 1, i));
        }
        return this;
    }

    public GameDataBuilder AddSnake(int x, int y, Direction direction, int countBody)
    {
        // создание змейки
        _head = new Pixel(x, y);
        _body = new Queue<Pixel>();
        for (int i = countBody; i >= 1; i--)
        {
            _body.Enqueue(new Pixel(x - i, y));
        }
        _body.Enqueue(_head);

        _direction = direction;
        _snake = new Snake(_body, _direction, _head);
        return this;
    }

    public GameDataBuilder AddFood()
    {
        Random random = new Random();

        while (true)
        {
            int x = random.Next(1, _width - 1);
            int y = random.Next(1, _height - 1);

            Pixel food = new Pixel(x, y);

            if (!_walls.Contains(food) && (!_snake.Body.Contains(food)))
            {
                _food = food;
                break;
            }
        }
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
            IsGameOver = false
        };
    }
}
