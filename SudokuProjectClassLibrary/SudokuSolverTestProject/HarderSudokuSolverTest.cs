using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using SudokuProjectClassLibrary;

namespace SudokuSolverTestProject
{
    [TestFixture]
    public class HarderSudokuSolverTest
    {
        HarderSudokuSolver sudokuSolver;

        [TestFixtureSetUp]
        public void InitializeTests()
        {
            sudokuSolver = new HarderSudokuSolver();
        }

        [Test]
        public void TryToSolveInvalidSudoku_CheckSolveReturnsNull()
        {
            ImmutableSudokuGrid grid = new ImmutableSudokuGrid(CreateInvalidSudokuArray());
            var returnFromSolve = sudokuSolver.Solve(grid);
            Assert.IsNull(returnFromSolve);
        }

        [Test]
        public void TryToSolveValidSudoku_CheckSolveReturnsCompletedGrid()
        {
            ImmutableSudokuGrid grid = new ImmutableSudokuGrid(CreateSudokuArray());
            var returnFromSolve = sudokuSolver.Solve(grid);
            var numberEmptySquares = returnFromSolve.FindAllEmptySquares().Count;

            Assert.IsNotNull(grid);
            Assert.AreEqual(numberEmptySquares, 0);
        }

        private int[,] CreateInvalidSudokuArray()
        {
            int[,] array = CreateSudokuArray();
            for (int i = 1; i < 9; i++)
            {
                array[0, i] = i + 1;
            }
            array[1, 0] = 1;

            return array;
        }

        private static int[,] CreateSudokuArray()
        {
            int[,] array = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    array[i, j] = -1;
                }
            }
            return array;
        }
    }
}
