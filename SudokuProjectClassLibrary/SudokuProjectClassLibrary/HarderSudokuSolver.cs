using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    public class HarderSudokuSolver : SudokuSolver
    {
        public HarderSudokuSolver(SudokuGrid grid)
            : base(grid)
        {}


        public SudokuGrid SolveBetter(SudokuGrid inputGrid)
        {
            SudokuGrid grid = inputGrid;

            SudokuSolver simpleSolver = new SudokuSolver(grid);
            grid = simpleSolver.Solve();

            if (grid == null)
            {
                return null;
            }
            if (grid.FindAllEmptySquares().Count == 0)
            {
                return grid;
            }

            var emptySquareToFill = grid.FindAllEmptySquares()[0];
            var validOptions = DetermineValidOptionsForSquare(emptySquareToFill);

            var savedSudokuArray = new int[9, 9];
            Array.Copy(grid.Grid, savedSudokuArray, 81);
            SudokuGrid savedGrid = new SudokuGrid(savedSudokuArray);

            for (int i = 0; i < validOptions.Count; i++)
            {
                if (grid.FindAllEmptySquares().Count != 0)
                {
                    grid.FillInSquare(emptySquareToFill, validOptions[i]);
                }
                SudokuGrid trialSmallerGrid = SolveBetter(grid);
                if (trialSmallerGrid != null)
                {
                    grid = trialSmallerGrid;                    
                }
                else
                {
                    grid = savedGrid;
                }
            }

            return grid;
        }
    }
}
