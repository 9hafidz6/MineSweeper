using System;

namespace Minesweeper // Make sure this matches your console app's namespace
{
    public class MessageWriter
    {
        public void WriteHelloWorld()
        {
            Console.WriteLine("Hello, World!");
        }

        public void WriteArraySize(int size)
        {
            Console.WriteLine($"The size of the grid is: {size}");
        }
        public void WriteNumOfMines(int numOfMines)
        {
            Console.WriteLine($"The number of mines is: {numOfMines}");
        }

        // You would add other testable methods here
        // For example, a method to print the game board
        // public void PrintBoard(char[,] board) { ... }
    }
}