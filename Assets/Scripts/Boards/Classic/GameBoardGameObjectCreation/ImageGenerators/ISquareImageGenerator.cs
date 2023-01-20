using System.Drawing;
using Squares;

namespace Boards.Classic.GameBoardGameObjectCreation.ImageGenerators
{
    public interface ISquareImageGenerator
    {
        Bitmap GetImage(Square square);
    }
}
