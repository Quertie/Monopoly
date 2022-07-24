using System.Drawing;

public abstract class BorderSquareImageGeneratorBase : ISquareImageGenerator
{
    protected float _squareHeight;
    protected float _squareWidth;

    protected BorderSquareImageGeneratorBase(float squareHeight, float squareWidth)
    {
        _squareHeight = squareHeight;
        _squareWidth = squareWidth;
    }

    public abstract Bitmap GetImage(Square square);
}