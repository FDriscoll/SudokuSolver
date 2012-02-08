using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    public class SudokuSolver : ISudokuSolver
    {
        public SudokuGrid Solve(SudokuGrid inputGrid)
        {
            SudokuGrid grid = TryAndSolveOnce(inputGrid);
            if (grid.FindAllEmptySquares().Count != 0)
            {
                Console.WriteLine("Solving failed at this step");
            }

            return grid;
        }

        // Solve searches for the next empty square in the grid and finds valid options for that square.
        // If there's only one valid option it fills it in.
        // Else it looks for the next empty square.
        // Terminates once grid is solved or once it cannot fill in any more squares.
        public SudokuGrid TryAndSolveOnce(SudokuGrid inputGrid)
        { 
            if (inputGrid == null)
            {
                return null;
            }

            SudokuGrid grid = inputGrid;

            while (grid.FindAllEmptySquares().Count != 0)
            {
                bool hasASquareBeenFilled = false;
                foreach (var emptySquare in grid.FindAllEmptySquares())
                {
                    List<int> validOptionsForSquare = DetermineValidOptionsForSquare(emptySquare, grid);

                    if (validOptionsForSquare.Count == 0)
                    {
                        return null;
                    }
                    if (validOptionsForSquare.Count == 1)
                    {
                        grid.FillInSquare(emptySquare, validOptionsForSquare[0]);
                        hasASquareBeenFilled = true;
                    }
                }

                if (!hasASquareBeenFilled)
                {
                    return new SudokuGrid(grid.Grid);
                }
            }

            return new SudokuGrid(grid.Grid);
        }


        // Method establishes which values can fill an empty square by eliminating any values already occurring in row/column/box.
        public List<int> DetermineValidOptionsForSquare(SquareCoordinate squareCoordinate, SudokuGrid grid)
        {
            IEnumerable<int> validSquareOptions = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            IEnumerable<int> numbersAlreadyInRow = FindNumbersAlreadyInRow(squareCoordinate.Row, grid);
            validSquareOptions = validSquareOptions.Except(numbersAlreadyInRow);
            IEnumerable<int> numbersAlreadyInColumn = FindNumbersAlreadyInColumn(squareCoordinate.Column, grid);
            validSquareOptions = validSquareOptions.Except(numbersAlreadyInColumn);
            IEnumerable<int> numbersAlreadyInBox = FindNumbersAlreadyInBox(squareCoordinate, grid);
            validSquareOptions = validSquareOptions.Except(numbersAlreadyInBox);

            return validSquareOptions.ToList<int>();
        }

        private List<int> FindNumbersAlreadyInBox(SquareCoordinate squareCoordinate, SudokuGrid grid)
        {
            List<int> listOfNumbersInBox = new List<int>();

            List<int> rowTriple;
            List<int> columnTriple;

            // Establishes which of the nine boxes the square is in.
            rowTriple = SetTriple(squareCoordinate.Row);
            columnTriple = SetTriple(squareCoordinate.Column);

            foreach (int i in rowTriple)
            {
                foreach (int j in columnTriple)
                {
                    int squareEntry = grid.Grid[i, j];
                    if (squareEntry != -1)
                    {
                        listOfNumbersInBox.Add(squareEntry);
                    }
                }
            }

            return listOfNumbersInBox;
        }

        // Method groups rows/columns into trios to check the squares which are in the same box as the square being searched for.
        private List<int> SetTriple(int numberInTriple)
        {
            List<int> triple;

            List<int> firstTriple = new List<int> { 0, 1, 2 };
            List<int> secondTriple = new List<int> { 3, 4, 5 };
            List<int> thirdTriple = new List<int> { 6, 7, 8 };

            if (firstTriple.Contains(numberInTriple)) { triple = firstTriple; }
            else if (secondTriple.Contains(numberInTriple)) { triple = secondTriple; }
            else { triple = thirdTriple; }
            return triple;
        }

        private List<int> FindNumbersAlreadyInColumn(int columnNumber, SudokuGrid grid)
        {
            List<int> listOfNumbersInColumn = new List<int>();

            for (int i = 0; i < 9; i++)
            {
                int squareEntry = grid.Grid[i, columnNumber];
                if (squareEntry != -1)
                {
                    listOfNumbersInColumn.Add(squareEntry);
                }
            }
            return listOfNumbersInColumn;
        }

        private List<int> FindNumbersAlreadyInRow(int rowNumber, SudokuGrid grid)
        {
            List<int> listOfNumbersInRow = new List<int>();

            for (int i = 0; i < 9; i++)
            {
                int squareEntry = grid.Grid[rowNumber, i];
                if (squareEntry != -1)
                {
                    listOfNumbersInRow.Add(squareEntry);
                }
            }
            return listOfNumbersInRow;
        }
    }
}
