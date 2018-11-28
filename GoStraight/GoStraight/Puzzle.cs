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
        private static bool isTrapted;
        public static bool isFail { get; set; } = false;
        private static int X= 10, Y = 5;
        public static int puzzleCount { get; set; } = 0;

        public static void MultiplePuzzle()
        {
            //if (x==X && y==Y)
            //{
            //    isTrapted = true;
            //}

            //if (isTrapted)
            
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;

                Random r = new Random();
                int p = r.Next(1, 10);
                int q = r.Next(1, 10);
                int answer = p * q;
                Console.WriteLine($"What is {p} * {q}?");
                Console.Write("Your Answer: ");
                int reply = Convert.ToInt32(Console.ReadLine());

                if (reply != (answer))
                {
                    Console.WriteLine("You faild to solve the answer!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    isFail = true;
                   // Console.WriteLine("GAME OVER!");
                    return;
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("What is Sin(pi) * Cos(0)?");
                    Console.Write("Your Answer: ");
                    int reply1 = Convert.ToInt32(Console.ReadLine());
                    if (reply1 == 0)
                    {
                        Console.WriteLine("Okay, Let's Continue!");
                        Thread.Sleep(1000);
                        Console.Clear();
                        puzzleCount++;
                        return;
                    }
                    else
                    {
                        Console.WriteLine("You faild to solve the answer!");
                        Thread.Sleep(2000);
                        Console.WriteLine("GAME OVER!");
                        Console.Clear();
                        isFail = true;
                        return;
                    }

                }
            
        }
    }
}
