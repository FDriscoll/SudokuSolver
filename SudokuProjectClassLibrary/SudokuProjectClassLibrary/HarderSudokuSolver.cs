using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    public class HarderSudokuSolver : SudokuSolver
    {
        private Stack<SudokuGuessState> stackSudokuGuessPoints;

        public HarderSudokuSolver(SudokuGrid grid) : base(grid)          
        {
            stackSudokuGuessPoints = new Stack<SudokuGuessState>();
        }

        public void Solve()
        {
            while (Grid.EmptySquareList.Count != 0)
            {
                Solve(GuessASquare, BackTrack);
            }
        }

        private void GuessASquare()
        {
            SudokuGuessState currentState = new SudokuGuessState(new SudokuGrid(Grid.Grid));
            List<SquareCoordinate> emptySquareList = Grid.EmptySquareList;
            currentState.GuessingSquare = emptySquareList[0];
            foreach (SquareCoordinate square in emptySquareList)
            {
                if (DetermineValidOptionsForSquare(square).Count < DetermineValidOptionsForSquare(currentState.GuessingSquare).Count)
                {
                    currentState.GuessingSquare = square;
                }
            }

            currentState.ValidOptionsForSquare = DetermineValidOptionsForSquare(currentState.GuessingSquare);
            stackSudokuGuessPoints.Push(currentState);

            Grid.FillInSquare(currentState.GuessingSquare, currentState.ValidOptionsForSquare[0]);
        }

        private void BackTrack()
        {
            stackSudokuGuessPoints.Peek().ValidOptionsForSquare.RemoveAt(0);

            while(stackSudokuGuessPoints.Peek().ValidOptionsForSquare.Count == 0)
            {
                stackSudokuGuessPoints.Pop();
                stackSudokuGuessPoints.Peek().ValidOptionsForSquare.RemoveAt(0);
            }

            SudokuGuessState savePoint = stackSudokuGuessPoints.Peek();
            savePoint.Grid.FillInSquare(savePoint.GuessingSquare,savePoint.ValidOptionsForSquare[0]);
            Grid = savePoint.Grid;
        }
    }
}
