using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    public class SudokuSolver
    {
        public SudokuSolver(SudokuGrid grid)
        {
            this.Grid = grid;
        }

        protected SudokuGrid Grid { get; set; }
        
        // Solve searches for the next empty square in the grid and finds valid options for that square.
        // If there's only one valid option it fills it in.
        // Else it looks for the next empty square.
        // Terminates once grid is solved or once it cannot fill in any more squares.
        public void Solve(Action CallbackWhenStuck, Action CallbackWhenContradiction)
        {
            while (Grid.EmptySquareList.Count != 0)
            {
                bool hasASquareBeenFilled = false;
                bool isThereAContradiction = false;
                foreach (var emptySquare in Grid.EmptySquareList)
                {
                    List<int> validOptionsForSquare = DetermineValidOptionsForSquare(emptySquare);
                    if (validOptionsForSquare.Count == 0)
                    {
                        isThereAContradiction = true;
                        break;
                    }
                    if (validOptionsForSquare.Count == 1)
                    {
                        Grid.FillInSquare(emptySquare, validOptionsForSquare[0]);
                        hasASquareBeenFilled = true;
                    }
                }
                if (isThereAContradiction)
                {
                    CallbackWhenContradiction();
                }
                else if (!hasASquareBeenFilled)
                {
                    CallbackWhenStuck();
                }
            }

            Grid.PrintGrid();
            Console.WriteLine("Solved!");
            return;
        }


        // Method establishes which values can fill an empty square by eliminating any values already occurring in row/column/box.
        protected List<int> DetermineValidOptionsForSquare(SquareCoordinate squareCoordinate)
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
                    int squareEntry = Grid.Grid[i, j];
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
                int squareEntry = Grid.Grid[i, columnNumber];
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
                int squareEntry = Grid.Grid[rowNumber, i];
                if (squareEntry != -1)
                {
                    listOfNumbersInRow.Add(squareEntry);
                }
            }
            return listOfNumbersInRow;
        }
    }
}
