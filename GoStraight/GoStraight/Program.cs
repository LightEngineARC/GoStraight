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
        //const ConsoleColor BACKGROUND_COLOR = ConsoleColor.Green;

        public static Coordinate PlayerSpace { get; set; } //Will represent our player that's moving around

        static void Main(string[] args)
        {

            
            Board ACTIVE_BOARD = new Board("MyMaze");
            InitGame(ACTIVE_BOARD);
            Console.CursorVisible = false;
            // Console.WriteLine(ACTIVE_BOARD.GetCoordinate(0,0)); //shows if there is a wall at coordinate

            ConsoleKeyInfo keyInfo;
            ACTIVE_BOARD.PrintBoard();
            MovePlayer(0, 0, ACTIVE_BOARD);
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        MovePlayer(0, -1,ACTIVE_BOARD);
                        break;

                    case ConsoleKey.RightArrow:
                        MovePlayer(1, 0,ACTIVE_BOARD);
                        break;

                    case ConsoleKey.DownArrow:
                        MovePlayer(0, 1,ACTIVE_BOARD);
                        break;

                    case ConsoleKey.LeftArrow:
                        MovePlayer(-1, 0,ACTIVE_BOARD);
                        break;
                }
            }
            //TODO add end game stuff here.

        }

        /// <summary>
        /// Paint the new player
        /// </summary>
        static void MovePlayer(int x, int y,Board active)
        {
            Coordinate newPlayer = new Coordinate()
            {
                X = PlayerSpace.X + x,
                Y = PlayerSpace.Y + y
            };

            if (CanMove(newPlayer, active))
            {
                RemoveOldPlayer(active);

                //Console.BackgroundColor = PLAYERCOLOR;
                Console.SetCursorPosition(newPlayer.X, newPlayer.Y);
                Console.OutputEncoding = Encoding.Default;
                Console.Write('X');
                Console.OutputEncoding = Encoding.Default;

                PlayerSpace = newPlayer;
            }
        }

        /// <summary>
        /// Overpaint the old player
        /// </summary>
        static void RemoveOldPlayer(Board active)
        {
            Console.BackgroundColor = active.GetPathColor();
            Console.SetCursorPosition(PlayerSpace.X, PlayerSpace.Y);
            Console.Write(" ");
        }

        /// <summary>
        /// Make sure that the new coordinate is not placed outside the
        /// console window (since that will cause a runtime crash
        /// </summary>
        static bool CanMove(Coordinate c, Board active)
        {
            if (c.X < 21 || c.X >= Console.WindowWidth-20)
                return false;

            if (c.Y < 6 || c.Y >= Console.WindowHeight)
                return false;

            //TODO check map for the walls or blocks that can not be crossed.
            if (active.GetCoordinate(c.X-21,c.Y-6))//coordinates inside the allowed maze block
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Paint a background color
        /// </summary>
        /// <remarks>
        /// It is very important that you run the Clear() method after
        /// changing the background color since this causes a repaint of the background
        /// </remarks>
        static void SetBackgroundColor(Board active)
        {
            Console.BackgroundColor = active.GetPathColor();
            Console.Clear(); //Important!
        }

        /// <summary>
        /// Initiates the game by painting the background
        /// and initiating the player
        /// </summary>
        static void InitGame(Board active)
        {
            SetBackgroundColor(active);

            PlayerSpace = new Coordinate()
            {
                X = 22,
                Y = 7
            };

            MovePlayer(0, 0,active);

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
    //TODO add the game display 20 for first section, 25 for second section, 20 for inventory section
    //TODO 12 high for maze, about 5 high for message section
    //TODO print each area by controlling the background color and the foreground color then printing the maze
}
