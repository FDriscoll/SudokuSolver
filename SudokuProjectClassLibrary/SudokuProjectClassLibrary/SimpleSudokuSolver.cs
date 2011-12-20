using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    // The Simple Sudoku Solver can solve Sudokus where at every stage there is at least one empty square for which there is only one options
    // So all the eight other values already occur in the same row/column/box as that square.
    public class SimpleSudokuSolver
    {
        private SudokuGrid grid;
        private SquareCoordinate currentSquareCoordinate = new SquareCoordinate(0,0);
        private SquareCoordinate lastFilledSquare = new SquareCoordinate(0, 0);

        public SimpleSudokuSolver(SudokuGrid grid)
        {
            this.grid = grid;
        }

        // Solve searches for the next empty square in the grid and finds valid options for that square.
        // If there's only one valid option it fills it in.
        // Else it looks for the next empty square.
        // Terminates once grid is solved or once it cannot fill in any more squares.
        public void Solve()
        {
            while (true)
            {
                currentSquareCoordinate = FindNextEmptySquare();

                if (currentSquareCoordinate.Row == -1)
                {
                    grid.PrintGrid();
                    Console.WriteLine("Solved!");
                    break;
                }

                if (currentSquareCoordinate.Row == -2)
                {
                    Console.WriteLine("This Sudoku's too hard!");
                    break;
                }

                List<int> validOptionsForSquare = DetermineValidOptionsForSquare(currentSquareCoordinate);
                if (validOptionsForSquare.Count == 1)
                {
                    grid.FillInSquare(currentSquareCoordinate, validOptionsForSquare[0]);
                    lastFilledSquare = currentSquareCoordinate;
                }
            }
        }

        // Searches Grid for an empty square and returns coordinates of that square.  
        // Begins searching from the next empty square to the one which it's just looked at.
        // An empty square is represented by -1.
        private SquareCoordinate FindNextEmptySquare()
        {
            SquareCoordinate coordinateToStartFrom = currentSquareCoordinate.nextSquareCoordinate();

            // First searches along the rest of the current row.
            for (int j = coordinateToStartFrom.Column; j < 9; j++)
            {
                if (grid.Grid[coordinateToStartFrom.Row, j] == -1)
                {
                    return new SquareCoordinate(coordinateToStartFrom.Row, j);
                }

                // If unsolvable returns square with position (-2,-2)
                if (IsUnsolvableHaveCheckedWholeSudoku(coordinateToStartFrom.Row, j))
                {
                    return new SquareCoordinate(-2, -2);
                }
            }

            // Then searches through remaining rows.
            for (int i = (coordinateToStartFrom.Row + 1) % 9; ; i = (i+1)%9)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (grid.Grid[i, j] == -1)
                    {
                        return new SquareCoordinate(i, j);
                    }

                    // If search checks the whole Sudoku and doesn't find an empty square then return a "dummy" square coordinate - the Sudoku is complete.
                    if (coordinateToStartFrom.previousSquareCoordinate().Row == i && coordinateToStartFrom.previousSquareCoordinate().Column == j)
                    {
                        return new SquareCoordinate(-1,-1);
                    }

                    // If unsolvable returns square with coordinate (-2,-2)
                    if (IsUnsolvableHaveCheckedWholeSudoku(i,j))
                    {
                        return new SquareCoordinate(-2, -2);
                    }
                }
            }
        }

        // Checks to see if the whole Sudoku has been looped through without filling in a square.
        private bool IsUnsolvableHaveCheckedWholeSudoku(int Row, int Column)
        {
            return (lastFilledSquare.Row == Row && lastFilledSquare.Column == Column);
        }

        // Method establishes which values can fill an empty square by eliminating any values already occurring in row/column/box.
        private List<int> DetermineValidOptionsForSquare(SquareCoordinate squareCoordinate)
        {
            IEnumerable<int> validSquareOptions = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            IEnumerable<int> numbersAlreadyInRow = FindNumbersAlreadyInRow(squareCoordinate.Row);
            validSquareOptions = validSquareOptions.Except(numbersAlreadyInRow);
            IEnumerable<int> numbersAlreadyInColumn = FindNumbersAlreadyInColumn(squareCoordinate.Column);
            validSquareOptions = validSquareOptions.Except(numbersAlreadyInColumn);
            IEnumerable<int> numbersAlreadyInBox = FindNumbersAlreadyInBox(squareCoordinate);
            validSquareOptions = validSquareOptions.Except(numbersAlreadyInBox);

            return validSquareOptions.ToList<int>();
        }

        private List<int> FindNumbersAlreadyInBox(SquareCoordinate squareCoordinate)
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

        private List<int> FindNumbersAlreadyInColumn(int columnNumber)
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

        private List<int> FindNumbersAlreadyInRow(int rowNumber)
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
