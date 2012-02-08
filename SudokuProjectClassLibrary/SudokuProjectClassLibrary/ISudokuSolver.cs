using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    public interface ISudokuSolver
    {
        SudokuGrid Solve(SudokuGrid grid);
    }
}
