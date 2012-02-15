using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    public class HarderSudokuSolver
    {
        public ImmutableSudokuGrid Solve(ImmutableSudokuGrid grid)
        {
            SudokuGrid mutableGrid = grid.MakeMutableCopy();

            SudokuSolver simpleSolver = new SudokuSolver();            
            bool isGridValid = simpleSolver.TryAndSolveOnce(mutableGrid);

            grid = new ImmutableSudokuGrid(grid.Elements);

            if (!isGridValid)
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
                ImmutableSudokuGrid solvedGrid = Solve(grid.WithExtraSquare(emptySquareToFill, validOptions[i]));

                if(solvedGrid != null)                
                {
                    return solvedGrid;
                }
            }

            return null;
        }
    }
}
