using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PuzzleFramework;

namespace PuzzleTest
{
    [TestFixture]
    public class Puzzle__tests
    {
        readonly int[,] testGrid;
        public Puzzle__tests()
        {
            testGrid = new int[,] { 
{ 1, 2, 3 }, 
{ 4, 5, 6 }, 
{ 7, 8, 9 }, 
{ 10, 11, 12 },
{ 13, 14, 15 } };
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void DimensionsBad1()
        {
            Puzzle p = new Puzzle(0, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void DimensionsBad2()
        {
            Puzzle p = new Puzzle(0, 2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void DimensionsBad3()
        {
            Puzzle p = new Puzzle(3, 0);
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void DimensionsBad4()
        {
            Puzzle p = new Puzzle(-4, 0);
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void DimensionsBad5()
        {
            Puzzle p = new Puzzle(0, -5);
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void DimensionsBad6()
        {
            Puzzle p = new Puzzle(-6, 2);
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void DimensionsBad7()
        {
            Puzzle p = new Puzzle(2, -7);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void DimensionsBad8()
        {
            Puzzle p = new Puzzle(-8, -8);
        }
        [Test]
        public void CheckWidthExplicit()
        {
            Puzzle puz = new Puzzle(3, 5);
            Assert.AreEqual(3, puz.Width);
        }
        [Test]
        public void CheckHeightExplicit()
        {
            Puzzle puz = new Puzzle(3, 5);
            Assert.AreEqual(5, puz.Height);
        }
        [Test]
        public void InitToNegOne()
        {
            Puzzle puz = new Puzzle(3, 5);
            for (int y = 0; y < puz.Height; ++y)
                for (int x = 0; x < puz.Width; ++x)
                    Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void InitGridBad()
        {
            Puzzle p = new Puzzle(null);
        }
        [Test]
        public void CheckWidthImplicit()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.AreEqual(3, puz.Width);
        }
        [Test]
        public void CheckHeightImplicit()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.AreEqual(5, puz.Height);
        }

        [Test]
        public void InitToGrid()
        {
            Puzzle puz = new Puzzle(testGrid);
            for (int y = 0; y < puz.Height; ++y)
                for (int x = 0; x < puz.Width; ++x)
                    Assert.AreEqual(testGrid[y, x], puz[x, y]);
        }

        [Test]
        public void Comparable()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            int[,] grid = new int[testGrid.GetLength(0), testGrid.GetLength(1)];
            Array.Copy(testGrid, grid, testGrid.Length);
            grid[grid.GetLength(0) - 1, grid.GetLength(1) - 1] = 99;
            Puzzle r = new Puzzle(grid);

            Assert.AreEqual(q, p);
            Assert.AreNotEqual(p, r);
            Assert.AreNotEqual(q, r);
        }


        [Test]
        public void InBounds1()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.IsFalse(p.IsInBounds(-1, -1));
        }


        [Test]
        public void InBounds2()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.IsFalse(p.IsInBounds(p.Width / 2, -1));
        }
        [Test]
        public void InBounds3()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.IsFalse(p.IsInBounds(p.Width, -1));
        }
        [Test]
        public void InBounds4()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.IsFalse(p.IsInBounds(-1, p.Height / 2));
        }
        [Test]
        public void InBounds5()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.IsTrue(p.IsInBounds(p.Width / 2, p.Height / 2));
        }
        [Test]
        public void InBounds6()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.IsFalse(p.IsInBounds(p.Width, p.Height / 2));
        }
        [Test]
        public void InBounds7()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.IsFalse(p.IsInBounds(-1, p.Height));
        }
        [Test]
        public void InBounds8()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.IsFalse(p.IsInBounds(p.Width / 2, p.Height));
        }
        [Test]
        public void InBounds9()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.IsFalse(p.IsInBounds(p.Width, p.Height));
        }

        [Test]
        public void InBoundsRect1_1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-2, -3, 2, 3));
        }

        [Test]
        public void InBoundsRect2_1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-1, -3, 2, 3));
        }


        [Test]
        public void InBoundsRect3_1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(0, -3, 2, 3));
        }

        [Test]
        public void InBoundsRect4_1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width - 1, -3, 2, 3));
        }

        [Test]
        public void InBoundsRect5_1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width, -3, 2, 3));
        }

        [Test]
        public void InBoundsRect1_2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-2, -2, 2, 3));
        }

        [Test]
        public void InBoundsRect2_2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-1, -2, 2, 3));
        }


        [Test]
        public void InBoundsRect3_2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(0, -2, 2, 3));
        }

        [Test]
        public void InBoundsRect4_2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width - 1, -2, 2, 3));
        }

        [Test]
        public void InBoundsRect5_2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width, -2, 2, 3));
        }

        [Test]
        public void InBoundsRect1_3()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-2, 0, 2, 3));
        }

        [Test]
        public void InBoundsRect2_3()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-1, 0, 2, 3));
        }

        [Test]
        public void InBoundsRect3_3()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsTrue(p.IsInBounds(0, 0, 2, 3));
        }

        [Test]
        public void InBoundsRect4_3()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width - 1, 0, 2, 3));
        }

        [Test]
        public void InBoundsRect5_3()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width, 0, 2, 3));
        }

        [Test]
        public void InBoundsRect1_4()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-2, p.Height - 2, 2, 3));
        }

        [Test]
        public void InBoundsRect2_4()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-1, p.Height - 2, 2, 3));
        }

        [Test]
        public void InBoundsRect3_4()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(0, p.Height - 2, 2, 3));
        }

        [Test]
        public void InBoundsRect4_4()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width - 1, p.Height - 2, 2, 3));
        }

        [Test]
        public void InBoundsRect5_4()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width, p.Height - 2, 2, 3));
        }

        [Test]
        public void InBoundsRect1_5()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-2, p.Height, 2, 3));
        }

        [Test]
        public void InBoundsRect2_5()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-1, p.Height, 2, 3));
        }

        [Test]
        public void InBoundsRect3_5()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(0, p.Height, 2, 3));
        }

        [Test]
        public void InBoundsRect4_5()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width - 1, p.Height, 2, 3));
        }

        [Test]
        public void InBoundsRect5_5()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width, p.Height, 2, 3));
        }

        [Test]
        public void InBoundsShape1_1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-2, -3, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape2_1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-1, -3, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }


        [Test]
        public void InBoundsShape3_1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(0, -3, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape4_1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width - 1, -3, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape5_1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width, -3, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape1_2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-2, -2, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape2_2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-1, -2, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }


        [Test]
        public void InBoundsShape3_2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(0, -2, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape4_2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width - 1, -2, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape5_2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width, -2, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape1_3()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-2, 0, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape2_3()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-1, 0, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape3_3()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsTrue(p.IsInBounds(0, 0, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape4_3()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width - 1, 0, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape5_3()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width, 0, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape1_4()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-2, p.Height - 2, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape2_4()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-1, p.Height - 2, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape3_4()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(0, p.Height - 2, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape4_4()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width - 1, p.Height - 2, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape5_4()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width, p.Height - 2, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape1_5()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-2, p.Height, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape2_5()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-1, p.Height, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape3_5()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(0, p.Height, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape4_5()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width - 1, p.Height, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShape5_5()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width, p.Height, new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } }));
        }

        [Test]
        public void InBoundsShapeBad1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(1, 2, (int[,])null));
        }

        [Test]
        public void InBoundsShapeBad2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(1, 2, new int[0, 0]));
        }

        [Test]
        public void InBoundsPuzzle1_1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-2, -3, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle2_1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-1, -3, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }


        [Test]
        public void InBoundsPuzzle3_1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(0, -3, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle4_1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width - 1, -3, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle5_1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width, -3, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle1_2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-2, -2, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle2_2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-1, -2, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }


        [Test]
        public void InBoundsPuzzle3_2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(0, -2, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle4_2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width - 1, -2, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle5_2()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width, -2, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle1_3()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-2, 0, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle2_3()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-1, 0, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle3_3()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsTrue(p.IsInBounds(0, 0, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle4_3()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width - 1, 0, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle5_3()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width, 0, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle1_4()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-2, p.Height - 2, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle2_4()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-1, p.Height - 2, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle3_4()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(0, p.Height - 2, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle4_4()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width - 1, p.Height - 2, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle5_4()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width, p.Height - 2, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle1_5()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-2, p.Height, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle2_5()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(-1, p.Height, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle3_5()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(0, p.Height, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle4_5()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width - 1, p.Height, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzle5_5()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(p.Width, p.Height, new Puzzle(new int[,] { { 1, -1 }, { 2, 3 }, { 5, -1 } })));
        }

        [Test]
        public void InBoundsPuzzleBad1()
        {
            Puzzle p = new Puzzle(3, 5);
            Assert.IsFalse(p.IsInBounds(1, 1, (Puzzle)null));
        }

        [Test]
        public void IndexerGetParams1()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.AreEqual(Puzzle.EmptyTile, p[-1, -1]);
        }

        [Test]
        public void IndexerGetParams2()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.AreEqual(Puzzle.EmptyTile, p[p.Width / 2, -1]);
        }
        [Test]
        public void IndexerGetParams3()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.AreEqual(Puzzle.EmptyTile, p[p.Width, -1]);
        }
        [Test]
        public void IndexerGetParams4()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.AreEqual(Puzzle.EmptyTile, p[-1, p.Height / 2]);
        }
        [Test]
        public void IndexerGetParams5()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.AreNotEqual(Puzzle.EmptyTile, p[p.Width / 2, p.Height / 2]);
        }
        [Test]
        public void IndexerGetParams6()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.AreEqual(Puzzle.EmptyTile, p[p.Width, p.Height / 2]);
        }
        [Test]
        public void IndexerGetParams7()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.AreEqual(Puzzle.EmptyTile, p[-1, p.Height]);
        }
        [Test]
        public void IndexerGetParams8()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.AreEqual(Puzzle.EmptyTile, p[p.Width / 2, p.Height]);
        }
        [Test]
        public void IndexerGetParams9()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Assert.AreEqual(Puzzle.EmptyTile, p[p.Width, p.Height]);
        }

        [Test]
        public void IndexerSetParams1()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Puzzle q = new Puzzle(this.testGrid);
            p[-1, -1] = 99;
            Assert.AreEqual(q, p);
        }

        [Test]
        public void IndexerSetParams2()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Puzzle q = new Puzzle(this.testGrid);
            p[p.Width / 2, -1] = 99;
            Assert.AreEqual(q, p);
        }

        [Test]
        public void IndexerSetParams3()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Puzzle q = new Puzzle(this.testGrid);
            p[p.Width, -1] = 99;
            Assert.AreEqual(q, p);
        }

        [Test]
        public void IndexerSetParams4()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Puzzle q = new Puzzle(this.testGrid);
            p[-1, p.Height / 2] = 99;
            Assert.AreEqual(q, p);
        }

        [Test]
        public void IndexerSetParams5()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Puzzle q = new Puzzle(this.testGrid);
            p[p.Width / 2, p.Height / 2] = 99;
            Assert.AreNotEqual(q, p);
        }

        [Test]
        public void IndexerSetParams6()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Puzzle q = new Puzzle(this.testGrid);
            p[p.Width, p.Height / 2] = 99;
            Assert.AreEqual(q, p);
        }

        [Test]
        public void IndexerSetParams7()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Puzzle q = new Puzzle(this.testGrid);
            p[-1, p.Height] = 99;
            Assert.AreEqual(q, p);
        }

        [Test]
        public void IndexerSetParams8()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Puzzle q = new Puzzle(this.testGrid);
            p[p.Width / 2, p.Height] = 99;
            Assert.AreEqual(q, p, "8");
        }

        [Test]
        public void IndexerSetParams9()
        {
            Puzzle p = new Puzzle(this.testGrid);
            Puzzle q = new Puzzle(this.testGrid);
            p[p.Width, p.Height] = 99;
            Assert.AreEqual(q, p, "9");
        }

        [Test]
        public void ClearRow()
        {
            Puzzle puz = new Puzzle(testGrid);
            for (int y = 0; y < puz.Height; ++y)
                for (int x = 0; x < puz.Width; ++x)
                    Assert.AreNotEqual(Puzzle.EmptyTile, puz[x, y]);
            puz.Clear(Puzzle.RowOrder, 2);
            for (int y = 0; y < puz.Height; ++y)
                for (int x = 0; x < puz.Width; ++x)
                    if (y == 2)
                        Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
                    else
                        Assert.AreNotEqual(Puzzle.EmptyTile, puz[x, y]);
        }
        [Test]
        public void ClearColumn()
        {
            Puzzle puz = new Puzzle(testGrid);
            for (int y = 0; y < puz.Height; ++y)
                for (int x = 0; x < puz.Width; ++x)
                    Assert.AreNotEqual(Puzzle.EmptyTile, puz[x, y]);
            puz.Clear(Puzzle.ColumnOrder, 2);
            for (int y = 0; y < puz.Height; ++y)
                for (int x = 0; x < puz.Width; ++x)
                    if (x == 2)
                        Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
                    else
                        Assert.AreNotEqual(Puzzle.EmptyTile, puz[x, y]);
        }

        [Test]
        public void Clear()
        {
            Puzzle puz = new Puzzle(testGrid);
            for (int y = 0; y < puz.Height; ++y)
                for (int x = 0; x < puz.Width; ++x)
                    Assert.AreNotEqual(Puzzle.EmptyTile, puz[x, y]);
            puz.Clear();
            for (int y = 0; y < puz.Height; ++y)
                for (int x = 0; x < puz.Width; ++x)
                    Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
        }

        [Test]
        public void ClearPoint()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.AreNotEqual(Puzzle.EmptyTile, puz[2, 2]);
            puz.Clear(2, 2);
            Assert.AreEqual(Puzzle.EmptyTile, puz[2, 2]);
        }

        [Test]
        public void ClearPointBad1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle p2 = new Puzzle(testGrid);
            puz.Clear(-1, -1);
            Assert.AreEqual(p2, puz);
        }

        [Test]
        public void ClearPointBad2()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle p2 = new Puzzle(testGrid);
            puz.Clear(-1, 1);
            Assert.AreEqual(p2, puz);
        }

        [Test]
        public void ClearPointBad3()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle p2 = new Puzzle(testGrid);
            puz.Clear(-1, puz.Height);
            Assert.AreEqual(p2, puz);
        }

        [Test]
        public void ClearPointBad4()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle p2 = new Puzzle(testGrid);
            puz.Clear(1, -1);
            Assert.AreEqual(p2, puz);
        }

        [Test]
        public void ClearPointBad5()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle p2 = new Puzzle(testGrid);
            puz.Clear(1, puz.Height);
            Assert.AreEqual(p2, puz);
        }

        [Test]
        public void ClearPointBad6()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle p2 = new Puzzle(testGrid);
            puz.Clear(puz.Width, -1);
            Assert.AreEqual(p2, puz);
        }

        [Test]
        public void ClearPointBad7()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle p2 = new Puzzle(testGrid);
            puz.Clear(puz.Width, 1);
            Assert.AreEqual(p2, puz);
        }

        [Test]
        public void ClearPointBad8()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle p2 = new Puzzle(testGrid);
            puz.Clear(puz.Width, puz.Height);
            Assert.AreEqual(p2, puz);
        }

        [Test]
        public void ClearRect()
        {
            Puzzle puz = new Puzzle(testGrid);
            for (int y = 1; y < 3; ++y)
                for (int x = 1; x < 3; ++x)
                    Assert.AreNotEqual(Puzzle.EmptyTile, puz[x, y]);
            puz.Clear(1, 1, 2, 2);
            for (int y = 1; y < 3; ++y)
                for (int x = 1; x < 3; ++x)
                    Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
        }

        [Test]
        public void ClearRectBad1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(-2, -2, 2, 2);
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearRectBad2()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(-2, 0, 2, 2);
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearRectBad3()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(-2, puz.Height, 2, 2);
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearRectBad4()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(0, -2, 2, 2);
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearRectBad5()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(0, puz.Height, 2, 2);
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearRectBad6()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(puz.Width, -2, 2, 2);
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearRectBad7()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(puz.Width, 0, 2, 2);
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearRectBad8()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(puz.Width, puz.Height, 2, 2);
            Assert.AreEqual(quz, puz);
        }
        [Test]
        public void ClearMask()
        {
            Puzzle puz = new Puzzle(testGrid);
            int[,] mask = new int[,] { { -1, -1, -1 }, { -1, -1, 1 }, { -1, 2, 3 }, { -1, 5, 7 }, { -1, 11, -1 } };
            for (int y = 0; y < puz.Height; ++y)
                for (int x = 0; x < puz.Width; ++x)
                    Assert.AreNotEqual(Puzzle.EmptyTile, puz[x, y]);
            puz.Clear(mask);
            for (int y = 0; y < puz.Height; ++y)
                for (int x = 0; x < puz.Width; ++x)
                    if (mask[y, x] != Puzzle.EmptyTile)
                        Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
                    else
                        Assert.AreNotEqual(Puzzle.EmptyTile, puz[x, y]);
        }

        [Test]
        public void ClearMaskBad1()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);

            p.Clear(null);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void ClearMaskBad2()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);

            p.Clear(new int[0, 0]);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void ClearMaskBad3()
        {

            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);

            p.Clear(new int[1, 1] { { 1 } });
            Assert.AreEqual(q, p);
        }

        [Test]
        public void ClearShape()
        {
            Puzzle puz = new Puzzle(testGrid);
            int[,] shape = new int[,] { { 1, -1, -1 }, { 2, 3, 4 } };
            for (int y = 0; y < puz.Height; ++y)
                for (int x = 0; x < puz.Width; ++x)
                    Assert.AreNotEqual(Puzzle.EmptyTile, puz[x, y]);
            puz.Clear(0, 2, shape);
            for (int y = 0; y < puz.Height; ++y)
                for (int x = 0; x < puz.Width; ++x)
                    if (y >= 2 && y < 4 && shape[y - 2, x] != Puzzle.EmptyTile)
                        Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
                    else
                        Assert.AreNotEqual(Puzzle.EmptyTile, puz[x, y]);
        }
        [Test]
        public void ClearShapeBad1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(-2, -2, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearShapeBad2()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(-2, 0, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearShapeBad3()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(-2, puz.Height, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearShapeBad4()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(0, -2, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearShapeBad5()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(0, puz.Height, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearShapeBad6()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(puz.Width, -2, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearShapeBad7()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(puz.Width, 0, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearShapeBad8()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(puz.Width, puz.Height, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }
        [Test]
        public void ClearShapeBad9()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);

            p.Clear(0, 0, (int[,])null);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void ClearShapeBad10()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);

            p.Clear(0, 0, new int[0, 0]);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void ClearPuzzle()
        {
            int[,] before = new int[,] { { 1, 2, 3 }, { 23, 29, 31 }, { 5, 7, 11 }, { 13, 17, 19 } };
            int[,] after = new int[,] { { 1, 2, 3 }, { 23, 29, 31 }, { 5, -1, 11 }, { 13, -1, -1 } };
            int[,] shape = new int[,] { { 1, -1 }, { 2, 3 } };
            Puzzle p = new Puzzle(after);
            Puzzle q = new Puzzle(shape);
            Puzzle r = new Puzzle(before);
            r.Clear(1, 2, q);
            Assert.AreEqual(p, r);
        }
        [Test]
        public void ClearPuzzleBad1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(-2, -2, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearPuzzleBad2()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(-2, 0, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearPuzzleBad3()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(-2, puz.Height, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearPuzzleBad4()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(0, -2, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearPuzzleBad5()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(0, puz.Height, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearPuzzleBad6()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(puz.Width, -2, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearPuzzleBad7()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(puz.Width, 0, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void ClearPuzzleBad8()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Clear(puz.Width, puz.Height, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }
        [Test]
        public void ClearPuzzleBad9()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);

            p.Clear(0, 0, (Puzzle)null);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void FillGrid()
        {
            Puzzle puz = new Puzzle(4, 5);
            for (int y = 0; y < 5; ++y)
                for (int x = 0; x < 4; ++x)
                    Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
            puz.Fill(3);
            for (int y = 0; y < 5; ++y)
                for (int x = 0; x < 4; ++x)
                    Assert.AreEqual(3, puz[x, y]);
        }

        [Test]
        public void FillRow()
        {
            Puzzle puz = new Puzzle(4, 5);
            for (int y = 0; y < 5; ++y)
                for (int x = 0; x < 4; ++x)
                    Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
            puz.Fill(Puzzle.RowOrder, 2, 3);
            for (int y = 0; y < 5; ++y)
                for (int x = 0; x < 4; ++x)
                    if (y == 2)
                        Assert.AreEqual(3, puz[x, y]);
                    else
                        Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
        }

        [Test]
        public void FillRowBad1()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Fill(Puzzle.RowOrder, -1, 99);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void FillRowBad2()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Fill(Puzzle.RowOrder, p.Height, 99);
            Assert.AreEqual(q, p);
        }
        [Test]
        public void FillColumn()
        {
            Puzzle puz = new Puzzle(4, 5);
            for (int y = 0; y < 5; ++y)
                for (int x = 0; x < 4; ++x)
                    Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
            puz.Fill(Puzzle.ColumnOrder, 2, 3);
            for (int y = 0; y < 5; ++y)
                for (int x = 0; x < 4; ++x)
                    if (x == 2)
                        Assert.AreEqual(3, puz[x, y]);
                    else
                        Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
        }

        [Test]
        public void FillColumnBad1()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Fill(Puzzle.ColumnOrder, -1, 99);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void FillColumnBad2()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Fill(Puzzle.ColumnOrder, p.Width, 99);
            Assert.AreEqual(q, p);
        }
        [Test]
        public void FillRect()
        {
            Puzzle puz = new Puzzle(4, 5);
            for (int y = 0; y < 5; ++y)
                for (int x = 0; x < 4; ++x)
                    Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
            puz.Fill(1, 1, 2, 2, 3);
            for (int y = 0; y < 5; ++y)
                for (int x = 0; x < 4; ++x)
                    if (x >= 1 && x < 3 && y >= 1 && y < 3)
                        Assert.AreEqual(3, puz[x, y]);
                    else
                        Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
        }
        [Test]
        public void FillRectBad1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(-2, -2, 2, 2, 99);
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillRectBad2()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(-2, 0, 2, 2, 99);
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillRectBad3()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(-2, puz.Height, 2, 2, 99);
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillRectBad4()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(0, -2, 2, 2, 99);
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillRectBad5()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(0, puz.Height, 2, 2, 99);
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillRectBad6()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(puz.Width, -2, 2, 2, 99);
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillRectBad7()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(puz.Width, 0, 2, 2, 99);
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillRectBad8()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(puz.Width, puz.Height, 2, 2, 99);
            Assert.AreEqual(quz, puz);
        }
        [Test]
        public void FillGridRandom1()
        {
            Puzzle puz = new Puzzle(4, 5);
            for (int y = 0; y < 5; ++y)
                for (int x = 0; x < 4; ++x)
                    Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
            Random a = new Random(5), b = new Random(5);
            puz.Fill(a);
            for (int y = 0; y < 5; ++y)
                for (int x = 0; x < 4; ++x)
                    Assert.AreEqual(b.Next(), puz[x, y]);
        }

        [Test]
        public void FillGridRandomBad()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Fill(null);
            Assert.AreEqual(q, p);
        }
        [Test]
        public void FillGridRandom2()
        {
            Puzzle puz = new Puzzle(4, 5);
            for (int y = 0; y < 5; ++y)
                for (int x = 0; x < 4; ++x)
                    Assert.AreEqual(Puzzle.EmptyTile, puz[x, y]);
            puz.Fill();
            for (int y = 0; y < 5; ++y)
                for (int x = 0; x < 4; ++x)
                    Assert.AreNotEqual(Puzzle.EmptyTile, puz[x, y]);
        }

        [Test]
        public void FillShape()
        {
            Puzzle puz = new Puzzle(4, 5);
            int[,] shape = new int[,] { { 2, 3, Puzzle.EmptyTile }, { Puzzle.EmptyTile, 5, 6 } };
            puz.Fill(1, 3, shape);
            for (int y = 3; y < 5; ++y)
                for (int x = 1; x < 4; ++x)
                    Assert.AreEqual(shape[y - 3, x - 1], puz[x, y]);
        }

        [Test]
        public void FillShapeBad1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(-2, -2, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillShapeBad2()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(-2, 0, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillShapeBad3()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(-2, puz.Height, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillShapeBad4()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(0, -2, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillShapeBad5()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(0, puz.Height, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillShapeBad6()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(puz.Width, -2, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillShapeBad7()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(puz.Width, 0, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillShapeBad8()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(puz.Width, puz.Height, new int[2, 2] { { 1, -1 }, { 2, 3 } });
            Assert.AreEqual(quz, puz);
        }
        [Test]
        public void FillShapeBad9()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);

            p.Fill(0, 0, (int[,])null);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void FillShapeBad10()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);

            p.Fill(0, 0, new int[0, 0]);
            Assert.AreEqual(q, p);
        }
        [Test]
        public void FillShapeIgnoresEmpties()
        {
            Puzzle p = new Puzzle(4, 5);
            int[,] after = new int[,] { 
{ 10, 10, 10, 10 }, 
{ 10, 10, 10, 10 }, 
{ 10, 10, 10, 10 }, 
{ 10,  2,  3, 10 }, 
{ 10, 10,  5,  6 } };
            Puzzle q = new Puzzle(after);
            int[,] shape = new int[,] { { 2, 3, Puzzle.EmptyTile }, { Puzzle.EmptyTile, 5, 6 } };
            p.Fill(10);
            p.Fill(1, 3, shape);
            Assert.AreEqual(q, p);
        }


        [Test]
        public void FillPuzzle()
        {
            Puzzle puz = new Puzzle(4, 5);
            Puzzle piece = new Puzzle(new int[,] { { 2, 3, Puzzle.EmptyTile }, { Puzzle.EmptyTile, 5, 6 } });
            puz.Fill(1, 3, piece);
            for (int y = 3; y < 5; ++y)
                for (int x = 1; x < 4; ++x)
                    Assert.AreEqual(piece[x - 1, y - 3], puz[x, y]);
        }

        [Test]
        public void FillPuzzleBad1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(-2, -2, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillPuzzleBad2()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(-2, 0, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillPuzzleBad3()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(-2, puz.Height, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillPuzzleBad4()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(0, -2, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillPuzzleBad5()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(0, puz.Height, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillPuzzleBad6()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(puz.Width, -2, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillPuzzleBad7()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(puz.Width, 0, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }

        [Test]
        public void FillPuzzleBad8()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle quz = new Puzzle(testGrid);
            puz.Fill(puz.Width, puz.Height, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 3 } }));
            Assert.AreEqual(quz, puz);
        }
        [Test]
        public void FillPuzzleBad9()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);

            p.Fill(0, 0, (Puzzle)null);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void FillPuzzleIgnoresEmpties()
        {

            Puzzle p = new Puzzle(4, 5);
            int[,] after = new int[,] { 
{ 10, 10, 10, 10 }, 
{ 10, 10, 10, 10 }, 
{ 10, 10, 10, 10 }, 
{ 10,  2,  3, 10 }, 
{ 10, 10,  5,  6 } };
            Puzzle q = new Puzzle(after);
            Puzzle shape = new Puzzle(new int[,] { { 2, 3, Puzzle.EmptyTile }, { Puzzle.EmptyTile, 5, 6 } });
            p.Fill(10);
            p.Fill(1, 3, shape);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void ShiftColumnsDown()
        {
            int[,] before = new int[,] { 
{ 1, 2, 3 }, 
{ 4, Puzzle.EmptyTile, 5 }, 
{ 6, 7, 8 } };
            int[,] after = new int[,] { 
{ 1, Puzzle.EmptyTile, 3 }, 
{ 4, 2, 5 }, 
{ 6, 7, 8 } };
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            p.ShiftColumnsDown();
            Assert.AreEqual(q, p);
        }


        [Test]
        public void ShiftColumnsDown2()
        {
            int[,] before = new int[,] { 
                {1, 2, 3, 4},
                {5, 6, 7, 8},
                {9, 10, 11, 12},
                {13, 14, 15, Puzzle.EmptyTile},
                {17, 18, Puzzle.EmptyTile, 20},
                {Puzzle.EmptyTile, 22, 23, 24},
                {25, Puzzle.EmptyTile, 27, 28}};
            int[,] after = new int[,] { 
                {Puzzle.EmptyTile, Puzzle.EmptyTile, Puzzle.EmptyTile, Puzzle.EmptyTile},
                {1, 2, 3, 4},
                {5, 6, 7, 8},
                {9, 10, 11, 12},
                {13, 14, 15, 20},
                {17, 18, 23, 24},
                {25, 22, 27, 28}};
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            p.ShiftColumnsDown();
            Assert.AreEqual(q, p);
        }
        [Test]
        public void ShiftColumnsDown3()
        {
            int[,] before = new int[,] { 
{ 1, 2, 3 }, 
{ 4, Puzzle.EmptyTile, 5 }, 
{ 6, Puzzle.EmptyTile, 8 } };
            int[,] after = new int[,] { 
{ 1, Puzzle.EmptyTile, 3 }, 
{ 4, Puzzle.EmptyTile, 5 }, 
{ 6, 2, 8 } };
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            p.ShiftColumnsDown();
            Assert.AreEqual(q, p);
        }

        [Test]
        public void ShiftColumnsRight()
        {
            int[,] before = new int[,] { 
{ 1, 2, 3 }, 
{ 4, Puzzle.EmptyTile, 5 }, 
{ 6, 7, 8 } };
            int[,] after = new int[,] { 
{ 1, 2, 3 }, 
{ Puzzle.EmptyTile, 4, 5 }, 
{ 6, 7, 8 } };
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            p.ShiftColumnsRight();
            Assert.AreEqual(q, p);
        }


        [Test]
        public void ShiftColumnsRight2()
        {
            int[,] before = new int[,] { 
                {1, 2, 3, 4},
                {5, 6, 7, 8},
                {9, 10, 11, 12},
                {13, 14, 15, Puzzle.EmptyTile},
                {17, 18, Puzzle.EmptyTile, 20},
                {Puzzle.EmptyTile, 22, 23, 24},
                {25, Puzzle.EmptyTile, 27, 28}};
            int[,] after = new int[,]  { 
                {1, 2, 3, 4},
                {5, 6, 7, 8},
                {9, 10, 11, 12},
                {Puzzle.EmptyTile, 13, 14, 15},
                {Puzzle.EmptyTile, 17, 18, 20},
                {Puzzle.EmptyTile, 22, 23, 24},
                {Puzzle.EmptyTile, 25, 27, 28}};
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            p.ShiftColumnsRight();
            Assert.AreEqual(q, p);
        }
        [Test]
        public void ShiftColumnsRight3()
        {
            int[,] before = new int[,] { 
{ 1, 2, 3 }, 
{ 4, Puzzle.EmptyTile, 5 }, 
{ 6, Puzzle.EmptyTile, 8 } };
            int[,] after = new int[,]{ 
{ 1, 2, 3 }, 
{ Puzzle.EmptyTile, 4, 5 }, 
{ Puzzle.EmptyTile, 6, 8 } };
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            p.ShiftColumnsRight();
            Assert.AreEqual(q, p);
        }

        [Test]
        public void ShiftColumnsLeft()
        {
            int[,] before = new int[,] { 
{ 1, 2, 3 }, 
{ 4, Puzzle.EmptyTile, 5 }, 
{ 6, 7, 8 } };
            int[,] after = new int[,] { 
{ 1, 2, 3 }, 
{ 4, 5, Puzzle.EmptyTile }, 
{ 6, 7, 8 } };
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            p.ShiftColumnsLeft();
            Assert.AreEqual(q, p);
        }


        [Test]
        public void ShiftColumnsLeft2()
        {
            int[,] before = new int[,] { 
                {1, 2, 3, 4},
                {5, 6, 7, 8},
                {9, 10, 11, 12},
                {13, 14, 15, Puzzle.EmptyTile},
                {17, 18, Puzzle.EmptyTile, 20},
                {Puzzle.EmptyTile, 22, 23, 24},
                {25, Puzzle.EmptyTile, 27, 28}};
            int[,] after = new int[,]  { 
                {1, 2, 3, 4},
                {5, 6, 7, 8},
                {9, 10, 11, 12},
                {13, 14, 15, Puzzle.EmptyTile},
                {17, 18, 20, Puzzle.EmptyTile},
                {22, 23, 24, Puzzle.EmptyTile},
                {25, 27, 28, Puzzle.EmptyTile}};
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            p.ShiftColumnsLeft();
            Assert.AreEqual(q, p);
        }
        [Test]
        public void ShiftColumnsLeft3()
        {
            int[,] before = new int[,] { 
{ 1, 2, 3 }, 
{ 4, Puzzle.EmptyTile, 5 }, 
{ 6, Puzzle.EmptyTile, 8 } };
            int[,] after = new int[,]{ 
{ 1, 2, 3 }, 
{ 4, 5, Puzzle.EmptyTile }, 
{ 6, 8, Puzzle.EmptyTile } };
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            p.ShiftColumnsLeft();
            Assert.AreEqual(q, p);
        }
        [Test]
        public void ShiftColumnsUp()
        {
            int[,] before = new int[,] { 
{ 1, 2, 3 }, 
{ 4, Puzzle.EmptyTile, 5 }, 
{ 6, 7, 8 } };
            int[,] after = new int[,] { 
{ 1, 2, 3 }, 
{ 4, 7, 5 }, 
{ 6, Puzzle.EmptyTile, 8 } };
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            p.ShiftColumnsUp();
            Assert.AreEqual(q, p);
        }

        [Test]
        public void ShiftColumnsUp2()
        {
            int[,] before = new int[,] { 
                {1, Puzzle.EmptyTile, 3, 4},
                {5, 6, 7, Puzzle.EmptyTile},
                {9, 10, Puzzle.EmptyTile, 12},
                {Puzzle.EmptyTile, 14, 15, 16},
                {17, 18, 19, 20},
                {21, 22, 23, 24},
                {25, 26, 27, 28}};
            int[,] after = new int[,] { 
                {1, 6, 3, 4},
                {5, 10, 7, 12},
                {9, 14, 15, 16},
                {17, 18, 19, 20},
                {21, 22, 23, 24},
                {25, 26, 27, 28},
                {Puzzle.EmptyTile, Puzzle.EmptyTile, Puzzle.EmptyTile, Puzzle.EmptyTile}};
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            p.ShiftColumnsUp();
            Assert.AreEqual(q, p);
        }
        [Test]
        public void ShiftColumnsUp3()
        {
            int[,] before = new int[,] { 
{ 1, Puzzle.EmptyTile, 3 }, 
{ 4, Puzzle.EmptyTile, 5 }, 
{ 6, 2, 8 } };
            int[,] after = new int[,] { 
{ 1, 2, 3 }, 
{ 4, Puzzle.EmptyTile, 5 }, 
{ 6, Puzzle.EmptyTile, 8 } };
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            p.ShiftColumnsUp();
            Assert.AreEqual(q, p);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullGrid()
        {
            Puzzle p = new Puzzle(1, 1);
            p.Grid = null;
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyGrid()
        {
            Puzzle p = new Puzzle(1, 1);
            int[,] grid = new int[0, 0];
            p.Grid = grid;
        }

        [Test]
        public void SwapPoint()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(0, 1, 2, 3);
            Assert.AreEqual(p[0, 1], q[2, 3]);
            Assert.AreEqual(p[2, 3], q[0, 1]);
        }
        [Test]
        public void SwapPointBad1()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(-1, -1, -2, -2);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void SwapPointBad2()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(-1, -1, 2, 2);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void SwapPointBad3()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(1, 1, -2, -2);
            Assert.AreEqual(q, p);
        }


        [Test]
        public void SwapPointBad4()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(p.Width, p.Height, p.Width + 1, p.Height + 2);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void SwapPointBad5()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(p.Width, p.Height, 2, 2);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void SwapPointBad6()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(1, 1, p.Width + 1, p.Height + 2);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void SwapRows()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(Puzzle.RowOrder, 1, 3);
            for (int x = 0; x < p.Width; ++x)
            {
                Assert.AreEqual(p[x, 1], q[x, 3]);
                Assert.AreEqual(p[x, 3], q[x, 1]);
            }
        }
        [Test]
        public void SwapRowsBad1()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(Puzzle.RowOrder, -1, 3);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void SwapRowsBad2()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(Puzzle.RowOrder, -1, -3);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void SwapRowsBad3()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(Puzzle.RowOrder, 1, -3);
            Assert.AreEqual(q, p);
        }
        [Test]
        public void SwapColumns()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(Puzzle.ColumnOrder, 0, 2);
            for (int y = 0; y < p.Height; ++y)
            {
                Assert.AreEqual(p[0, y], q[2, y]);
                Assert.AreEqual(p[2, y], q[0, y]);
            }
        }
        [Test]
        public void SwapColumnsBad1()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(Puzzle.ColumnOrder, -1, 2);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void SwapColumnsBad2()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(Puzzle.ColumnOrder, -1, -2);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void SwapColumnsBad3()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(Puzzle.ColumnOrder, 1, -2);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void SwapRect()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            int sx = 0;
            int sy = 0;
            int dx = 0;
            int dy = 2;
            int w = 3;
            int h = 2;
            p.Swap(sx, sy, sx + dx, sy + dy, w, h);
            for (int y = sy; y < sy + h; y++)
                for (int x = sx; x < sx + w; x++)
                {
                    Assert.AreEqual(q[x + dx, y + dy], p[x, y], string.Format("1. At ({0}, {1}) for\n{2}", x, y, p));
                    Assert.AreEqual(q[x, y], p[x + dx, y + dy], string.Format("2. At ({0}, {1}) for\n{2}", x, y, p));
                }
        }

        [Test]
        public void SwapRect2()
        {
            int[,] before = new int[,] { 
{ 1, 2, 3, 4 }, 
{ 5, 6, 7, 8 } };
            int[,] after = new int[,] { 
{ 3, 4, 1, 2 }, 
{ 7, 8, 5, 6 } };
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            p.Swap(0, 0, 2, 0, 2, 2);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void SwapRectBad1()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(-1, -1, 1, 2, 2, 2);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void SwapRectBad2()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(0, 0, p.Width, 0, 2, 2);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void SwapRectBad3()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            p.Swap(0, 0, 1, 1, 2, 2);
            Assert.AreEqual(q, p);
        }

        [Test]
        public void RectsIntersect1()
        {
            Assert.IsTrue(Puzzle.RectsIntersect(0, 0, 1, 2, 3, 5));
        }

        [Test]
        public void RectsIntersect2()
        {
            Assert.IsTrue(Puzzle.RectsIntersect(1, 2, 0, 0, 3, 5));
        }

        [Test]
        public void RectsDontIntersect1()
        {
            Assert.IsFalse(Puzzle.RectsIntersect(1, 2, 7, 11, 3, 5));
        }

        [Test]
        public void RectsDontIntersect2()
        {
            Assert.IsFalse(Puzzle.RectsIntersect(7, 11, 1, 2, 3, 5));
        }

        [Test]
        public void RectsDontIntersect3()
        {
            Assert.IsFalse(Puzzle.RectsIntersect(1, 2, 4, 2, 3, 5));
        }
        [Test]
        public void Rotate1()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = p.Rotate(Puzzle.Clockwise);
            Assert.AreEqual(p.Width, q.Height);
            Assert.AreEqual(p.Height, q.Width);
        }

        [Test]
        public void Rotate2()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = p.Rotate(Puzzle.CounterClockwise);
            Assert.AreEqual(p.Width, q.Height);
            Assert.AreEqual(p.Height, q.Width);
        }

        [Test]
        public void Rotate3()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = p.Rotate(Puzzle.Clockwise).Rotate(Puzzle.Clockwise);
            Assert.AreEqual(p.Width, q.Width);
            Assert.AreEqual(p.Height, q.Height);
        }

        [Test]
        public void Rotate4()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = p.Rotate(Puzzle.CounterClockwise).Rotate(Puzzle.CounterClockwise);
            Assert.AreEqual(p.Width, q.Width);
            Assert.AreEqual(p.Height, q.Height);
        }

        [Test]
        public void Rotate5()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = p.Rotate(Puzzle.Clockwise).Rotate(Puzzle.Clockwise).Rotate(Puzzle.Clockwise);
            Assert.AreEqual(p.Width, q.Height);
            Assert.AreEqual(p.Height, q.Width);
        }

        [Test]
        public void Rotate6()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = p.Rotate(Puzzle.CounterClockwise).Rotate(Puzzle.CounterClockwise).Rotate(Puzzle.CounterClockwise);
            Assert.AreEqual(p.Width, q.Height);
            Assert.AreEqual(p.Height, q.Width);
        }

        [Test]
        public void Rotate7()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = p.Rotate(Puzzle.Clockwise).Rotate(Puzzle.Clockwise).Rotate(Puzzle.Clockwise).Rotate(Puzzle.Clockwise);
            Assert.AreEqual(p, q);
        }

        [Test]
        public void Rotate8()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = p.Rotate(Puzzle.CounterClockwise).Rotate(Puzzle.CounterClockwise).Rotate(Puzzle.CounterClockwise).Rotate(Puzzle.CounterClockwise);
            Assert.AreEqual(p, q);
        }

        [Test]
        public void Rotate9()
        {
            int[,] before = new int[,] { 
{ 1, 2 }, 
{ 3, 5 } };
            int[,] after = new int[,] { 
{ 3, 1 }, 
{ 5, 2 } };
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            Assert.AreEqual(q, p.Rotate(Puzzle.Clockwise));
        }


        [Test]
        public void Rotate10()
        {
            int[,] before = new int[,] {
{ 1, 2 }, 
{ 3, 5 } };
            int[,] after = new int[,] { 
{ 2, 5 }, 
{ 1, 3 } };
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            Assert.AreEqual(q, p.Rotate(Puzzle.CounterClockwise));
        }

        [Test]
        public void Rotate11()
        {
            int[,] before = new int[,] { 
{ 1, 2, 3 },
{ 5, 7, 11 } };
            int[,] after = new int[,] { 
{ 5, 1 }, 
{ 7, 2 },
{11, 3}};
            Puzzle p = new Puzzle(before);
            Puzzle q = new Puzzle(after);
            Assert.AreEqual(q, p.Rotate(Puzzle.Clockwise));
        }

        [Test]
        public void Duplicate()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = p.Duplicate();
            Assert.AreNotSame(p, q);
            Assert.AreEqual(p, q);
        }

        [Test]
        public void IsFullGrid1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsFull());
        }

        [Test]
        public void IsFullGrid2()
        {
            Puzzle puz = new Puzzle(testGrid);
            puz[0, 0] = -1;
            Assert.IsFalse(puz.IsFull());
        }

        [Test]
        public void IsFullGrid3()
        {
            Puzzle puz = new Puzzle(11, 13);
            Assert.IsFalse(puz.IsFull());
        }

        [Test]
        public void IsFullRow1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsFull(Puzzle.RowOrder, 2));
        }

        [Test]
        public void IsFullRow2()
        {
            Puzzle puz = new Puzzle(testGrid);
            puz[1, 2] = Puzzle.EmptyTile;
            Assert.IsFalse(puz.IsFull(Puzzle.RowOrder, 2));
        }

        [Test]
        public void IsFullRow3()
        {
            Puzzle puz = new Puzzle(3, 5);
            Assert.IsFalse(puz.IsFull(Puzzle.RowOrder, 2));
        }
        [Test]
        public void IsFullRowBad1()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            Assert.IsFalse(p.IsFull(Puzzle.RowOrder, -1));
        }

        [Test]
        public void IsFullRowBad2()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            Assert.IsFalse(p.IsFull(Puzzle.RowOrder, p.Height));
        }


        [Test]
        public void IsFullColumn1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsFull(Puzzle.ColumnOrder, 2));
        }

        [Test]
        public void IsFullColumn2()
        {
            Puzzle puz = new Puzzle(testGrid);
            puz[2, 1] = Puzzle.EmptyTile;
            Assert.IsFalse(puz.IsFull(Puzzle.ColumnOrder, 2));
        }

        [Test]
        public void IsFullColumn3()
        {
            Puzzle puz = new Puzzle(3, 5);
            Assert.IsFalse(puz.IsFull(Puzzle.ColumnOrder, 2));
        }
        [Test]
        public void IsFullColumnBad1()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            Assert.IsFalse(p.IsFull(Puzzle.ColumnOrder, -1));
        }

        [Test]
        public void IsFullColumnBad2()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            Assert.IsFalse(p.IsFull(Puzzle.ColumnOrder, p.Width));
        }

        [Test]
        public void IsFullRect()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsFull(1, 1, 2, 2));
        }
        [Test]
        public void IsFullRectBad1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(-2, -2, 2, 2));
        }

        [Test]
        public void IsFullRectBad2()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(-2, 0, 2, 2));
        }

        [Test]
        public void IsFullRectBad3()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(-2, puz.Height, 2, 2));
        }

        [Test]
        public void IsFullRectBad4()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(0, -2, 2, 2));
        }

        [Test]
        public void IsFullRectBad5()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(0, puz.Height, 2, 2));
        }

        [Test]
        public void IsFullRectBad6()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(puz.Width, -2, 2, 2));
        }

        [Test]
        public void IsFullRectBad7()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(puz.Width, 0, 2, 2));
        }

        [Test]
        public void IsFullRectBad8()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(puz.Width, puz.Height, 2, 2));
        }
        [Test]
        public void IsFullShape()
        {
            Puzzle puz = new Puzzle(testGrid);
            int[,] shape = new int[,] { { 1, 2, -1 }, { -1, 3, 4 } };
            Assert.IsTrue(puz.IsFull(0, 0, shape));
        }

        [Test]
        public void IsFullShapeBad1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(-2, -2, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }

        [Test]
        public void IsFullShapeBad2()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(-2, 0, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }

        [Test]
        public void IsFullShapeBad3()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(-2, puz.Height, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }

        [Test]
        public void IsFullShapeBad4()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(0, -2, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }

        [Test]
        public void IsFullShapeBad5()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(0, puz.Height, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }

        [Test]
        public void IsFullShapeBad6()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(puz.Width, -2, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }

        [Test]
        public void IsFullShapeBad7()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(puz.Width, 0, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }

        [Test]
        public void IsFullShapeBad8()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(puz.Width, puz.Height, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }
        [Test]
        public void IsFullShapeBad9()
        {
            Puzzle p = new Puzzle(testGrid);
            Assert.IsFalse(p.IsFull(0, 0, (int[,])null));
        }

        [Test]
        public void IsFullShapeBad10()
        {
            Puzzle p = new Puzzle(testGrid);
            Assert.IsFalse(p.IsFull(0, 0, new int[0, 0]));
        }

        [Test]
        public void IsFullPuzzle()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle shape = new Puzzle(new int[,] { { 1, 2, -1 }, { -1, 3, 5 } });
            Assert.IsTrue(puz.IsFull(0, 0, shape));
        }

        [Test]
        public void IsFullPuzzleBad1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(-2, -2, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }

        [Test]
        public void IsFullPuzzleBad2()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(-2, 0, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }

        [Test]
        public void IsFullPuzzleBad3()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(-2, puz.Height, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }

        [Test]
        public void IsFullPuzzleBad4()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(0, -2, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }

        [Test]
        public void IsFullPuzzleBad5()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(0, puz.Height, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }

        [Test]
        public void IsFullPuzzleBad6()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(puz.Width, -2, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }

        [Test]
        public void IsFullPuzzleBad7()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(puz.Width, 0, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }

        [Test]
        public void IsFullPuzzleBad8()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsFull(puz.Width, puz.Height, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }
        [Test]
        public void IsFullPuzzleBad9()
        {
            Puzzle p = new Puzzle(testGrid);
            Assert.IsFalse(p.IsFull(0, 0, (Puzzle)null));
        }


        [Test]
        public void IsEmptyGrid1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsEmpty());
        }

        [Test]
        public void IsEmptyGrid2()
        {
            Puzzle puz = new Puzzle(testGrid);
            puz[0, 0] = -1;
            Assert.IsFalse(puz.IsEmpty());
        }

        [Test]
        public void IsEmptyGrid3()
        {
            Puzzle puz = new Puzzle(11, 13);
            Assert.IsTrue(puz.IsEmpty());
        }

        [Test]
        public void IsEmptyRow1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsEmpty(Puzzle.RowOrder, 2));
        }

        [Test]
        public void IsEmptyRow2()
        {
            Puzzle puz = new Puzzle(testGrid);
            puz[1, 2] = Puzzle.EmptyTile;
            Assert.IsFalse(puz.IsEmpty(Puzzle.RowOrder, 2));
        }

        [Test]
        public void IsEmptyRow3()
        {
            Puzzle puz = new Puzzle(3, 5);
            Assert.IsTrue(puz.IsEmpty(Puzzle.RowOrder, 2));
        }
        [Test]
        public void IsEmptyRowBad1()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            Assert.IsTrue(p.IsEmpty(Puzzle.RowOrder, -1));
        }

        [Test]
        public void IsEmptyRowBad2()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            Assert.IsTrue(p.IsEmpty(Puzzle.RowOrder, p.Height));
        }


        [Test]
        public void IsEmptyColumn1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsEmpty(Puzzle.ColumnOrder, 2));
        }

        [Test]
        public void IsEmptyColumn2()
        {
            Puzzle puz = new Puzzle(testGrid);
            puz[2, 1] = Puzzle.EmptyTile;
            Assert.IsFalse(puz.IsEmpty(Puzzle.ColumnOrder, 2));
        }

        [Test]
        public void IsEmptyColumn3()
        {
            Puzzle puz = new Puzzle(3, 5);
            Assert.IsTrue(puz.IsEmpty(Puzzle.ColumnOrder, 2));
        }
        [Test]
        public void IsEmptyColumnBad1()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            Assert.IsTrue(p.IsEmpty(Puzzle.ColumnOrder, -1));
        }

        [Test]
        public void IsEmptyColumnBad2()
        {
            Puzzle p = new Puzzle(testGrid);
            Puzzle q = new Puzzle(testGrid);
            Assert.IsTrue(p.IsEmpty(Puzzle.ColumnOrder, p.Width));
        }

        [Test]
        public void IsEmptyRect1()
        {
            Puzzle puz = new Puzzle(3, 5);
            Assert.IsTrue(puz.IsEmpty(1, 1, 2, 2));
        }

        [Test]
        public void IsEmptyRect2()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsFalse(puz.IsEmpty(1, 1, 2, 2));
        }

        [Test]
        public void IsEmptyRect3()
        {
            Puzzle puz = new Puzzle(testGrid);
            puz[1, 1] = Puzzle.EmptyTile;
            Assert.IsFalse(puz.IsEmpty(1, 1, 2, 2));
        }

        [Test]
        public void IsEmptyRectBad1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(-2, -2, 2, 2));
        }

        [Test]
        public void IsEmptyRectBad2()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(-2, 0, 2, 2));
        }

        [Test]
        public void IsEmptyRectBad3()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(-2, puz.Height, 2, 2));
        }

        [Test]
        public void IsEmptyRectBad4()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(0, -2, 2, 2));
        }

        [Test]
        public void IsEmptyRectBad5()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(0, puz.Height, 2, 2));
        }

        [Test]
        public void IsEmptyRectBad6()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(puz.Width, -2, 2, 2));
        }

        [Test]
        public void IsEmptyRectBad7()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(puz.Width, 0, 2, 2));
        }

        [Test]
        public void IsEmptyRectBad8()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(puz.Width, puz.Height, 2, 2));
        }
        [Test]
        public void IsEmptyShape()
        {
            Puzzle puz = new Puzzle(testGrid);
            int[,] shape = new int[,] { { 1, 2, -1 }, { -1, 3, 4 } };
            Assert.IsFalse(puz.IsEmpty(0, 0, shape));
        }

        [Test]
        public void IsEmptyShapeBad1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(-2, -2, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }

        [Test]
        public void IsEmptyShapeBad2()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(-2, 0, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }

        [Test]
        public void IsEmptyShapeBad3()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(-2, puz.Height, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }

        [Test]
        public void IsEmptyShapeBad4()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(0, -2, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }

        [Test]
        public void IsEmptyShapeBad5()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(0, puz.Height, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }

        [Test]
        public void IsEmptyShapeBad6()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(puz.Width, -2, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }

        [Test]
        public void IsEmptyShapeBad7()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(puz.Width, 0, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }

        [Test]
        public void IsEmptyShapeBad8()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(puz.Width, puz.Height, new int[2, 2] { { 1, -1 }, { 2, 3 } }));
        }
        [Test]
        public void IsEmptyShapeBad9()
        {
            Puzzle p = new Puzzle(testGrid);
            Assert.IsFalse(p.IsEmpty(0, 0, (int[,])null));
        }

        [Test]
        public void IsEmptyShapeBad10()
        {
            Puzzle p = new Puzzle(testGrid);
            Assert.IsFalse(p.IsEmpty(0, 0, new int[0, 0]));
        }

        [Test]
        public void IsEmptyPuzzle()
        {
            Puzzle puz = new Puzzle(testGrid);
            Puzzle shape = new Puzzle(new int[,] { { 1, 2, -1 }, { -1, 3, 5 } });
            Assert.IsFalse(puz.IsEmpty(0, 0, shape));
        }

        [Test]
        public void IsEmptyPuzzleBad1()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(-2, -2, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }

        [Test]
        public void IsEmptyPuzzleBad2()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(-2, 0, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }

        [Test]
        public void IsEmptyPuzzleBad3()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(-2, puz.Height, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }

        [Test]
        public void IsEmptyPuzzleBad4()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(0, -2, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }

        [Test]
        public void IsEmptyPuzzleBad5()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(0, puz.Height, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }

        [Test]
        public void IsEmptyPuzzleBad6()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(puz.Width, -2, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }

        [Test]
        public void IsEmptyPuzzleBad7()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(puz.Width, 0, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }

        [Test]
        public void IsEmptyPuzzleBad8()
        {
            Puzzle puz = new Puzzle(testGrid);
            Assert.IsTrue(puz.IsEmpty(puz.Width, puz.Height, new Puzzle(new int[2, 2] { { 1, -1 }, { 2, 2 } })));
        }
        [Test]
        public void IsEmptyPuzzleBad9()
        {
            Puzzle p = new Puzzle(testGrid);
            Assert.IsFalse(p.IsEmpty(0, 0, (Puzzle)null));
        }

    }
}