using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuProjectClassLibrary
{
    public class SquareCoordinate
    {
        public SquareCoordinate(int i, int j)
        {
            Row = i;
            Column = j;
        }

        public int Row { get; private set; }
        public int Column { get; private set; }

        public SquareCoordinate nextSquareCoordinate()
        {
            if (Row == 8 && Column == 8) { return new SquareCoordinate(0, 0); }
            else if (Column == 8) { return new SquareCoordinate(Row + 1, 0); }
            else { return new SquareCoordinate(Row, Column + 1); }
        }

        public SquareCoordinate previousSquareCoordinate()
        {
            if (Row == 0 && Column == 0) { return new SquareCoordinate(8, 8); }
            else if (Column == 0) { return new SquareCoordinate(Row - 1, 8); }
            else { return new SquareCoordinate(Row, Column - 1); }
        }
    }
}
