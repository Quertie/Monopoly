using System;
using System.Collections.Generic;
using System.Linq;
using Boards.Classic.Squares;
using Boards.Classic.Style;
using Squares;

namespace Boards.Classic
{
    public class GameBoard : IGameBoard
    {

        public List<Square> CurrentSquare { get; set; }
        public List<ColorGroup> ColorGroups { get; }
        public List<Square> Squares {get;}

        private int _numberOfPlayers;

        public GameBoard(List<Square> squares, List<ColorGroup> colorGroups, int numberOfPlayers)
        {
            Squares = squares;
            ColorGroups = colorGroups;
            CurrentSquare = new List<Square>();
            for (var i = 0; i < numberOfPlayers; i++)
            {
                CurrentSquare.Add(squares[0]);
            }
        }

        public int GetSquareIndex(Square square)
        {
            return Squares.IndexOf(square);
        }

        public Square GetLandingSquare(Square originSquare, int diceRoll)
        {
            var originSquareIndex = GetSquareIndex(originSquare);
            return Squares[GetLandingSquareIndex(originSquareIndex, diceRoll)];
        }

        public int GetLandingSquareIndex(int originIndex, int diceRoll)
        {
            return (originIndex + diceRoll) % Squares.Count;
        }

        public List<Square> GetWaypoint(Square originSquare, Square destinationSquare)
        {
            var originSquareIndex = GetSquareIndex(originSquare);
            var destinationSquareIndex = GetSquareIndex(destinationSquare);

            return GetSquareIndicesInBetween(originSquareIndex, destinationSquareIndex)
                .Select(i => Squares[i])
                .Append(destinationSquare)
                .ToList();
        }

        private IEnumerable<int> GetSquareIndicesInBetween(int originSquareIndex, int destinationSquareIndex)
        {
            if (destinationSquareIndex > originSquareIndex) return Enumerable.Range(originSquareIndex+1, destinationSquareIndex-originSquareIndex-1).ToList();
            var firstList = originSquareIndex != Squares.Count - 1
                ? Enumerable.Range(originSquareIndex + 1, Squares.Count - originSquareIndex - 1)
                : new List<int>();
            var secondList = Enumerable.Range(0, destinationSquareIndex);
            return firstList.Concat(secondList);
        }
    }
}
