//using System;
//using System.Diagnostics.CodeAnalysis;

//namespace Minesweeper // Make sure this matches your console app's namespace
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            MessageWriter writer = new MessageWriter();
//            GridInitialization gridInit = new GridInitialization();
//            GridUpdate gridUpdate = new GridUpdate();
//            Common converter = new Common();

//            //writer.WriteHelloWorld();

//            Console.WriteLine("welcome to Minesweeper!");

//            Console.WriteLine("Enter the size of Grid (e.g. 4 for a 4x4 grid)");
//            int size = Convert.ToInt32(Console.ReadLine());
//            // Check if the size is valid
//            //writer.WriteArraySize(4); // Call the method to write the size
//            if (size < 2 || size > 20)
//            {
//                Console.WriteLine("Invalid grid size. Please enter a number between 2 and 20.");
//                return;
//            }

//            Console.WriteLine("Enter the number of mines to be placed on the grid (maximum is 35% of total squares)");
//            int NumOfMines = Convert.ToInt32(Console.ReadLine());

//            // check if the number of mines is valid
//            //writer.WriteNumOfMines(4); // Call the method to write the number of mines
//            if (NumOfMines < 1 || NumOfMines > (size * size) * 0.35)
//            {
//                Console.WriteLine("Invalid number of mines. Please enter a number between 1 and " + ((size * size) * 0.35));
//                return;
//            }

//            // Create the game board with mines
//            char[,] board = gridInit.CreateGrid(size, NumOfMines); // initialize the board with mines
//            char[,] EmptyBoard = gridInit.CreateCopyGrid(size); // create an empty board to show the player

//            // print the game board
//            Console.WriteLine("Here is your minefield:");
//            for (int j = 0; j <= size; j++)
//            {
//                // Print column number, followed by a space for separation
//                Console.Write(j + " ");
//            }
//            Console.WriteLine();
//            Console.WriteLine();

//            for (int i = 0; i < size; i++)
//            {
//                Console.Write((char)('a' + i) + " ");
//                for (int j = 0; j < size; j++)
//                {
//                    Console.Write(board[i, j] + " ");
//                }
//                // breakline
//                Console.WriteLine();
//            }

//            gridUpdate.RenderuserGrid(size, EmptyBoard);

//            while (true)
//            {
//                string GameStatus = "";

//                Console.Write("Select a square to reveal (e.g. A1): ");
//                string grid = Console.ReadLine() ?? string.Empty;
//                if (grid == string.Empty)
//                {
//                    Console.WriteLine("Please enter a valid grid location.");
//                    continue;
//                }
//                if (grid.Length < 2)
//                {
//                    Console.WriteLine("Please enter a valid grid location.");
//                    continue;
//                }

//                // Convert the grid input to row and column indices
//                int row = converter.ConvertLetterToIndex(grid[0]);
//                int column = int.Parse(grid.Substring(1));

//                // Check if the input is valid
//                if (row < 1 || row > size || column < 1 || column > size)
//                {
//                    Console.WriteLine("Invalid input. Please enter a valid grid location.");
//                    continue;
//                }
//                //Console.WriteLine($"You selected: Row {row}, Column {column}");

//                // update empty board with the selected cell cross check with board
//                // check if the cell is a mine
//                if (board[row - 1, column - 1] == '*')
//                {
//                    Console.WriteLine("Game Over! You hit a mine.");
//                    break;
//                }
//                else
//                {
//                    // flood fill the empty board with the number of mines around the selected cell
//                    // check if the cell is already revealed
//                    if (EmptyBoard[row - 1, column - 1] != '_')
//                    {
//                        Console.WriteLine("You already revealed this cell.");
//                        continue;
//                    }
//                    // check if the cell is empty
//                    if (EmptyBoard[row - 1, column - 1] == '_')
//                    {
//                        if (board[row - 1, column - 1] == '0')
//                        {
//                            // find all connected '0' in board via depth first search                              
//                            gridUpdate.ReplaceConnectedZeros(size, board, EmptyBoard, row - 1, column - 1);
//                        }
//                        else if (board[row - 1, column - 1] == '*')
//                        {
//                            // user lost
//                            GameStatus = "Lose";
//                        }
//                        else
//                        {
//                            // update the empty board with the number of mines around the selected cell
//                            EmptyBoard[row - 1, column - 1] = board[row - 1, column - 1];
//                        }
//                        gridUpdate.RenderuserGrid(size, EmptyBoard);
//                    }

//                    // check win condition
//                    int EmptyCount = 0; // reset empty cell count
//                    for (int i = 0; i < size; i++)
//                    {
//                        for (int j = 0; j < size; j++)
//                        {
//                            if (EmptyBoard[i, j] == '_')
//                            {
//                                EmptyCount++;
//                            }
//                        }
//                    }
//                    GameStatus = EmptyCount == NumOfMines ? "Win" : "Play";

//                    if (GameStatus == "Win" || GameStatus == "Lose")
//                    {
//                        string msg = GameStatus == "Win" ? "Congratulations, you won the game" : "Oh no, you detonated a mine! Game Over";

//                        Console.WriteLine(msg);

//                        Console.Write("Press any key to play again");
//                        string replayStatus = Console.ReadLine() ?? string.Empty;

//                        if (replayStatus == "Y")
//                        {
//                            continue;
//                        }
//                        else
//                        {
//                            break;
//                        }

//                    }
//                }
//            }
//        }
//    }
//}

using System;
using System.Diagnostics.CodeAnalysis;

namespace Minesweeper 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Instantiate necessary classes
            MessageWriter writer = new MessageWriter(); // Assuming MessageWriter exists for potential future use
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
                    // Use flood fill to reveal connected empty squares and adjacent numbered squares
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
                hiddenCount = MiscFunc.CountHiddenCells(size, userBoard, hiddenCount);

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

    // Assuming these classes exist in your project with the necessary methods:
    // MessageWriter (optional, for custom messages)
    // GridInitialization (for creating the board and user board)
    // GridUpdate (for rendering the user board and handling flood fill)
    // Common (for converting letter input to row index)
}
