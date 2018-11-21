using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoStraight
{
    class Program
    {
        
        const ConsoleColor PLAYERCOLOR = ConsoleColor.DarkBlue;
        const ConsoleColor BACKGROUND_COLOR = ConsoleColor.Green;
        

        public static Coordinate Player { get; set; } //Will represent our player that's moving around :P/>

        static void Main(string[] args)
        {
            InitGame();
            Console.CursorVisible = false;

            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        MovePlayer(0, -1);
                        break;

                    case ConsoleKey.RightArrow:
                        MovePlayer(1, 0);
                        break;

                    case ConsoleKey.DownArrow:
                        MovePlayer(0, 1);
                        break;

                    case ConsoleKey.LeftArrow:
                        MovePlayer(-1, 0);
                        break;
                }
            }
        }

        /// <summary>
        /// Paint the new player
        /// </summary>
        static void MovePlayer(int x, int y)
        {
            Coordinate newPlayer = new Coordinate()
            {
                X = Player.X + x,
                Y = Player.Y + y
            };

            if (CanMove(newPlayer))
            {
                RemoveOldPlayer();

                Console.BackgroundColor = PLAYERCOLOR;
                Console.SetCursorPosition(newPlayer.X, newPlayer.Y);
                Console.OutputEncoding = Encoding.Default;
                Console.Write('웃');
                Console.OutputEncoding = Encoding.Default;

                Player = newPlayer;
            }
        }

        /// <summary>
        /// Overpaint the old player
        /// </summary>
        static void RemoveOldPlayer()
        {
            Console.BackgroundColor = BACKGROUND_COLOR;
            Console.SetCursorPosition(Player.X, Player.Y);
            Console.Write(" ");
        }

        /// <summary>
        /// Make sure that the new coordinate is not placed outside the
        /// console window (since that will cause a runtime crash
        /// </summary>
        static bool CanMove(Coordinate c)
        {
            if (c.X < 0 || c.X >= Console.WindowWidth)
                return false;

            if (c.Y < 0 || c.Y >= Console.WindowHeight)
                return false;

            //TODO check map for the walls or blocks that can not be crossed.
            /*
             * if(Board.SpaceIsBlocked(c))
             * return false;
            */

            return true;
        }

        /// <summary>
        /// Paint a background color
        /// </summary>
        /// <remarks>
        /// It is very important that you run the Clear() method after
        /// changing the background color since this causes a repaint of the background
        /// </remarks>
        static void SetBackgroundColor()
        {
            Console.BackgroundColor = BACKGROUND_COLOR;
            Console.Clear(); //Important!
        }

        /// <summary>
        /// Initiates the game by painting the background
        /// and initiating the player
        /// </summary>
        static void InitGame()
        {
            SetBackgroundColor();

            Player = new Coordinate()
            {
                X = 0,
                Y = 0
            };

            MovePlayer(0, 0);

        }
    }

    /// <summary>
    /// Represents a map coordinate
    /// </summary>
    class Coordinate
    {
        public int X { get; set; } //Left
        public int Y { get; set; } //Top
    }
}
