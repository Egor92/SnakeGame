using Snake;
using System;
using System.Collections.Generic;
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
            Console.CursorVisible = false;
            int width = 45;
            int height = 15;
            GameData gameData = new GameData(width, height);
            GameRenderer renderer = new GameRenderer();
            Game game = new Game(gameData, renderer);
            game.Start();
            Console.ReadLine();
        }

    }
}