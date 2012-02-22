using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SudokuProjectClassLibrary;

namespace SudokuWriterConsoleApplication
{
    class SudokuWriterProgram
    {
        static void Main(string[] args)
        {
            SudokuWriter sudokuWriter = new SudokuWriter();
            ImmutableSudokuGrid filledGrid = sudokuWriter.CreateFilledGrid();
            filledGrid.PrintGrid();

            Console.WriteLine("\n");

            ImmutableSudokuGrid emptiedGrid = sudokuWriter.EmptyGridForHardSolve(filledGrid);
            emptiedGrid.PrintGrid();

            Console.WriteLine("\n");

            SudokuGrid mutableEmptiedGrid = emptiedGrid.MakeMutableCopy();
            SudokuSolver sudokuSolver = new SudokuSolver();
            sudokuSolver.Solve(mutableEmptiedGrid);
            mutableEmptiedGrid.PrintGrid();

            if (mutableEmptiedGrid.FindAllEmptySquares().Count != 0)
            {
                Console.WriteLine("\n");
                HarderSudokuSolver harderSolver = new HarderSudokuSolver();
                ImmutableSudokuGrid solvedGrid = harderSolver.Solve(emptiedGrid);
                solvedGrid.PrintGrid();
            }
        }
    }
}
