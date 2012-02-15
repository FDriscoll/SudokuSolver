using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    public interface IImmutableSudokuGrid
    {
        int[,] Elements { get; }
        ImmutableSudokuGrid WithExtraSquare(SquareCoordinate coord, int value);
    }
}
