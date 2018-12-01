using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoStraight
{
    class Program
    {
        public static Coordinate PlayerSpace { get; set; } //Will represent our player that's moving around

        private static int CountSteps = -1;
        private static string saveBoard = "savedboard";
        private static string loadboard;
        private static bool isEscaped = false;
        private static bool isGameOver = false;

        static void Main(string[] args)
        {
            while (true)
            {

                try
                {
                    using (StreamReader sr = new StreamReader(saveBoard + ".txt"))
                    {

                        loadboard = sr.ReadLine().Trim();
                        if (Board.EndPositionX == PlayerSpace.X && Board.EndPositionY == PlayerSpace.Y)
                        {
                            loadboard = sr.ReadLine().Trim();
                        } 
                    }
                }
                catch (Exception e)
                {
                    Console.SetCursorPosition(44, 15);
                    Console.WriteLine("The saveboard could not be read:");
                    Console.WriteLine(e.Message);
                }
                Board ACTIVE_BOARD = new Board(loadboard);
                InitGame(ACTIVE_BOARD);
                Console.CursorVisible = false;
                // Console.WriteLine(ACTIVE_BOARD.GetCoordinate(0,0)); //shows if there is a wall at coordinate
                ConsoleKeyInfo keyInfo;
                ACTIVE_BOARD.PrintBoard();
                MovePlayer(Board.StartPositionX, Board.StartPositionY - 4, ACTIVE_BOARD);
                bool isPuzzleDone = false;
                while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
                {
                    if (PlayerSpace.X == 40 && PlayerSpace.Y == 10)
                    {
                        Puzzle.MultiplePuzzle();
                        if (Puzzle.isFail == true)
                        {
                            Console.Clear();
                            Console.WriteLine("GAME OVER");
                            return;
                        }
                        else
                        {
                            Main(new string[1]);
                        }
                    }
                    if (PlayerSpace.X == 41 && PlayerSpace.Y == 10)
                    {
                        isPuzzleDone = true;
                    }
                    //Puzzle.MultiplePuzzle(PlayerSpace.X-21, PlayerSpace.Y-5);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            MovePlayer(0, -1, ACTIVE_BOARD);
                            break;

                        case ConsoleKey.RightArrow:
                            MovePlayer(1, 0, ACTIVE_BOARD);
                            break;

                        case ConsoleKey.DownArrow:
                            MovePlayer(0, 1, ACTIVE_BOARD);
                            break;

                        case ConsoleKey.LeftArrow:
                            MovePlayer(-1, 0, ACTIVE_BOARD);
                            break;
                        case ConsoleKey.S:

                            UpdateLoadBoard();
                            Board.Save(PlayerSpace.X, PlayerSpace.Y, loadboard);
                            Console.BackgroundColor = ConsoleColor.Black;
                            return;
                        case ConsoleKey.Q:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                            return;
                    }
                    if(PlayerSpace.X == 54 && PlayerSpace.Y == 24)
                    {
                        Board newActiveBoard = new Board("Maze");
                        Console.Clear();
                        newActiveBoard.PrintBoard();
                    }
                }
                //TODO add end game stuff here.
            }
        }

        /// <summary>
        /// Paint the new player
        /// </summary>
        /// <author>Ashton</author>
        static void MovePlayer(int x, int y, Board active)
        {
            Coordinate newPlayer = new Coordinate()
            {
                X = PlayerSpace.X + x,
                Y = PlayerSpace.Y + y
            };

            if (CanMove(newPlayer, active))
            {
                CountSteps++;
                RemoveOldPlayer(active);


                //Console.BackgroundColor = PLAYERCOLOR;
                Console.SetCursorPosition(newPlayer.X, newPlayer.Y);
                Console.OutputEncoding = Encoding.Default;
                Console.Write('X');
                Console.OutputEncoding = Encoding.Default;
                CountSteps++;
                PlayerSpace = newPlayer;
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(14, 12);
                //Console.Write(Console.CursorLeft + "," + (Console.CursorTop) + " ");
                Console.Write((PlayerSpace.X) + "," + (PlayerSpace.Y) + " ");
                Console.SetCursorPosition(90, 9);
                Console.Write(Puzzle.puzzleCount);
                Console.SetCursorPosition(PlayerSpace.X, PlayerSpace.Y);
                Console.SetCursorPosition(80, 7);
                Console.Write(CountSteps);
            }
        }

        /// <summary>
        /// Overpaint the old player
        /// </summary>
        /// <author>Ashton</author>
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
        /// <author>Ashton</author>
        static bool CanMove(Coordinate c, Board active)
        {
            if (c.X < 21 || c.X >= Console.WindowWidth - 21)
                return false;

            if (c.Y < 5 || c.Y >= Console.WindowHeight)
                return false;

            //check map for the walls or blocks that can not be crossed.
            if (active.GetCoordinate(c.X - 21, c.Y - 5))//coordinates inside the allowed maze block
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Paint a background color
        /// </summary>
        /// <author>Ashton</author>
        static void SetBackgroundColor(Board active)
        {
            Console.BackgroundColor = active.GetPathColor();
            Console.Clear();
        }

        /// <summary>
        /// Initiates the game by painting the background
        /// and initiating the player
        /// </summary>
        /// <author>Ashton</author>
        static void InitGame(Board active)
        {
            SetBackgroundColor(active);

            if (PlayerSpace == null)
            {
                PlayerSpace = new Coordinate()
                {
                    X = 0,
                    Y = 4
                };
            }
            else
            {
                PlayerSpace.X++;
            }
            MovePlayer(0, 0, active);

        }

        /// <summary>
        /// writes the filename of the current board to the savedboard file.
        /// <author>Ashton</author>
        /// </summary>
        static void UpdateLoadBoard()
        {
            using (StreamWriter writer = new StreamWriter(saveBoard + ".txt"))
            {
                writer.Write(loadboard);
            }
        }

        static void Escape(string s, int x, int y)
        {
            if (s.Equals("Maze"))
            {
                if (x == 69 && y == 28)
                {
                    isEscaped = true;
                }
                else isEscaped = false;
            }
            else if (s.Equals("StartBoard"))
            {
                if (x == 54 && y == 24)
                {
                    isEscaped = true;
                }
                else isEscaped = false;
            }
        }
    }

    /// <summary>
    /// Represents a map coordinate
    /// </summary>
    /// <author>Ashton</author>
    class Coordinate
    {
        public int X { get; set; } //Left
        public int Y { get; set; } //Top
    }
    //TODO use the areas to display text for the players
}