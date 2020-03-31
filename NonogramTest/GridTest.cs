using Microsoft.VisualStudio.TestTools.UnitTesting;
using nonogram.Core;

namespace NonogramTest
{
    [TestClass]
    public class GridTest
    {
        private char[,] bird = new char[10, 10] 
        { 
            {'.', '.', '.', '.', '.', '.', '.', '.', '#', '#',},
            {'.', '.', '#', '#', '.', '.', '.', '#', '#', '#',},
            {'.', '#', '.', '#', '#', '.', '#', '#', '#', '#',},
            {'#', '#', '#', '#', '#', '#', '#', '#', '#', '.',},
            {'.', '.', '#', '#', '#', '#', '#', '#', '.', '.',},
            {'.', '.', '.', '#', '#', '#', '#', '#', '#', '.',},
            {'.', '.', '.', '.', '#', '#', '#', '#', '#', '#',},
            {'.', '.', '.', '#', '.', '#', '.', '#', '#', '#',},
            {'.', '.', '.', '.', '.', '.', '.', '.', '#', '#',},
            {'.', '.', '.', '.', '.', '.', '.', '#', '#', '.',} 
        };

        [TestMethod]
        public void SolveGridTest()
        {
            var grid = new Grid(bird);
            grid.CurrentState = GameState.Playing;

            //Solve() should automatically set GameState to Lost 
            grid.Solve();
            Assert.AreEqual<GameState>(GameState.Lost, grid.CurrentState);

            //IsWon() checks if all Input TileStates equal Actual Tilestates, if yes -> set GameState to Won
            grid.IsWon();
            Assert.AreEqual<GameState>(GameState.Won, grid.CurrentState);
        }

        [TestMethod]
        public void ActualTileTest()
        {
            var grid = new Grid(bird);

            Assert.AreEqual<TileState>(TileState.Blank, grid.Tiles[0,0].Actual);
            Assert.AreEqual<TileState>(TileState.Colored, grid.Tiles[0,9].Actual);
            Assert.AreEqual<TileState>(TileState.Colored, grid.Tiles[3,0].Actual);
            Assert.AreEqual<TileState>(TileState.Blank, grid.Tiles[3,9].Actual);
        }

        [TestMethod]
        public void InputTileTest() 
        {
            var grid = new Grid(bird);

            //x and y in 2D arrays are represented opposite (array[y,x]) to our standard thinking, therefore input is mapped vice versa
            grid.MarkTile(0,0);
            Assert.AreEqual<TileState>(TileState.Colored, grid.Tiles[0,0].Input);
            grid.MarkTile(2,1);
            Assert.AreEqual<TileState>(TileState.Colored, grid.Tiles[1,2].Input);

            grid.BlankTile(9,9);
            Assert.AreEqual<TileState>(TileState.Blank, grid.Tiles[9,9].Input);
            grid.BlankTile(1,2);
            Assert.AreEqual<TileState>(TileState.Blank, grid.Tiles[2,1].Input);
        }

        [TestMethod]
        public void GridSizeTest() 
        {
            var grid = new Grid(bird);

            Assert.AreEqual<int>(bird.GetLength(0), grid.ySize);
            Assert.AreEqual<int>(bird.GetLength(1), grid.xSize);
        }

        [TestMethod]
        public void ClearGridTest()
        {
            var grid = new Grid(bird);
            grid.BlankTile(1,2);
            grid.BlankTile(3,2);
            grid.BlankTile(7,9);
            grid.BlankTile(9,9);
            grid.MarkTile(2,3);
            grid.MarkTile(5,1);
            grid.MarkTile(2,9);
            grid.MarkTile(2,4);

            grid.Clear();
            foreach (var tile in grid.Tiles)
            {
                Assert.AreEqual<TileState>(TileState.Hidden, tile.Input);
            }
        }
    }
}
