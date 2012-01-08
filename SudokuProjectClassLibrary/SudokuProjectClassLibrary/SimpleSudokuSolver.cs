using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    // The Simple Sudoku Solver can solve Sudokus where at every stage there is at least one empty square for which there is only one option
    // So all the eight other values already occur in the same row/column/box as that square.
    public class SimpleSudokuSolver : SudokuSolver
    {
        public SimpleSudokuSolver(SudokuGrid grid)
            : base(grid)
        { }

        public void Solve()
        {
            Solve(TellUserThatSudokuIsTooHard, TellUserThatSomethingsGoneWrong);
        }

        public void TellUserThatSudokuIsTooHard()
        {
            Console.WriteLine("This Sudoku Is Too Hard");
            return;
        }

        public void TellUserThatSomethingsGoneWrong()
        {
            Console.WriteLine("Your Sudoku Was Invalid");
            return;
        }
    }
}
