using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Common
    {
        public int ConvertLetterToIndex(char letter)
        {
            if (letter < 'A' || letter > 'Z')
            {
                return -1;
            }
            return char.ToUpper(letter) - 'A'; // Convert letter to index (A=0, B=1, ...)
        }

        public int CountHiddenCells(int size, char[,] userBoard, int hiddenCount)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (userBoard[i, j] == '_')
                    {
                        hiddenCount++;
                    }
                }
            }

            return hiddenCount;
        }
    }
}
