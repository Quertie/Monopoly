using System.Collections.Generic;
using Squares;

public interface IGameBoard
{
    List<ColorGroup> ColorGroups { get; }
    List<Square> Squares { get; }

    int GetSquareIndex(Square square);
    Square GetLandingSquare(Square originSquare, int diceRoll);
}
