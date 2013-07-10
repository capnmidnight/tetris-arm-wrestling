using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PuzzleFramework;
using Puzzles;

namespace PuzzleTest
{
    [TestFixture]
    public class TetrisGame__tests
    {
        [Test]
        public void TetrisTypeRowClear()
        {
            int[,] before = new int[,] {
            {1, 2, 3, 4},
            {5, -1, -1, 6},
            {7, 8, 9, 10},
            {11, 12, 13, 14}};
            int[,] after = new int[,] {
                {-1, -1, -1, -1},
            {1, 2, 3, 4},
            {5, -1, -1, 6},
            {11, 12, 13, 14}};

            Puzzle p = new Puzzle(after);
            TetrisGame q = new TetrisGame(before);
            q.TetrisClearRow(2);

            Assert.AreEqual(p, q);
        }


        [Test]
        public void TetrisTypeRowClearBad1()
        {
            int[,] before = new int[,] {
            {1, 2, 3, 4},
            {5, -1, -1, 6},
            {7, 8, 9, 10},
            {11, 12, 13, 14}};

            Puzzle p = new Puzzle(before);
            TetrisGame q = new TetrisGame(before);
            q.TetrisClearRow(-1);

            Assert.AreEqual(p, q);
        }

        [Test]
        public void TetrisTypeRowClearBad2()
        {
            int[,] before = new int[,] {
            {1, 2, 3, 4},
            {5, -1, -1, 6},
            {7, 8, 9, 10},
            {11, 12, 13, 14}};

            Puzzle p = new Puzzle(before);
            TetrisGame q = new TetrisGame(before);
            q.TetrisClearRow(q.Height);

            Assert.AreEqual(p, q);
        }
    }
}
