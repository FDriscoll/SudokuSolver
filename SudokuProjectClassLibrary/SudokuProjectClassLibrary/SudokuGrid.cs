using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SudokuProjectClassLibrary
{
    public class SudokuGrid : ImmutableSudokuGrid, ISudokuGrid
    {
        public SudokuGrid(int[,] grid)
            : base(grid)
        {
        }

        public new int[,] Elements
        { 
            get { return base.Elements; } 
            set { value = base.Elements; }
        }

        public void FillInSquare(SquareCoordinate squareCoordinate, int value)
        {
            Elements[squareCoordinate.Row, squareCoordinate.Column] = value;
        }
    }
}
