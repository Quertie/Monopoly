using System.Drawing;
using Squares;

namespace Boards.Classic.GameBoardGameObjectCreation.ImageGenerators
{
    public abstract class BorderDeedSquareImageGenerator:BorderSquareImageGeneratorBase
    {
        protected BorderDeedSquareImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
        {
        }

        protected void DrawPriceToImage(Bitmap bitmap, Deed deed)
        {
            DrawTextToBottomOfImage(bitmap, $"M{deed.Price}");
        }
    }
}
