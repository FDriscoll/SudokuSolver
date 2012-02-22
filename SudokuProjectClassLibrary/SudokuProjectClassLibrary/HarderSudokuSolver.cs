using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    public class HarderSudokuSolver
    {
        public ImmutableSudokuGrid Solve(ImmutableSudokuGrid grid, bool isRandomSolve = false, int disallowedValue = 0, int disallowedRow = -1, int disallowedColumn = -1)
        {
            return Solve(grid, new SudokuSolver(), isRandomSolve, disallowedValue, disallowedRow, disallowedColumn);
        }

        private ImmutableSudokuGrid Solve(ImmutableSudokuGrid grid, SudokuSolver simpleSolver, bool isRandomSolve, int disallowedValue, int disallowedRow, int disallowedColumn)
        {
            SudokuGrid mutableGrid = grid.MakeMutableCopy();

            bool isGridValid = simpleSolver.TryAndSolveOnce(mutableGrid);

            grid = new ImmutableSudokuGrid(mutableGrid.Elements);

            if (!isGridValid)
            {
                return null;
            }

            var emptySquareList = grid.FindAllEmptySquares();

            if (emptySquareList.Count == 0)
            {
                return grid;
            }

            SquareCoordinate emptySquareToFill = emptySquareList[0];

            if (isRandomSolve)
            {
                var randomEmptySquare = new Random().Next(emptySquareList.Count - 1);
                emptySquareToFill = emptySquareList[randomEmptySquare];
            }

            var validOptions = simpleSolver.DetermineValidOptionsForSquare(emptySquareToFill, grid);
            if (emptySquareToFill.Row == disallowedRow && emptySquareToFill.Column == disallowedColumn && disallowedValue != 0)
            {
                validOptions.Remove(disallowedValue);
            }

            for (int i = 0; i < validOptions.Count; i++)
            {
                ImmutableSudokuGrid solvedGrid = Solve(grid.WithExtraSquare(emptySquareToFill, validOptions[i]), isRandomSolve);

                if (solvedGrid != null)
                {
                    return solvedGrid;
                }
            }

            return null;
        }

        public bool CanBeSolvedWithDifferentValueInSquare(ImmutableSudokuGrid grid, SquareCoordinate square, int disallowedValue)
        {
            ImmutableSudokuGrid differentValuedGrid = Solve(grid, disallowedValue: disallowedValue, disallowedRow: square.Row, disallowedColumn : square.Column);
            return differentValuedGrid == null ? false : true;
        }
    }
}
