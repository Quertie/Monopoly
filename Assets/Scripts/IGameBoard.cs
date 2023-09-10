using System.Collections.Generic;
using Squares;

public interface IGameBoard
{
    List<ColorGroup> ColorGroups { get; }
    List<Square> Squares { get; }
    List<Square> CurrentSquare { get; set; }

    int GetSquareIndex(Square square);
    Square GetLandingSquare(Square originSquare, int diceRoll);
    List<Square> GetWaypoint(Square originSquare, Square destinationSquare);

}
