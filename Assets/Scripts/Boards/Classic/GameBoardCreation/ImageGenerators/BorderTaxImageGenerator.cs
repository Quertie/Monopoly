using System.Drawing;

public abstract class BorderTaxImageGenerator : BorderSquareImageGeneratorBase
{
    public BorderTaxImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
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
