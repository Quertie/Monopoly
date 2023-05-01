using System.Collections.Generic;
using Squares;

public interface IGameBoard
{
    List<ColorGroup> ColorGroups { get; }
    List<Square> Squares { get; }
    Square CurrentSquare { get; set; }

    int GetSquareIndex(Square square);
    Square GetLandingSquare(Square originSquare, int diceRoll);
}
