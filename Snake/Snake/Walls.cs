using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    internal class Walls
    {
        public void DrawWalls(int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                new Pixel(i, 0).Draw('#');
                new Pixel(i, height - 1).Draw('#');
            }
            for (int i = 0; i < height; i++)
            {
                new Pixel(0, i).Draw('#');
                new Pixel(width - 1, i).Draw('#');
            }
        }
    }
}
