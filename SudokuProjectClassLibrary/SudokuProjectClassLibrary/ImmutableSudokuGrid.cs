using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SudokuProjectClassLibrary
{
    public class ImmutableSudokuGrid : IImmutableSudokuGrid
    {        
        public ImmutableSudokuGrid(string path)
        {
            Elements = ReadGridFromFile(path);
        }

        public ImmutableSudokuGrid(int[,] array)
        {
            Elements = (int[,])array.Clone();
        }

        public int[,] Elements { get; private set; }

        public SudokuGrid MakeMutableCopy()
        {
            return new SudokuGrid((int[,])Elements.Clone());
        }
        
        public List<SquareCoordinate> FindAllEmptySquares()
        {
            var emptySquareList = new List<SquareCoordinate>();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Elements[i, j] == -1)
                    {
                        emptySquareList.Add(new SquareCoordinate(i, j));
                    }
                }
            }

            return emptySquareList;
        }

        public ImmutableSudokuGrid WithExtraSquare(SquareCoordinate squareCoordinate, int value)
        {
            int[,] arrayForNewGrid = (int[,])Elements.Clone();
            arrayForNewGrid[squareCoordinate.Row, squareCoordinate.Column] = value;
            return new ImmutableSudokuGrid(arrayForNewGrid);
        }

        public void PrintGrid()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write("{0}", Elements[i, j]);
                }
                Console.WriteLine("");
            }
        }
        
        private int[,] ReadGridFromFile(string sudokuPath)
        {
            string sudokuString = File.ReadAllText(sudokuPath);
            sudokuString = sudokuString.Replace("\r", "");
            sudokuString = sudokuString.Replace("\n", "");
            
            int[,] grid = new int[9,9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    char numberToInsert = sudokuString[0];

                    if (numberToInsert == '_')
                    {
                        grid[i, j] = -1;
                    }
                    else if ((int)Char.GetNumericValue(numberToInsert) <= 9 && (int)Char.GetNumericValue(numberToInsert) >= 1)
                    {
                        grid[i, j] = (int)Char.GetNumericValue(numberToInsert);
                    }
                    else
                    {
                        return null;
                    }
                    sudokuString = sudokuString.Remove(0, 1);
                }
            }

            return grid;
        }
    }
}
