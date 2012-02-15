using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SudokuProjectClassLibrary;

namespace SudokuConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Do you want me to try and solve an Easy Sudoku (type 1) or a Very Hard Sudoku (type 2)?");
            string difficulty = Console.ReadLine();
            if (difficulty == "1") { difficulty = "Easy" ; }
            else if (difficulty == "2") { difficulty = "VeryHard"; }
            else
            {
                Console.WriteLine("Stop being difficult.  I'm going to solve an easy one then...");
                difficulty = "Easy";
            }

            ImmutableSudokuGrid grid = new ImmutableSudokuGrid(@"..\..\..\" + difficulty + @"Sudoku.txt");

            if (grid.Elements == null)
            {
                Console.WriteLine("Error, cannot read grid from file.");
                return;
            }

            HarderSudokuSolver sudokuSolver = new HarderSudokuSolver();
            sudokuSolver.Solve(grid).PrintGrid();
            
         }
    }
}
