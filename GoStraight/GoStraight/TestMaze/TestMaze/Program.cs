using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Maze m = new Maze();
            m.PrintMaze();
            //m.MovePlayer();

            Console.WriteLine("\n\n");
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
