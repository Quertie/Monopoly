using System.Drawing;
using System.IO;

public class BorderWaterWorksImageGenerator : BorderDeedSquareImageGenerator
{
    public BorderWaterWorksImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
    {
    }

    public override Bitmap GetImage(Square square)
    {
        var deed = (Deed)square;
        var (height, width) = GetImageSize();

        var bitmap = InitImageWithBackground();
        DrawTextToImage(bitmap, deed.Name.ToUpper(), .08f, MonopolyClassicTheme.PropertyNameFontSize);
        DrawImageToImage(bitmap, Path.Combine("Classic", "Images", "water-works.png"), .1f, .37f);
        DrawPriceToImage(bitmap, deed);
        return bitmap;
    }
}