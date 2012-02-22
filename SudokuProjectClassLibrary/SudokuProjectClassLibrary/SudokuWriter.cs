using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    public class SudokuWriter
    {
        public ImmutableSudokuGrid CreateFilledGrid()
        {
            return CreateFilledGrid(new HarderSudokuSolver());
        }

        private ImmutableSudokuGrid CreateFilledGrid(HarderSudokuSolver solver)
        {
            ImmutableSudokuGrid sudokuGrid = new ImmutableSudokuGrid();
            return solver.Solve(sudokuGrid, true);             
        }

        public ImmutableSudokuGrid EmptyGridForHardSolve(ImmutableSudokuGrid grid)
        {
            return EmptyGridForHardSolve(grid, new HarderSudokuSolver());
        }

        private ImmutableSudokuGrid EmptyGridForHardSolve(ImmutableSudokuGrid inputGrid, HarderSudokuSolver solver)
        {
            ImmutableSudokuGrid grid = EmptyGridForSimpleSolve(inputGrid);
            var listOfFilledSquares = grid.FindAllFilledSquares();
            for (int i = 0; i < listOfFilledSquares.Count; i++)
            {
                var square = listOfFilledSquares[i];
                bool isNotUniqueValue = solver.CanBeSolvedWithDifferentValueInSquare(grid, square, grid.Elements[square.Row, square.Column]);

                if (!isNotUniqueValue)
                {
                    grid = grid.WithoutSquare(square);
                    return EmptyGridForHardSolve(grid);
                }
            }

            return grid;

        }


        public ImmutableSudokuGrid EmptyGridForSimpleSolve(ImmutableSudokuGrid grid)
        {
            return EmptyGridForSimpleSolve(grid, new SudokuSolver());
        }

        private ImmutableSudokuGrid EmptyGridForSimpleSolve(ImmutableSudokuGrid inputGrid, SudokuSolver solver)
        {
            ImmutableSudokuGrid grid = inputGrid;

            while (true)
            {
                bool hasSquareBeenRemoved = false;
                List<SquareCoordinate> filledSquareList = grid.FindAllFilledSquares();

                foreach (SquareCoordinate square in filledSquareList)
                {
                    ImmutableSudokuGrid gridWithoutSquare = grid.WithoutSquare(square);
                    int numberOfOptions = solver.DetermineValidOptionsForSquare(square, gridWithoutSquare).Count;
                    if (numberOfOptions == 1)
                    {
                        grid = gridWithoutSquare;
                        hasSquareBeenRemoved = true;
                    }
                }

                if (!hasSquareBeenRemoved)
                {
                    return grid;
                }
            }

        }
    }
}
