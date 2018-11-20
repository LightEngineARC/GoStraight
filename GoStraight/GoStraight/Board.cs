using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoStraight
{
    class Board
    {
        public static void Outline()
        {
            Console.SetCursorPosition(20, 0);
            Console.WriteLine("BackPacking Adventure");
            GameFunction();
            PlayerInfo();
        }
        private static void GameFunction()
        {
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("Save");
            Console.WriteLine("Quit");
            Console.WriteLine("Menu");
            Console.WriteLine("Controlls"); // maybe we can put some background sounds and make it stop with this
            Console.SetCursorPosition(0, 15);

        }
        private static void PlayerInfo()
        {
            Console.SetCursorPosition(60, 5);
            Console.WriteLine("Player's info");
            Console.WriteLine("Statics");
            Console.WriteLine("Inventory");
            Console.WriteLine("Item slots");
            Console.WriteLine("Puzzle Keys");
        }
    }
  
}
