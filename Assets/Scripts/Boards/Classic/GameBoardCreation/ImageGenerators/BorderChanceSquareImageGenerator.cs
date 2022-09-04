using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class BorderChanceImageGenerator:BorderSquareImageGeneratorBase
{
    private int chanceSquareOccurence = 0;

    private Dictionary<int, string> occurenceToFileName = new Dictionary<int, string> ()
    {
        {0, "chance-violet.png"},
        {1, "chance-blue.png"},
        {2, "chance-orange.png"}
    };
    
    public BorderChanceImageGenerator(float _squareHeight, float _squareWidth):base(_squareHeight, _squareWidth)
    {
    }

    public override Bitmap GetImage(Square square)
    {
        var bitmap = InitImageWithBackground();

        DrawTextToImage(bitmap, square.Name.ToUpper(), .08f, MonopolyClassicTheme.PropertyNameFontSize);
        DrawImageToImage(bitmap, Path.Combine("Classic", "Images", occurenceToFileName[chanceSquareOccurence++%3]), .22f, .2f);
        return bitmap;
    }
}