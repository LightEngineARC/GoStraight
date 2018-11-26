using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GoStraight
{
    public class Board
    {
        public static void Outline()//Jae and Matthew
        {
            //TODO set background for this position top 0 bottom 4 left 0 right (95?)
            Console.SetCursorPosition(21, 0);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.WriteLine("                                                                                            ");
            }
            Console.SetCursorPosition(35, 1);
            Console.WriteLine("Backpack Adventure");
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("A game by Ashton, Jae, Khayman, and Matthew");
            GameFunction();
            PlayerInfo();
        }

        private static void GameFunction()//Jae and Mathew
        {
            //TODO create unchanging background for this section top 5 bottom 55 left 20
            Console.SetCursorPosition(0, 5);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            for(int i= 0; i < 25; i++)
            {
                Console.WriteLine("                    ");
            }
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("     Save");
            Console.WriteLine("     Quit");
            Console.WriteLine("     Menu");
            Console.WriteLine("     Controls"); // maybe we can put some background sounds and make it stop with this
            Console.SetCursorPosition(0, 15);
        }

        private static void PlayerInfo()//Jay and Matthew
        {
            //TODO create unchanging background for this section. top 5, left (72?) right +20
            for (int i = 0; i < 25; i++)
            {
                Console.SetCursorPosition(72, 5+i);
                Console.WriteLine("                    ");
            }
            Console.SetCursorPosition(74, 5);
            Console.WriteLine("Player's info");
            Console.SetCursorPosition(74, 6);
            Console.WriteLine("Statics");
            Console.SetCursorPosition(74, 7);
            Console.WriteLine("Inventory");
            Console.SetCursorPosition(74, 8);
            Console.WriteLine("Item slots");
            Console.SetCursorPosition(74, 9);
            Console.WriteLine("Puzzle Keys");
        }
        

        private ConsoleColor PathColor;
        private ConsoleColor WallColor;
        private bool[,] board = new bool[50,25];//Matthew

        /// <summary>
        /// Constructor builds board from filename
        /// </summary>
        /// <author>Ashton</author>
        /// <param name="FILEPATH">String of the file name of the Board</param>
        public Board(string FILEPATH)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(FILEPATH+".txt"))
                {

                    String line = sr.ReadLine();
                    

                    //set path color to first line
                    PathColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), line);

                    //set wall color to second line
                    line = sr.ReadLine();
                    WallColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), line);

                    //set the maze array to the rest of the lines.
                    line = sr.ReadToEnd();
                    int i = 0, j = 0;
                    bool[,] result = new bool[50, 25];
                    foreach (var row in line.Split('\n'))
                    {
                        j = 0;
                        foreach (var col in row.Trim().Split(','))
                        {
                            result[i, j] = bool.Parse(col.Trim());
                            j++;
                        }
                        i++;
                    }
                    board = result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Helper method to verify the bool array was read correctly
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <author>Ashton</author>
        /// <returns></returns>
        public bool GetCoordinate(int row, int column)
        {
            return board[row, column];
        }

        public ConsoleColor GetPathColor()
        {
            return PathColor;
        }
        public ConsoleColor GetWallColor()
        {
            return WallColor;
        }

        /// <summary>
        /// Prints the base maze/board layer
        /// </summary>
        public void PrintBoard()
        {
            Outline();PlayerInfo();GameFunction();
            for(int i = 0; i< 50;i++)
            {
                for(int j = 0; j<25;  j++)
                {
                    // print " " if wall or path
                    Console.SetCursorPosition(21+i,5+j);
                    if(board[i, j])//if wall use WallColor
                    {
                        Console.BackgroundColor = WallColor;
                    }
                    else //if not wall use path color
                    {
                        Console.BackgroundColor = PathColor;
                    }
                    Console.Write(" ");
                }
                Console.WriteLine();
            }


        }
        public void InverseBoard()
        {
           for(int i = 0; i< 25; i++)
            {
                for(int j = 0; j < 50; j++)
                {
                    board[j, i] = !board[j, i];
                }
            }
            this.PrintBoard();

        }
    }
  
}
