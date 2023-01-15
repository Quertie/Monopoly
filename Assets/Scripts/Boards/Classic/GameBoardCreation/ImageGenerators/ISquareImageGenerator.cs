using System.Drawing;
using Squares;

namespace Boards.Classic.GameBoardCreation.ImageGenerators
{
    public interface ISquareImageGenerator
    {
        Bitmap GetImage(Square square);
    }
}
