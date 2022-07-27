using System.Drawing;
using System.IO;

public class BorderElectricCompanyImageGenerator : BorderDeedSquareImageGenerator
{

    private const int ElectricCompanyNameFontSize = 40;

    public BorderElectricCompanyImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
    {
    }

    public override Bitmap GetImage(Square square)
    {
        var deed = (Deed)square;
        var (height, width) = GetImageSize();

        var bitmap = InitImageWithBackground();
        DrawTextToImage(bitmap, deed.Name.ToUpper(), .08f, ElectricCompanyNameFontSize);
        DrawImageToImage(bitmap, Path.Combine("Classic", "Images", "electric-company.png"), .2f, .37f);
        DrawPriceToImage(bitmap, deed);
        return bitmap;
    }
}
