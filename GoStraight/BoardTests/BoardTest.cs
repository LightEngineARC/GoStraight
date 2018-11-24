using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Board;

namespace BoardTests
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void TestReadFileToBoard()
        {
            Board board = new Board("StartBoard.txt");
        }
    }
}
