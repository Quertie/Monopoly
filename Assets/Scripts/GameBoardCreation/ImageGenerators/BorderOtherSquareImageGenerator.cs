using System.Drawing;

public class BorderOtherSquareImageGenerator : BorderSquareImageGeneratorBase
{
    public BorderOtherSquareImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
    {
    }
    public override Bitmap GetImage(Square square)
    {
        return InitImageWithBackground();
    }
}
