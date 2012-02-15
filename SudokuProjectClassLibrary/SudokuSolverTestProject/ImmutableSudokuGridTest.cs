using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using SudokuProjectClassLibrary;

namespace SudokuSolverTestProject
{
    [TestFixture]
    public class ImmutableSudokuGridTest
    {
        private ImmutableSudokuGrid sudokuGrid;

        [TestFixtureSetUp]
        public void InitializeTest()
        {
            sudokuGrid = new ImmutableSudokuGrid(@"..\..\..\VeryHardSudoku.txt");
        }

        [Test]
        public void AddExtraSquareWithDifferentValueFromOriginal_CheckDifferentArrayReturned()
        {
            ImmutableSudokuGrid newGrid = FillInSquareWithValue(1);
            Assert.AreNotSame(newGrid.Elements, sudokuGrid.Elements);
            Assert.AreEqual(sudokuGrid.Elements[0, 1], -1);
            Assert.AreEqual(newGrid.Elements[0, 0], 1);
        }

        [Test]
        public void AddExtraSquareWhichIsSameAsOldSquare_CheckDifferentArrayReturned()
        {
            ImmutableSudokuGrid newGrid = FillInSquareWithValue(-1);
            Assert.AreNotSame(newGrid.Elements, sudokuGrid.Elements);
            Assert.AreEqual(sudokuGrid.Elements[0, 1], -1);
            Assert.AreEqual(newGrid.Elements[0, 0], -1);
        }

        [Test]
        public void MakeMutableCopy_ReturnsSudokuGridWithAllSameElementsAsImmutableVersion()
        {
            SudokuGrid mutableCopy = sudokuGrid.MakeMutableCopy();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Assert.AreEqual(sudokuGrid.Elements[i, j], mutableCopy.Elements[i, j]);
                }
            }
        }

        private ImmutableSudokuGrid FillInSquareWithValue(int value)
        {
            SquareCoordinate newSquare = new SquareCoordinate(0, 0);
            ImmutableSudokuGrid newGrid = sudokuGrid.WithExtraSquare(newSquare, value);
            return newGrid;
        }
    }
}
