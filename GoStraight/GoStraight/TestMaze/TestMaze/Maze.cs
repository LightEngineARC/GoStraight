using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestMaze
{

    class Maze
    {
        public enum direction { up, down, left, right};

		bool[,] isWalls =
        {
		    {false, false, false, false },
		    {true, false, true, false },
		    {false, false, false, true },
		    {false, true, false, false }
	    };
        int[] playerPosition = { 0, 0 };
        public void PrintMaze()
        {
            Console.SetCursorPosition(0, 0);

            Console.BackgroundColor = ConsoleColor.Blue;
            for (int i = 0; i <= isWalls.GetLength(0) + 1; i++)  //print top outer wall
            {
                Console.Write(" ");
            }
            Console.WriteLine();
            for (int i = 0; i < isWalls.GetLength(0); i++)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write(" ");  // print left outer wall
                for (int j = 0; j < isWalls.GetLength(1); j++)
                {
                    Console.BackgroundColor = ((isWalls[i, j]) ? (ConsoleColor.Blue) : (ConsoleColor.Black));
                    Console.Write(" ");
                }
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write(" ");  // print right outer wall

                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Blue;
            for (int i = 0; i <= isWalls.GetLength(0) + 1; i++)  //print bottom outer wall
            {
                Console.Write(" ");
            }
        }
        public void PrintPlayer()
        {
            Console.SetCursorPosition(playerPosition[0] + 1, playerPosition[1] + 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write((char)41372);
        }
        public void PrintPlayer(int[] position)
        {
            Console.SetCursorPosition(position[0] + 1, position[1] + 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write((char)(50883));
        }

        public void MovePlayer()
        {
            playerPosition[0]++;
            PrintPlayer();
            Thread.Sleep(1000);

            playerPosition[0]++;
            PrintPlayer();
            Thread.Sleep(1000);

            playerPosition[0]--;
            PrintPlayer();
            Thread.Sleep(1000);

            playerPosition[1]++;
            PrintPlayer();
            Thread.Sleep(1000);

            playerPosition[1]++;
            PrintPlayer();
            Thread.Sleep(1000);

            playerPosition[0]++;
            PrintPlayer();
            Thread.Sleep(1000);

            playerPosition[1]++;
            PrintPlayer();
            Thread.Sleep(1000);

            playerPosition[0]++;
            PrintPlayer();
            Thread.Sleep(1000);
        }

        public void MovePlayer(direction d)  // in the works
        {
            //int[] squareToMoveTo = {playerPosition[0] + 1, 0}
            if (d.Equals(direction.up))
            {
                playerPosition[1]++;
            }
            if (d.Equals(direction.down))
            {
                playerPosition[1]--;
            }
            if (d.Equals(direction.left))
            {
                playerPosition[0]++;
            }
            if (d.Equals(direction.right))
            {
                playerPosition[0]--;
            }
        }
    }
}
