using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoStraight
{
    class Puzzle
    {
        private ConsoleColor PuzzleBlock { get; }

        /// <summary>
        /// string for filename of maze that the Puzzle belongs to
        /// </summary>
        public string Maze { get; }
        /// <summary>
        /// positon on the puzzle. Player steps on this coordinate and the puzzle triggers.
        /// </summary>
        public Coordinate PuzzleLocation { get; }
        /// <summary>
        /// true if is a trap and should noy be printed on the screen. If false, print the puxxle character.
        /// </summary>
        public bool IsTrap { get; }

        /// <summary>
        /// represents the text of the question the player will be asked
        /// </summary>
        private string Question { get;  }
        /// <summary>
        /// each element represents an answer to the question that is considered correct. 
        /// If the player's answer string is equal to ANY of the strings contained in this array, they pass the question. 
        /// </summary>
        private string[] Answers { get;  }
        /// <summary>
        /// Tracks whether the individual puzzle has already been solved.
        /// </summary>
        public bool HasBeenSolved { get; set; } = false;

        public static bool isFail { get; set; } = false;
        public static int puzzleCount { get; set; } = 0;

        // static fields/constants:
        public static readonly char PuzzleDisplay = (char)1421;  // U+058d  '֍'

        /// <summary>
        /// Ctor for a puzzle object. Each Puzzle object can only contain one question, though one question may have multiple answers.
        /// </summary>
        /// <param name="mazeFileName"></param>
        /// <param name="puzzleLocation"></param>
        /// <param name="isTrap"></param>
        /// <param name="question"></param>
        /// <param name="answers"></param>
        /// <author>Matthew Crump</author>
        public Puzzle(string mazeFileName, Coordinate puzzleLocation, bool isTrap, string question, string[] answers)
        {
            this.Maze = mazeFileName;
            this.PuzzleLocation = puzzleLocation;
            this.IsTrap = isTrap;
            this.Question = question;
            if (answers.Length == 0)   // checks to make sure there is at least one element as an answer
                throw new ArgumentException("The constructor for Puzzle must be provided at least 1 valid element in the answer array.");
            else
                this.Answers = answers;
        }

        /// <summary>
        /// Overloaded ctor for use when only one answer needs to be provided.
        /// </summary>
        /// <param name="mazeFileName"></param>
        /// <param name="puzzleLocation"></param>
        /// <param name="isTrap"></param>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        /// <author>Matthew Crump</author>
        public Puzzle(string mazeFileName, Coordinate puzzleLocation, bool isTrap, string question, string answer) : 
            this(mazeFileName, puzzleLocation, isTrap, question, new string[] { "Placeholder DEBUG" })
        {
            this.Answers = new string[] { answer };
        }

        /// <summary>
        /// Asks the question for the puzzle and returns true if the player passes the puzzle. 
        /// </summary>
        /// <returns></returns>
        public Boolean RunPuzzle()
        {
            // setup for the question
            //Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            (new Board("BlankMaze")).PrintBoard();   //clears the area for the question to appear
            Console.ReadKey();  //clears the reader so that the next input line is empty (IMPORTANT!)
 
            // beggining of first question:
            Console.SetCursorPosition(21, 5);
            Console.WriteLine(Question);
            Console.SetCursorPosition(21, 6);
            Console.Write("Your Answer: ");
            string reply = Console.ReadLine(); 

            if (!MatchesAnswer(reply))  // player answers wrong: gane over
            {
                Console.SetCursorPosition(21, 7);
                Console.WriteLine("You faild to solve the answer!");
                Thread.Sleep(2000);
                Console.Clear();
                //isFail = true;
                Console.WriteLine("GAME OVER!");
                //reply = "";
                //Console.ReadKey();
                return false;
            }

            else  //player answers right
            {
                Console.SetCursorPosition(21, 7);
                Console.WriteLine("Okay, Let's Continue!");
                Thread.Sleep(1000);
                Console.Clear();
                HasBeenSolved = true;
                puzzleCount++;
                //Console.ReadLine();
                //reply = "";
                return true;
            }

        }

        // returns true if the string passes equals one of the elements in the Answers array
        private bool MatchesAnswer(string response)
        {
            foreach (string el in Answers)
            {
                if (el.Equals(response))
                    return true;
            }

            return false;
        }
    }
}
