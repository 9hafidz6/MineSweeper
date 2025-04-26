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
        public void WriteHelloWorld_ShouldOutputHelloWorld()
        {
            // Arrange: Redirect console output to capture it.
            using var sw = new StringWriter();
            Console.SetOut(sw);
            MessageWriter writer = new MessageWriter(); // Create an instance of the class

            // Act: Invoke the method that writes to the console.
            writer.WriteHelloWorld();

            // Assert: Verify the expected output.
            string expected = "Hello, World!" + Environment.NewLine;
            Assert.AreEqual(expected, sw.ToString(), ignoreCase: false);

            // Restore the standard output (optional, but good practice if other tests rely on it)
            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }
        [TestMethod]
        public void testArraySize()
        {
            // Arrange: Redirect console input to capture it.
            using var sw = new StringWriter();
            Console.SetOut(sw);
            MessageWriter writer = new MessageWriter(); // Create an instance of the class
            // Act: Invoke the method that writes to the console.
            writer.WriteArraySize(4);
            // Assert: Verify the expected output.
            string expected = "The size of the grid is: 4" + Environment.NewLine;
            Assert.AreEqual(expected, sw.ToString(), ignoreCase: false);

            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }

        [TestMethod]
        public void testNumOfMines()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);
            MessageWriter writer = new MessageWriter();
            // Act: Invoke the method that writes to the console.
            writer.WriteNumOfMines(4);
            // Assert: Verify the expected output.
            string expected = "The number of mines is: 4" + Environment.NewLine;
            Assert.AreEqual(expected, sw.ToString(), ignoreCase: false);
            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);

        }
    }
}
