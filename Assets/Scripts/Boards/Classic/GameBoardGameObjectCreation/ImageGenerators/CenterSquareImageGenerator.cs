using System.Drawing;
using Squares;

namespace Boards.Classic.GameBoardGameObjectCreation.ImageGenerators
{
    public class CenterSquareImageGenerator : CornerSquareImageGenerator
    {
        public CenterSquareImageGenerator(float squareHeight) : base(squareHeight)
        {
        }

        public override Bitmap GetImage(Square square)
        {
            return InitImageWithBackground();
        }
    }
}