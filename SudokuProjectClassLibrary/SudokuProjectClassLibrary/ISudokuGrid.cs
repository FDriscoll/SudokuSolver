using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    interface ISudokuGrid : IImmutableSudokuGrid
    {
        new int[,] Elements { get; set; }
    }
}
