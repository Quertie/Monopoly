using System.Drawing;
using System.IO;
using Squares;

namespace Boards.Classic.GameBoardGameObjectCreation.ImageGenerators
{
    public class BorderLuxuryTaxImageGenerator : BorderTaxImageGenerator
    {
        public BorderLuxuryTaxImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
        {
        }


        public override Bitmap GetImage(Square square)
        {
            var tax = (Tax)square;

            var bitmap = InitImageWithBackground();
            DrawTaxNameToImage(bitmap, tax.Name.ToUpper());
            DrawImageToImage(bitmap, Path.Combine("Classic", "Images", "luxury-tax.png"), .2f, .47f);
            DrawAmountDueToImage(bitmap, tax.amountDue);
            return bitmap;
        }
    }
}
