using System.Collections.Generic;
using Boards.Classic;
using Moq;
using NUnit.Framework;
using Squares;

namespace Tests.Tests.Boards.Classic
{
    public class GameBoardTest
    {
        [TestCase(8, 0, 5, 5)]
        [TestCase(8, 2, 5, 7)]
        [TestCase(8, 3, 5, 0)]
        [TestCase(16, 12, 12, 8)]
        public void GameBoard_ReturnsCorrectLandingSquare(int numberOfSquaresOnBoard, int originIndex, int diceRoll, int expectedDestinationIndex)
        {
            // Arrange
            var squares = GetSquares(numberOfSquaresOnBoard);
            var gameBoard = new GameBoard(squares, null);
            var originSquare = squares[0];
            var expectedDestinationSquare = squares[5];
            
            //Act
            var destinationIndex = gameBoard.GetLandingSquareIndex(originIndex, diceRoll);
            
            //Assert
            Assert.That(destinationIndex == expectedDestinationIndex);
        }

        private static List<Square> GetSquares(int numberOfSquares)
        {
            var squares = new List<Square>();
            for (var i = 0; i < numberOfSquares; i++)
            {
                squares.Add(Mock.Of<Square>());
            }
            return squares;
        }
    }
}
