using System.Drawing;
using System.IO;
using Squares;

namespace Boards.Classic.GameBoardCreation.ImageGenerators
{
    public class BorderIncomeTaxImageGenerator : BorderTaxImageGenerator
    {
        public BorderIncomeTaxImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
        {
        }

        public override Bitmap GetImage(Square square)
        {
            var tax = (Tax)square;

            var bitmap = InitImageWithBackground();
            DrawTaxNameToImage(bitmap, tax.Name.ToUpper());
            DrawImageToImage(bitmap, Path.Combine("Classic", "Images", "income-tax.png"), .44f, .5f);
            DrawAmountDueToImage(bitmap, tax.amountDue);
            return bitmap;
        }
    }
}
