using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoStraight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoStraight.Tests
{
    [TestClass()]
    public class BoardTests
    {
        [TestMethod()]
        public void BoardTest()
        {
            
            // Arrange
            string filename = "StartBoard.txt";
            Board board = new Board(filename);

            // Act
            

            // Assert

            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetCoordinateTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetPathColorTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetWallColorTest()
        {
            throw new NotImplementedException();
        }
    }
}