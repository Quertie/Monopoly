using System.Drawing;

public abstract class BorderDeedSquareImageGenerator:BorderSquareImageGeneratorBase
{
    public BorderDeedSquareImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
    {
    }

    protected void DrawPriceToImage(Bitmap bitmap, Deed deed)
    {
        DrawTextToBottomOfImage(bitmap, $"M{deed.Price}");
    }
}
