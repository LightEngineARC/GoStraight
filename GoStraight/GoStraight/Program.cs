using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoStraight
{
    class Program
    {
        public static char playerChar = (char)976;
        
        public static Coordinate PlayerSpace { get; set; } //Will represent our player that's moving around
        
        private static int CountSteps = -1;
        private static string saveBoard = "savedboard";
        private static string loadboard;
        private static Board ACTIVE_BOARD;

        static Puzzle[] puzzlesInThisMaze;

        private static Random rand = new Random();

        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            try
            {
                using (StreamReader sr = new StreamReader(saveBoard + ".txt"))
                {
                    loadboard = sr.ReadLine().Trim();
                }
            }
            catch (Exception e)
            {
                Console.SetCursorPosition(44, 15);
                Console.WriteLine("The saveboard could not be read:");
                Console.WriteLine(e.Message);
            }
            ACTIVE_BOARD = new Board(loadboard);
            //            Console.SetBufferSize(1000, 1000);  // Just for Matt's computer. Everyone else can comment this out

            InitGame(ACTIVE_BOARD);
            Console.CursorVisible = false;
            // Console.WriteLine(ACTIVE_BOARD.GetCoordinate(0,0)); //shows if there is a wall at coordinate
            ConsoleKeyInfo keyInfo;
            ACTIVE_BOARD.PrintBoard();
            MovePlayer(Board.StartPositionX, Board.StartPositionY - 4, ACTIVE_BOARD);
            bool isMazeDone = false;

            /*
            // create puzzle to call
            Coordinate coordinate1 = new Coordinate
            {
                X = 40,
                Y = 10
            };
            int p = rand.Next(1, 10);
            int q = rand.Next(1, 10);
            Puzzle jaesPuzzle1 = new Puzzle(loadboard, coordinate1, true, $"What is {p} * {q}?", (p * q).ToString());
            //Puzzle jaesPuzzle1 = new Puzzle(loadboard, coordinate1, true, $"The answer is d", "d");

            Coordinate coordinate2 = new Coordinate
            {
                X = 46,
                Y = 13
            };
            Puzzle jaesPuzzle2 = new Puzzle(loadboard, coordinate2, false, "What is Sin(pi) * Cos(0)?", "0");
            */

            //Puzzle[] puzzles = new Puzzle[] { jaesPuzzle1, jaesPuzzle2 }; // in final, will be replaced with list of all puzzles in all mazes
            Puzzle[] puzzles = PuzzleQuestions.PuzzleArray();

            //array with just the Puzzles for the active maze.
            puzzlesInThisMaze = puzzles.Where((x) => x.Maze.Equals(loadboard)).ToArray();

            //print tiles for visible puzzles
            int xAdj = 0; //21;
            int yAdj = 0; // 5;

            //Puzzle[] visiblePuzzles = puzzlesInThisMaze.Where((x) => !x.IsTrap).ToArray();

            PrintPuzzle(puzzlesInThisMaze, xAdj, yAdj);

            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                // cycle through puzzles in this maze:
                Puzzle[] currentPuzzle = puzzlesInThisMaze.Where((x) => (!x.HasBeenSolved && x.PuzzleLocation.Equals(PlayerSpace))).ToArray();
                if (currentPuzzle.Length != 0 && !(currentPuzzle[0].HasBeenSolved))  // if the IEnumerable is not empty and thus the player occupies the space of a Puzzle
                {
                    if (currentPuzzle[0].RunPuzzle())  // if the player answers the puzzle successfully
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        ACTIVE_BOARD.PrintBoard();
                        PrintPuzzle(puzzlesInThisMaze, xAdj, yAdj);
                    }
                    else  //if the player gets the answer wrong
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        return;
                    }
                }


                /*
                if (PlayerSpace.Equals(jaesPuzzle1.PuzzleLocation)) // if the player's position equals the position of the trap/puzzle
                {
                    if(jaesPuzzle1.RunPuzzle())   // if the player fails the puzzle
                    {
                        //Console.Clear();
                        //Console.WriteLine("GAME OVER");
                        Main(new string[1]);
                    }
                    else
                    {
                        return;
                    }
                }
                */
                if (PlayerSpace.X == 41 && PlayerSpace.Y == 10)
                {
                    isMazeDone = true;
                    //Console.WriteLine("Congradulations, you won!");
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
            }
            //End game stuff here.
            CloseGame();
        }

        private static void CloseGame()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
 
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(34, 0);
            Console.WriteLine("THANKS FOR PLAYING!");
            Console.SetCursorPosition(31, 3);
        }

        private static void PrintPuzzle(Puzzle[] puzzlesInThisMaze, int xAdj, int yAdj)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            puzzlesInThisMaze.Where((x) => !x.IsTrap)  // prints tiles for Puzzles that are visible
                .ToList()
                .ForEach(x =>
                {
                    Console.SetCursorPosition(xAdj + x.PuzzleLocation.X, yAdj + x.PuzzleLocation.Y);
                    Console.ForegroundColor = ConsoleColor.Red; //TOD set dynamically based on active board
                    Console.Write(Puzzle.PuzzleDisplay);
                });
            Console.ForegroundColor = ConsoleColor.White;//TODO set dynamically based on active board

        }

        /// <summary>
        /// Paint the new player
        /// </summary>
        /// <author>Ashton</author>
        static void MovePlayer(int x, int y,Board active)
        {
            Coordinate newPlayer = new Coordinate()
            {
                X = PlayerSpace.X + x,
                Y = PlayerSpace.Y + y
            };

            if (CanMove(newPlayer, active))
            {
                if (active.CheckCoordinate(newPlayer))
                {
                    //TODO load new board
                    loadboard = active.getLinkedBoard(newPlayer);

                    ACTIVE_BOARD = new Board(loadboard);
                    puzzlesInThisMaze = PuzzleQuestions.PuzzleArray().Where((s) => s.Maze.Equals(loadboard)).ToArray();
                    Board blank = new Board("BlankMaze");
                    blank.PrintBoard();
                    ACTIVE_BOARD.PrintBoard();
                    newPlayer = new Coordinate
                    {
                        X = Board.StartPositionX,
                        Y = Board.StartPositionY
                    };
                    PlayerSpace = newPlayer;
                    MovePlayer(1, 0, ACTIVE_BOARD);

                }else
                {
                    //write over the old player's position
                    RemoveOldPlayer(active);

                    // print player in new position
                    //Console.BackgroundColor = PLAYERCOLOR;
                    Console.SetCursorPosition(newPlayer.X, newPlayer.Y);
                    Console.Write(playerChar);
                    // increase the counter for the number of steps the player has taken
                    CountSteps++;
                    // set the player's position to the new position
                    PlayerSpace = newPlayer;

                    // Update numbers in display
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
            if (c.X < 21 || c.X >= Console.WindowWidth-21)
                return false;

            if (c.Y < 5 || c.Y >= Console.WindowHeight)
                return false;

            //check map for the walls or blocks that can not be crossed.
            if (active.GetCoordinate(c.X-21,c.Y-5))//coordinates inside the allowed maze block
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
            } else
            {
                PlayerSpace.X++;
            }
            MovePlayer(0, 0,active);

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
    }

    /// <summary>
    /// Represents a map coordinate
    /// </summary>
    /// <author>Ashton</author>
    class Coordinate
    {
        public int X { get; set; } //Left
        public int Y { get; set; } //Top

        public override bool Equals(object obj)
        {
            return (X == ((Coordinate)obj).X && Y == ((Coordinate)obj).Y);
        }
    }
}