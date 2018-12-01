using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoStraight
{
    class PuzzleQuestions
    {
        private static Random rand = new Random();
        /// <summary>
        /// Puzzle Array that has all the Questions and answers for the puzzles and traps.
        /// </summary>
        /// <returns></returns>
        public static Puzzle[] PuzzleArray()
        {
            int number1 = rand.Next(1, 15);
            int number2 = rand.Next(1, 12);
            int number3 = rand.Next(1, 9);
            int number4 = rand.Next(1, 6);
            int number5 = rand.Next(1, 3);
            int number6 = rand.Next(1, 2);

            String[] mazeFileName = { "KMaze", "Maze", "MyMaze" };
            Coordinate coordinate1 = new Coordinate
            {
                X = 40,
                Y = 10
            };
            int p = rand.Next(1, 10);
            int q = rand.Next(1, 10);
            Puzzle puzzle1 = new Puzzle(mazeFileName[2], coordinate1, true, $"What is {p} * {q}?", (p * q).ToString());

            Coordinate coordinate2 = new Coordinate
            {
                X = 46,
                Y = 13
            };
            Puzzle puzzle2 = new Puzzle(mazeFileName[2], coordinate2, false, "What is Sin(pi) * Cos(0)?", "0");

            Coordinate coordinate3 = new Coordinate
            {
                X = 55,
                Y = 15
            };
            Puzzle puzzle3 = new Puzzle(mazeFileName[0], coordinate3, true,
                $"What is ({number1} + {number1}) * {number2}?",
                ((number1 + number1) * number2).ToString());

            Coordinate coordinate4 = new Coordinate
            {
                X = 30,
                Y = 13
            };
            Puzzle puzzle4 = new Puzzle(mazeFileName[0], coordinate4, false,
                "What can point in every direction but can't reach the destination by itself?" +
                "\na.your finger\n" +
                "b.a map\n" +
                "c.a compass",
                "a");

            Coordinate coordinate5 = new Coordinate
            {
                X = 55,
                Y = 15
            };
            Puzzle puzzle5 = new Puzzle(mazeFileName[0], coordinate3, true,
                "What hired killer never goes to jail?" +
                "\na. a hitman\n" +
                "b. an exterminator\n" +
                "c. a mercenary",
                "b");

            Coordinate coordinate6 = new Coordinate
            {
                X = 34,
                Y = 7
            };
            Puzzle puzzle6 = new Puzzle(mazeFileName[0], coordinate3, true,
                $"What is {number1} + {number1}?",
                ((number1 + number1).ToString()));

            Coordinate coordinate7 = new Coordinate
            {
                X = 46,
                Y = 9
            };
            Puzzle puzzle7 = new Puzzle(mazeFileName[0], coordinate3, true,
                "What can't be burned in fire, nor drowned in water?" +
                "\na. ice\n" +
                "b. paper\n" +
                "c. iron",
                "a");

            Coordinate coordinate8 = new Coordinate
            {
                X = 25,
                Y = 40
            };
            Puzzle puzzle8 = new Puzzle(mazeFileName[0], coordinate3, false,
                $"What is ({number2} * {number4}) + ({number3} * {number5})",
                (((number2 * number4) + (number3 * number5)).ToString()));

            Puzzle[] puzzles = new Puzzle[] { puzzle1, puzzle2, puzzle3, puzzle4, puzzle5, puzzle6, puzzle7, puzzle8 };
                    
            return puzzles;
        }
    }
}