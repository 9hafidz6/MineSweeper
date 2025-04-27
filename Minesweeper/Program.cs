using System;
using System.Diagnostics.CodeAnalysis;

namespace Minesweeper 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Instantiate necessary classes
            GridInitialization gridInit = new GridInitialization();
            GridUpdate gridUpdate = new GridUpdate();
            Common MiscFunc = new Common();

            Console.WriteLine("Welcome to Minesweeper!");

            int size;
            int numOfMines;

            // --- Get and Validate Grid Size ---

            while (true) 
            {
                Console.Write("Enter the size of Grid (e.g. 4 for a 4x4 grid, between 2 and 20): ");
                string sizeInput = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(sizeInput, out size)) // Try parsing input as integer
                {
                    if (size >= 2 && size <= 20) // Check if size is within valid range
                    {
                        break; // Valid size entered, exit loop
                    }
                    else
                    {
                        Console.WriteLine("Invalid grid size. Please enter a number between 2 and 20.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            // --- Get and Validate Number of Mines ---
            int maxMines = (int)Math.Floor((size * size) * 0.35); // Calculate maximum allowed mines
            while (true) 
            {
                Console.Write($"Enter the number of mines (between 1 and {maxMines}): ");
                string minesInput = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(minesInput, out numOfMines)) // Try parsing input as integer
                {
                    if (numOfMines >= 1 && numOfMines <= maxMines) // Check if number of mines is within valid range
                    {
                        break; // Valid number of mines entered, exit loop
                    }
                    else
                    {
                        Console.WriteLine($"Invalid number of mines. Please enter a number between 1 and {maxMines}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            // --- Game Setup ---
            // Create the game board with mines and calculated adjacent mine counts
            char[,] board = gridInit.CreateGrid(size, numOfMines);
            // Create an empty board to show the player, initially all hidden ('_')
            char[,] userBoard = gridInit.CreateCopyGrid(size);

            // --- Game Loop ---
            bool gameOver = false;
            bool hasWon = false;

            while (!gameOver)
            {
                // Render the current state of the user's board
                gridUpdate.RenderuserGrid(size, userBoard);

                // --- Get and Validate User Input for Square Selection ---
                int row = -1;
                int column = -1;
                bool validInput = false;

                while (!validInput)
                {
                    Console.Write("Select a square to reveal (e.g., A1): ");
                    string gridInput = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty; // Read input, trim whitespace, convert to uppercase

                    if (gridInput.Length >= 2)
                    {
                        string colString = gridInput.Substring(1);
                        row = MiscFunc.ConvertLetterToIndex(gridInput[0]);

                        /// check row valid
                        if (row == -1)
                        {
                            Console.WriteLine("Invalid input. Please enter a valid row (A~T)");
                        }
                        // check column valid
                        else if (int.TryParse(colString, out column))
                        {
                            column--;
                            // Check if the converted indices are within the board bounds
                            if (row >= 0 && row < size && column >= 0 && column < size)
                            {
                                // Check if the cell has already been revealed
                                if (userBoard[row, column] != '_')
                                {
                                    Console.WriteLine("This square has already been revealed. Please select another.");
                                }
                                else
                                {
                                    validInput = true; // Input is valid and cell is not revealed
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid grid location (e.g., A1).");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid column number (e.g., A1).");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input format. Please enter a letter followed by a number (e.g., A1).");
                    }
                }

                // --- Process User Selection ---
                // Check the actual board content at the selected location
                char revealedContent = board[row, column];

                if (revealedContent == '*') // Hit a mine
                {
                    gameOver = true;
                    hasWon = false; // Game over, and the player lost
                    userBoard[row, column] = '*'; // Show the mine on the user's board
                }
                else if (revealedContent == '0') // Hit an empty square (0 adjacent mines)
                {
                    // Use dfs flood fill to reveal connected empty squares and adjacent numbered squares
                    gridUpdate.ReplaceConnectedZeros(size, board, userBoard, row, column);
                }
                else // Hit a numbered square (1-8 adjacent mines)
                {
                    // Reveal the number on the user's board
                    userBoard[row, column] = revealedContent;
                }

                // --- Check Win Condition ---
                // Count how many squares are still hidden ('_')
                int hiddenCount = 0;
                hiddenCount = MiscFunc.CountHiddenCells(size, userBoard);

                // If the number of hidden squares equals the number of mines, the player has won
                if (hiddenCount == numOfMines)
                {
                    gameOver = true;
                    hasWon = true; // Player won
                }
            }

            // --- Game End ---
            // Render the final state of the user's board (including the mine if lost)
            gridUpdate.RenderuserGrid(size, userBoard);

            if (hasWon)
            {
                Console.WriteLine("Congratulations, you won the game!");
            }
            else
            {
                Console.WriteLine("Oh no, you detonated a mine! Game Over.");
            }

            // --- Play Again Prompt ---
            Console.Write("Press 'Y' to play again, or any other key to exit: ");
            string playAgainInput = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

            if (playAgainInput == "Y")
            {
                // If 'Y' is pressed, restart the game by calling Main again
                Main(args);
            }
            else
            {
                // Otherwise, the program will exit
                Console.WriteLine("Thanks for playing!");
            }
        }


    }
}
