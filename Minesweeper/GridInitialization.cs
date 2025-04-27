using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class GridInitialization
    {
        public char[,] CreateGrid(int size, int NumOfMines)
        {


            char[,] grid = new char[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = '0'; // Use '-' to represent an empty cell
                }
            }

            // Place mines randomly on the board
            Random random = new Random();
            int minesPlaced = 0;
            while (minesPlaced < NumOfMines)
            {
                int row = random.Next(size);
                int col = random.Next(size);
                // Check if the cell already has a mine
                if (grid[row, col] != '*')
                {
                    grid[row, col] = '*'; // Use '*' to represent a mine
                    minesPlaced++;
                }
            }

            // initialize the numbers around the mines
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (grid[i, j] == '*')
                    {
                        UpdateSurroundingCells(size, grid, i, j);
                    }
                }
            }

            return grid;
        }

        private static void UpdateSurroundingCells(int size, char[,] grid, int i, int j)
        {
            // Increment the count for all adjacent cells
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue; // Skip the mine cell itself
                    int newRow = i + x;
                    int newCol = j + y;
                    if (newRow >= 0 && newRow < size && newCol >= 0 && newCol < size && grid[newRow, newCol] != '*')
                    {
                        grid[newRow, newCol]++; // Increment the number of adjacent mines
                    }
                }
            }
        }

        public char[,] CreateCopyGrid(int size)
        {
            char[,] grid = new char[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = '_'; // Use '-' to represent an empty cell
                }
            }
            return grid;
        }
    }
}
