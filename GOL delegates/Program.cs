using System;
using System.Text;
using System.Threading;

namespace GOL
{
    internal class Program
    {
        public static StringBuilder sb = new StringBuilder();
        public static Random random = new Random();
        public static int columns = 116;
        public static int rows = 16;
        public static int neighbours = 0;
        public static int cellCounter = 0;
        public static bool[,] grid = new bool[columns, rows];
        public static bool[,] newGrid = new bool[columns, rows];

        private static void Main(string[] args)
        {
            Loop(CreateGrid);

            while (true)
            {
                Loop(Print);
                Loop(Evolve);

                Thread.Sleep(100);
            }
        }

        public static void Loop(Action<int, int> functionParam)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    functionParam(column, row);
                }
            }
        }

        public static void CreateGrid(int column, int row)
        {
            grid[column, row] = random.Next(4) == 0;
        }

        public static void Print(int column, int row)
        {
            if (column == columns - 1)
            {
                sb.Append(Environment.NewLine);
            }
            else
            {
                sb.AppendFormat("{0}", grid[column, row] ? "#" : " ");
            }

            if (cellCounter++ == grid.Length - 1)
            {
                Console.WriteLine(sb);

                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;

                sb.Clear();
                cellCounter = 0;
            }
        }

        public static int GetNeighbours(int column, int row)
        {
            neighbours = 0;

            neighbours += (row > 0 && grid[column, row - 1] ? 1 : 0);
            neighbours += (row > 0 && column < columns - 1 && grid[column + 1, row - 1] ? 1 : 0);
            neighbours += (column < columns - 1 && grid[column + 1, row] ? 1 : 0);
            neighbours += (row < rows - 1 && column < columns - 1 && grid[column + 1, row + 1] ? 1 : 0);
            neighbours += (row < rows - 1 && grid[column, row + 1] ? 1 : 0);
            neighbours += (row < rows - 1 && column > 0 && grid[column - 1, row + 1] ? 1 : 0);
            neighbours += (column > 0 && grid[column - 1, row] ? 1 : 0);
            neighbours += (row > 0 && column > 0 && grid[column - 1, row - 1] ? 1 : 0);

            return neighbours;
        }

        public static void Evolve(int column, int row)
        {
            neighbours = GetNeighbours(column, row);

            if (grid[column, row])
            {
                if (neighbours < 2 || neighbours > 3)
                {
                    newGrid[column, row] = false;
                }
                else if (neighbours == 2 || neighbours == 3)
                {
                    newGrid[column, row] = true;
                }
            }
            else
            {
                if (neighbours == 3)
                {
                    newGrid[column, row] = true;
                }
            }

            if (cellCounter++ == grid.Length - 1)
            {
                grid = (bool[,])newGrid.Clone();
                cellCounter = 0;
            }
        }
    }
}