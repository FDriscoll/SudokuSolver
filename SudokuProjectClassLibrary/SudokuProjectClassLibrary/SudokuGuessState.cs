using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    public class SudokuGuessState
    {
        private SudokuGrid grid;

        public SudokuGuessState(SudokuGrid grid)
        {
            this.grid = grid;
            ValidOptionsForSquare = new List<int>();
        }

        public SudokuGrid Grid
        {
            get
            { return grid;}
            set
            { value = grid; }
        }
        public List<int> ValidOptionsForSquare { get; set; }
        public SquareCoordinate GuessingSquare { get; set; }

    }
}
