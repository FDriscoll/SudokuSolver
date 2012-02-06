﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SudokuProjectClassLibrary
{
    public class SudokuGrid
    {
        public SudokuGrid(string sudokuPath)
        {
            Grid = ReadGridFromFile(sudokuPath);
        }

        public SudokuGrid(int[,] grid)
        {
            Grid = grid;
        }

        public int[,] Grid { get; set; }
        
        public void PrintGrid()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write("{0}", Grid[i, j]);
                }
                Console.WriteLine("");
            }
        }

        public List<SquareCoordinate> FindAllEmptySquares()
        {
            var emptySquareList = new List<SquareCoordinate>();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Grid[i, j] == -1)
                    {
                        emptySquareList.Add(new SquareCoordinate(i, j));
                    }
                }
            }

            return emptySquareList;
        }

        public void FillInSquare(SquareCoordinate squareCoordinate, int value)
        {
            Grid[squareCoordinate.Row, squareCoordinate.Column] = value;
        }

        // Method takes a path to a text file in which a Sudoku is saved as a 9 x 9 grid of integers, with underscores representing empty squares.
        // Converts the Sudoku from the text file into a 9 x 9 array where empty squares are represented as -1s.
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
