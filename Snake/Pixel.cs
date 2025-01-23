using System;

namespace Snake_Game
{
    public class Pixel
    {
        public Pixel(int width, int height)
        {
            X = width;
            Y = height;
        }
        public int X { get; }
        public int Y { get; }
        public void Draw(char symbol)
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(symbol);
        }
        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(' ');
        }
    }
}
