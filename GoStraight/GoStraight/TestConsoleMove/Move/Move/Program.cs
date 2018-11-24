using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Move
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo cki;
            int x = 0;
            int y = 0;

            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(x, y);
                Console.Write('@');
                cki = Console.ReadKey(true);
                switch(cki.Key)
                {
                    case ConsoleKey.LeftArrow:
                        x--;
                        break;
                    case ConsoleKey.RightArrow:
                        x++;
                        break;
                    case ConsoleKey.DownArrow:
                        y++;
                        break;
                    case ConsoleKey.UpArrow:
                        y--;
                        break;
                    case ConsoleKey.Q:
                        return;
                }
            }
        }
    }
}
