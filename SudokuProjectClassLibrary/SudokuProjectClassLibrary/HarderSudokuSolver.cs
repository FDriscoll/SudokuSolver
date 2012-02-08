using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    public class HarderSudokuSolver : ISudokuSolver
    {
        public SudokuGrid Solve(SudokuGrid inputGrid)
        {
            SudokuSolver simpleSolver = new SudokuSolver();            
            SudokuGrid grid = simpleSolver.TryAndSolveOnce(inputGrid);

            if (grid == null)
            {
                return null;
            }
            if (grid.FindAllEmptySquares().Count == 0)
            {
                return grid;
            }

            var emptySquareToFill = grid.FindAllEmptySquares()[0];
            var validOptions = simpleSolver.DetermineValidOptionsForSquare(emptySquareToFill, grid);
            
            for (int i = 0; i < validOptions.Count; i++)
            {
                grid.FillInSquare(emptySquareToFill, validOptions[i]);

                SudokuGrid trialSmallerGrid = Solve(grid);

                if(trialSmallerGrid == null)                
                {
                    Array.Copy(grid.ImmutableGrid, grid.Grid, 81);
                }
                else
                {
                    return trialSmallerGrid;                    
                }
            }

            return null;
        }
    }
}
