using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using SudokuProjectClassLibrary;


namespace SudokuSolverTestProject
{
    [TestFixture]
    public class SudokuSolverTest
    {
        SudokuSolver sudokuSolver;

        [TestFixtureSetUp]
        public void InitializeTests()
        {
            sudokuSolver = new SudokuSolver();
        }

        [Test]
        public void DetermineValidEntriesWorksCorrectly()
        {
            var grid = new ImmutableSudokuGrid(CreateNewMostlyEmptyArray());
            var validOptionsFor00 = sudokuSolver.DetermineValidOptionsForSquare(new SquareCoordinate(0, 0), grid);
            var validOptionsFor14 = sudokuSolver.DetermineValidOptionsForSquare(new SquareCoordinate(1, 4), grid);

            Assert.AreEqual(validOptionsFor00.Count, 6);
            for (int i = 4; i <= 9; i++)
            {
                Assert.IsTrue(validOptionsFor00.Contains(i));
            }

            Assert.AreEqual(validOptionsFor14.Count, 7);
            for (int i = 1; i <= 9; i++)
            {
                if (i != 3 && i != 4)
                {
                    Assert.IsTrue(validOptionsFor14.Contains(i));
                }
            }
        }

        // Creates an empty array with three squares filled in which will make the 
        // values 1,2,3 invalid for square (0,0) and values 3,4 invalid for square (1,4)
        private int[,] CreateNewMostlyEmptyArray()
        {
            int[,] array = new int[9, 9];
            array[0, 1] = 1;
            array[8, 0] = 2;
            array[1, 2] = 3;
            array[2, 3] = 4;

            return array;
        }
    }
}
