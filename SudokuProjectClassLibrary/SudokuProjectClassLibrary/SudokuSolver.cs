using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    public class SudokuSolver
    {
        public SudokuGrid Solve(SudokuGrid inputGrid)
        {
            bool returnFromOneSolve = TryAndSolveOnce(inputGrid);

            if (!returnFromOneSolve)
            {
                return null;
            }

            if (inputGrid.FindAllEmptySquares().Count != 0)
            {
                Console.WriteLine("Solving failed at this step");
            }

            return inputGrid;
        }
                
        public bool TryAndSolveOnce(SudokuGrid inputGrid)
        {          
            while (inputGrid.FindAllEmptySquares().Count != 0)
            {
                bool hasASquareBeenFilled = false;
                foreach (var emptySquare in inputGrid.FindAllEmptySquares())
                {
                    List<int> validOptionsForSquare = DetermineValidOptionsForSquare(emptySquare, inputGrid);

                    if (validOptionsForSquare.Count == 0)
                    {
                        return false;
                    }
                    if (validOptionsForSquare.Count == 1)
                    {
                        inputGrid.FillInSquare(emptySquare, validOptionsForSquare[0]);
                        hasASquareBeenFilled = true;
                    }
                }

                if (!hasASquareBeenFilled)
                {
                    return true;
                }
            }

            return true;
        }


        // Method establishes which values can fill an empty square by eliminating any values already occurring in row/column/box.
        public List<int> DetermineValidOptionsForSquare(SquareCoordinate squareCoordinate, IImmutableSudokuGrid grid)
        {
            HashSet<int> validSquareOptions = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            HashSet<int> invalidSquareOptions = new HashSet<int>();
            for (int offset = 1; offset < 9; offset++)
            {
                invalidSquareOptions.Add(grid.Elements[(squareCoordinate.Row + offset) % 9, squareCoordinate.Column]);
                invalidSquareOptions.Add(grid.Elements[(squareCoordinate.Row), (squareCoordinate.Column + offset) % 9]);
                invalidSquareOptions.Add(grid.Elements[(3 * (squareCoordinate.Row / 3)) + offset % 3, (3 * (squareCoordinate.Column / 3)) + (offset - offset % 3) / 3]);
            }

            return validSquareOptions.Except(invalidSquareOptions).ToList<int>();
        }
    }
}
