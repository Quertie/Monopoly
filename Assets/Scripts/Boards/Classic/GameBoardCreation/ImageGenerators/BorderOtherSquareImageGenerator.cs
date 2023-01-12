using System.Drawing;
using Squares;

namespace Boards.Classic.GameBoardCreation.ImageGenerators
{
    public class BorderOtherSquareImageGenerator : BorderSquareImageGeneratorBase
    {
        public BorderOtherSquareImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
        {
        }
        public override Bitmap GetImage(Square square)
        {
            return InitImageWithBackground();
        }
    }
}
