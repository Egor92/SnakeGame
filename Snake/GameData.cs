namespace Snake
{
    public class GameData
    {
        public Pixel _food;
        public List<Pixel> _walls;
        public Snake _snake;

        public GameData(int width, int height)
        {
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
            _snake = new Snake(new Pixel(width / 2, height / 2), 3);
        }

        public void Draw(char symbol, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
    }
}
