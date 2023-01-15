using System.Drawing;
using Squares;

namespace Boards.Classic.GameBoardCreation.ImageGenerators
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