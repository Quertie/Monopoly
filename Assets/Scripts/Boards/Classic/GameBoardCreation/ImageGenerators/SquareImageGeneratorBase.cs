using System.IO;
using System.Drawing;
using UnityEngine;

public abstract class SquareImageGeneratorBase:ISquareImageGenerator
{
    private const int SmallestSide = 500;
    protected const int borderThickness = 16;
    
    protected float _squareHeight;
    protected float _squareWidth;

    protected SquareImageGeneratorBase(float squareHeight, float squareWidth)
    {
        _squareHeight = squareHeight;
        _squareWidth = squareWidth;
    }

    public abstract Bitmap GetImage(Square square);

    protected (int height, int width) GetImageSize()
    {
        if (_squareHeight>_squareWidth) return ((int)(SmallestSide*_squareHeight/_squareWidth), SmallestSide);
        return (SmallestSide, (int)(SmallestSide * _squareWidth/_squareHeight));
    }

    protected void DrawImageToImage(Bitmap bitmap, string imageAssetPath, float imageMarginPct, float imageVerticalPositionPct)
    {
        var (height, width) = GetImageSize();
        
        using (var gfx = System.Drawing.Graphics.FromImage(bitmap))
        using (var blackBrush = new SolidBrush(MonopolyClassicTheme.Black))
        {
            var trainImage = new Bitmap(Path.Combine(Application.streamingAssetsPath, imageAssetPath).Replace("/", "\\"));
            var imageMargin = (int)(width * imageMarginPct);
            var imageWidth = (int)(width - imageMargin * 2);
            var imageHeight = (int)((float)trainImage.Height / trainImage.Width * imageWidth);
            gfx.DrawImage(trainImage, imageMargin, height * imageVerticalPositionPct, imageWidth, imageHeight);
        }
    }
}