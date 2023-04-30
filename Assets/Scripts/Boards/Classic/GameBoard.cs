using System.Collections.Generic;
using System.Linq;
using Boards.Classic.Squares;
using Boards.Classic.Style;
using Squares;

namespace Boards.Classic
{
    public class GameBoard : IGameBoard
    {

        public Square CurrentSquare { get; set; }

        public List<ColorGroup> ColorGroups { get; }

        public List<Square> Squares {get;}

        public GameBoard(List<Square> squares, List<ColorGroup> colorGroups)
        {
            Squares = squares;
            ColorGroups = colorGroups;
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
    }
}
