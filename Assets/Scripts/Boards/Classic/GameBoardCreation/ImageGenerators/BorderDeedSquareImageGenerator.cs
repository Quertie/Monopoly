using System.Drawing;

public abstract class BorderDeedSquareImageGenerator:BorderSquareImageGeneratorBase
{
    public BorderDeedSquareImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
    {
    }

    private const int PropertyPriceFontSize = 38;

    protected void DrawPriceToImage(Bitmap bitmap, Deed deed)
    {
        var (height, width) = GetImageSize();

        DrawTextToImage(bitmap, $"M{deed.Price}", .85f, PropertyPriceFontSize);
    }
}
