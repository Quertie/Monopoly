using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class BorderChanceImageGenerator:BorderSquareImageGeneratorBase
{

    private const int ChanceNameFontSize = 40;
    private int chanceSquareOccurence = 0;

    private Dictionary<int, string> occurenceToFileName = new Dictionary<int, string> ()
    {
        {0, "chance-orange.png"},
        {1, "chance-violet.png"},
        {2, "chance-blue.png"}
    };
    
    public BorderChanceImageGenerator(float _squareHeight, float _squareWidth):base(_squareHeight, _squareWidth)
    {
    }

    public override Bitmap GetImage(Square square)
    {
        var bitmap = InitImageWithBackground();

        DrawTextToImage(bitmap, square.Name.ToUpper(), .08f, ChanceNameFontSize);
        DrawImageToImage(bitmap, Path.Combine("Images", occurenceToFileName[chanceSquareOccurence++]), .22f, .2f);
        return bitmap;
    }
}