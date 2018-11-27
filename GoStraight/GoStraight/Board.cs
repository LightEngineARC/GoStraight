﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GoStraight
{
    public class Board
    {
        private int Height;
        private int Width;
        private ConsoleColor PathColor;
        private ConsoleColor WallColor;

        //TODO add these to maze files
        private int NumberOfBoards = 0;
        private IList<string> LinkedBoards;
        private IList<Coordinate> LinkedBoardCoordinates;

        private int NumberOfPuzzles = 0;
        private IList<Coordinate> PuzzleCoordinates;

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
                    //get height and width of the map.
                    String line = sr.ReadLine();
                    Width = int.Parse(line.Trim());
                    line = sr.ReadLine();
                    Height = int.Parse(line.Trim());
                    //set path color to first line
                    line = sr.ReadLine();
                    PathColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), line);

                    //set wall color to second line
                    line = sr.ReadLine();
                    WallColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), line);

                    //TODO read number of boards = 
                    //TODO read list of strings to add to LinkedBoards
                    //TODO add list of LinkedBoardCoordinates

                    //TODO read number of Puzzles
                    //TODO list of puzzle coords

                    //set the maze array to the rest of the lines.
                    line = sr.ReadToEnd();
                    int i = 0, j = 0;
                    bool[,] result = new bool[Width, Height];
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

        /// <summary>
        /// Returns if coordinates match a puzzle space
        /// </summary>
        /// <returns>bool</returns>
        public bool HasPuzzle(int x, int y)
        {
            return PuzzleCoordinates.Contains(new Coordinate { X=x, Y=y });
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
        public static void Outline()//Jae and Matthew
        {
            //set background for this position top 0 bottom 4 left 0 right (95?)
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
            //create unchanging background for this section top 5 bottom 55 left 20
            Console.SetCursorPosition(0, 5);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < 25; i++)
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
            //create unchanging background for this section. top 5, left (72?) right +20
            for (int i = 0; i < 25; i++)
            {
                Console.SetCursorPosition(72, 5 + i);
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

    }

}
