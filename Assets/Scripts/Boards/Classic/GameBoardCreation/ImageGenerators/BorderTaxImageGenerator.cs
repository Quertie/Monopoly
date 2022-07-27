using System.Drawing;

public abstract class BorderTaxImageGenerator : BorderSquareImageGeneratorBase
{

    private const int TaxNameFontSize = 53;

    public BorderTaxImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
    {
    }

    protected void DrawTaxNameToImage(Bitmap bitmap, string taxName)
    {
        DrawTextToImage(bitmap, taxName.ToUpper(), .08f, TaxNameFontSize);
    }

    protected void DrawAmountDueToImage(Bitmap bitmap, int amountDue)
    {
        DrawTextToBottomOfImage(bitmap, $"PAYEZ M{amountDue}");
    }
}
