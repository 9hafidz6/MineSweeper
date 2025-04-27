using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class GridUpdate
    {
        private static readonly int[] rowDirections = { -1, 1, 0, 0 };
        private static readonly int[] colDirections = { 0, 0, -1, 1 };
        char[,]? ReferenceGrid;


        public void RenderuserGrid(int size, char[,] EmptyBoard)
        {
            // print the empty board
            Console.WriteLine("Here is your empty board:");

            for (int j = 0; j <= size; j++)
            {
                // Print column number, followed by a space for separation
                Console.Write(j + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            for (int i = 0; i < size; i++)
            {
                // letter (A-Z) and print it
                Console.Write((char)('a' + i) + " ");
                for (int j = 0; j < size; j++)
                {
                    Console.Write(EmptyBoard[i, j] + " ");
                }
                // breakline
                Console.WriteLine();
            }
        }

        // Main function to find and replace connected zeros
        public void ReplaceConnectedZeros(int size, char[,] board, char[,] EmptyBoard, int row, int column)
        {
            int height = size - 1;
            int width = size - 1;

            // copy board to EmptyBoard
            // declare 2d grid of the same size as the board
            ReferenceGrid = board.Clone() as char[,]; // create a copy of the board

            DFS(board, EmptyBoard, height, width, row, column);

            ReferenceGrid = null; // clear the reference grid
        }

        // Recursive Depth-First Search helper function
        private void DFS(char[,] board, char[,] EmptyBoard, int height, int width, int r, int c)
        {
            // Base Cases for the recursion:
            // 1. Check if the current cell (r, c) is out of bounds
            if (r < 0 || r > height || c < 0 || c > width)
            {
                return; // Stop if out of bounds
            }
            // 2. Check if the current cell is NOT a '0'
            //    If it's not '0', it's either already been visited (and is now '-')
            //    or it's some other character ('1', '2', '*', etc.).
            if (board[r, c] != '0')
            {
                return; // Stop if it's not a '0'
            }

            // If we reach here, it means grid[r, c] IS a '0' and is within bounds.
            // Action: Replace the '-' with '0'
            EmptyBoard[r, c] = '0'; // Mark the cell as visited in the empty board

            // checkd up, down, left, right of current cell
            for (int i = 0; i < 4; i++)
            {
                int row = r + rowDirections[i];
                int col = c + colDirections[i];
                if (row < 0 || row > height || col < 0 || col > width)
                {
                    continue;
                }
                else if (ReferenceGrid != null)
                {
                    EmptyBoard[row, col] = ReferenceGrid[row, col];
                }
                else
                {
                    Console.WriteLine("ReferenceGrid is null"); // Debugging line
                }
            }
            board[r, c] = '-';


            // Recursive Step: Explore all 4 neighbors (Up, Down, Left, Right)
            for (int i = 0; i < 4; i++)
            {
                int nextRow = r + rowDirections[i];
                int nextCol = c + colDirections[i];
                DFS(board, EmptyBoard, height, width, nextRow, nextCol); // Recursively call DFS on the neighbor
            }
        }
    }
}
