using System.Collections.Generic;

public interface IGameBoard
{
    List<ColorGroup> ColorGroups { get; }
    List<Square> Squares { get; }

    int GetSquareIndex(Square square);
}
