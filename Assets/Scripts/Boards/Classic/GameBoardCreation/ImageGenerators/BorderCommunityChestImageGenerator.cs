using System.Drawing;
using System.IO;

public class BorderCommunityChestImageGenerator:BorderSquareImageGeneratorBase
{
    public BorderCommunityChestImageGenerator(float _squareHeight, float _squareWidth):base(_squareHeight, _squareWidth)
    {
    }

    public override Bitmap GetImage(Square square)
    {
        var bitmap = InitImageWithBackground();

        DrawTextToImage(bitmap, square.Name.ToUpper(), .08f, MonopolyClassicTheme.PropertyNameFontSize);
        DrawImageToImage(bitmap, Path.Combine("Classic", "Images", "community-chest.png"), .1f, .45f);
        return bitmap;
    }
}