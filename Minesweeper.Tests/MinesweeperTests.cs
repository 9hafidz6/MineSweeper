using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper; // Ensure this matches the namespace of your console app

namespace Minesweeper.Tests
{
    [TestClass]
    public class MessageWriterTests
    {

        [TestMethod]
        public void testArraySize()
        {
            // Arrange: Create an instance of the class and define expected dimensions.
            GridInitialization GridTest = new GridInitialization();
            int expectedRows = 10;
            int expectedCols = 10;

            // Act: Invoke the method to create the grid.
            char[,] board = GridTest.CreateGrid(expectedRows, expectedCols); // Pass expected values for clarity

            // Assert: Verify the actual dimensions match the expected dimensions.
            int actualRows = board.GetLength(0); // Get actual rows
            int actualCols = board.GetLength(1); // Get actual columns

            // Assert using direct integer comparison
            Assert.AreEqual(expectedRows, actualRows, "The number of rows in the grid is incorrect.");
            Assert.AreEqual(expectedCols, actualCols, "The number of columns in the grid is incorrect.");

            char[,] CopyBoard = GridTest.CreateCopyGrid(expectedRows);
            // Assert: Verify the actual dimensions match the expected dimensions.
            int actualCopyRows = board.GetLength(0); // Get actual rows
            int actualCopyCols = board.GetLength(1); // Get actual columns

            // Assert using direct integer comparison
            Assert.AreEqual(expectedRows, actualCopyRows, "The number of Copy rows in the grid is incorrect.");
            Assert.AreEqual(expectedCols, actualCopyCols, "The number of Copy columns in the grid is incorrect.");

        }

        [TestMethod]
        public void testCharConversion()
        {

            Common CharConvertTest = new Common();
            // Act: Invoke the method
            int expected = 0;

            int actualIndex = CharConvertTest.ConvertLetterToIndex('A');
            // Assert: Verify the expected output.
            Assert.AreEqual(expected, actualIndex, "The index of the letter is incorrect.");

        }

        [TestMethod]
        public void counHiddenCells()
        {
            int expectedCount = 5;
            int size = 10;
            Common countCells = new Common();

            char[,] userBoard = new char[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    userBoard[i, j] = '0'; // Use '-' to represent an empty cell
                }
            }
            userBoard[5, 5] = '_';
            userBoard[4, 6] = '_';
            userBoard[3, 7] = '_';
            userBoard[2, 8] = '_';
            userBoard[1, 9] = '_';

            int actualCount = countCells.CountHiddenCells(size, userBoard);

            Assert.AreEqual(expectedCount, actualCount, "The count of hidden cells is incorrect.");
        }
    }
}
