using System.Collections.Generic;
using System.Linq;
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
        public void GameBoard_ReturnsCorrectLandingSquare(int numberOfSquaresOnBoard, int originIndex, int diceRoll,
            int expectedDestinationIndex)
        {
            // Arrange
            var squares = GetSquares(numberOfSquaresOnBoard);
            var gameBoard = new GameBoard(squares, null, 1);

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

        [TestCaseSource(nameof(GetSquareIndicesInBetweenTestCaseSource))]
        public void GetSquareIndicesInBetween_WorksCorrectly(int originSquareIndex, int destinationSquareIndex,
            int[] expectedSquaresInBetween)
        {
            // Arrange
            var squares = GetSquares(8);
            var gameBoard = new GameBoard(squares, null, 1);

            // Act
            var squaresInBetween = gameBoard.GetSquareIndicesInBetween(originSquareIndex, destinationSquareIndex);

            // Assert
            Assert.That(squaresInBetween.SequenceEqual(expectedSquaresInBetween));
        }

        public static object[] GetSquareIndicesInBetweenTestCaseSource =
        {
            new object[] { 1, 7, new[] { 2, 3, 4, 5, 6 } },
            new object[] { 5, 3, new[] { 6, 7, 0, 1, 2 } },
            new object[] { 5, 0, new[] { 6, 7 } },
            new object[] { 0, 5, new[] { 1, 2, 3, 4 } }
        };

        [TestCaseSource(nameof(OrderFromCurrentSquareTestCaseSource))]
        public void OrderFromCurrentSquare_WorksCorrectly(int currentSquareIndex, List<int> squareIndexList,
            List<int> expectedOrderedSquareListFromCurrent)
        {
            // Arrange
            var squares = GetSquares(8);
            var gameBoard = new GameBoard(squares, null, 1);
            
            // Act
            var orderedSquaresFromCurrent = gameBoard.OrderFromCurrentSquare(squareIndexList, currentSquareIndex);

            // Assert
            Assert.That(orderedSquaresFromCurrent.SequenceEqual(expectedOrderedSquareListFromCurrent));
        }

        public static object[] OrderFromCurrentSquareTestCaseSource =
        {
            new object[] { 5, new List<int> { 6, 0, 2, 4 }, new List<int> { 6, 0, 2, 4 } },
            new object[] { 5, new List<int> { 0, 2, 4, 6 }, new List<int> { 6, 0, 2, 4 } },
            new object[] { 4, new List<int> { 4, 6, 0, 2 }, new List<int> { 6, 0, 2, 4 } },
            new object[] { 7, new List<int> { 0, 2, 4, 6 }, new List<int> { 0, 2, 4, 6 } }
        };
    }
}
