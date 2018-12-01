using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoStraight
{
    class Program
    {
        
        //const ConsoleColor PLAYERCOLOR = ConsoleColor.DarkBlue;
        //const ConsoleColor BACKGROUND_COLOR = ConsoleColor.Green;

        public static Coordinate PlayerSpace { get; set; } //Will represent our player that's moving around
        private static int CountSteps = -1;

        private static Random rand = new Random();

        static void Main(string[] args)
        {
            Console.SetBufferSize(1000, 1000);  // Just for Matt's computer. Everyone else can comment this out
            String mazeFileName = "Maze";

            Board ACTIVE_BOARD = new Board(mazeFileName);
            InitGame(ACTIVE_BOARD);
            Console.CursorVisible = false;
            // Console.WriteLine(ACTIVE_BOARD.GetCoordinate(0,0)); //shows if there is a wall at coordinate
            ConsoleKeyInfo keyInfo;
            ACTIVE_BOARD.PrintBoard();
            MovePlayer(Board.StartPositionX, Board.StartPositionY - 4, ACTIVE_BOARD);
            bool isMazeDone = false;

            // create puzzle to call
            Coordinate coordinate1 = new Coordinate
            {
                X = 40,
                Y = 10
            };
            int p = rand.Next(1, 10);
            int q = rand.Next(1, 10);
            Puzzle jaesPuzzle1 = new Puzzle(mazeFileName, coordinate1, true, $"What is {p} * {q}?", (p * q).ToString());

            Coordinate coordinate2 = new Coordinate
            {
                X = 46,
                Y = 13
            };
            Puzzle jaesPuzzle2 = new Puzzle(mazeFileName, coordinate2, false, "What is Sin(pi) * Cos(0)?", "0");

            Puzzle[] puzzles = new Puzzle[] { jaesPuzzle1, jaesPuzzle2 }; // in final, will be replaced with list of all puzzles in all mazes

            //array with just the Puzzles for the active maze.
            Puzzle[] puzzlesInThisMaze = puzzles.Where((x) => x.Maze.Equals(mazeFileName)).ToArray();

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
                        ACTIVE_BOARD.PrintBoard();
                        PrintPuzzle(puzzlesInThisMaze, xAdj, yAdj);
                    }
                    else  //if the player gets the answer wrong
                    {
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
                        Board.Save(PlayerSpace.X, PlayerSpace.Y, "Maze");
                        Console.BackgroundColor = ConsoleColor.Black;
                        return;
                    case ConsoleKey.Q:
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Clear();
                        return;
                }
            }
            //TODO add end game stuff here.

        }

        private static void PrintPuzzle(Puzzle[] puzzlesInThisMaze, int xAdj, int yAdj)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            puzzlesInThisMaze.Where((x) => !x.IsTrap)  // prints tiles for Puzzles that are visible
                .ToList()
                .ForEach(x =>
                {
                    Console.SetCursorPosition(xAdj + x.PuzzleLocation.X, yAdj + x.PuzzleLocation.Y);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(Puzzle.PuzzleDisplay);
                });
            Console.ForegroundColor = ConsoleColor.White;
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
                //write over the old player's position
                RemoveOldPlayer(active);
                
                // pront player in new position
                //Console.BackgroundColor = PLAYERCOLOR;
                Console.SetCursorPosition(newPlayer.X, newPlayer.Y);
                Console.OutputEncoding = Encoding.Default;
                Console.Write('X');
                Console.OutputEncoding = Encoding.Default;
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
                Console.SetCursorPosition(PlayerSpace.X,PlayerSpace.Y);
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
            if (c.X < 21 || c.X >= Console.WindowWidth-21)
                return false;

            if (c.Y < 5 || c.Y >= Console.WindowHeight)
                return false;

            //TODO check map for the walls or blocks that can not be crossed.
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
    //TODO define maze layout sizes. use the areas to display text
    //TODO print each area by controlling the background color and the foreground color then printing the maze
}