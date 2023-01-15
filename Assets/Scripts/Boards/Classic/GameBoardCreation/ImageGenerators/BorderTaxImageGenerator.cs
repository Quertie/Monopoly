using System.Drawing;
using Boards.Classic.Style;

namespace Boards.Classic.GameBoardCreation.ImageGenerators
{
    public abstract class BorderTaxImageGenerator : BorderSquareImageGeneratorBase
    {
        protected BorderTaxImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
        {
        }

        protected void DrawTaxNameToImage(Bitmap bitmap, string taxName)
        {
            DrawTextToImage(bitmap, taxName.ToUpper(), .08f, MonopolyClassicTheme.TaxNameFontSize);
        }

        protected void DrawAmountDueToImage(Bitmap bitmap, int amountDue)
        {
            DrawTextToBottomOfImage(bitmap, $"PAYEZ M{amountDue}");
        }
    }
}
