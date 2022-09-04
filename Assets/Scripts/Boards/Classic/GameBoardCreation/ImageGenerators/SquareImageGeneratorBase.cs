using System.IO;
using System.Drawing;
using UnityEngine;
using System.Drawing.Text;

public abstract class SquareImageGeneratorBase:ISquareImageGenerator
{
    private const int BiggestSide = 500;
    protected const int borderThickness = 8;
    
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
        if (_squareHeight>_squareWidth) return (BiggestSide, (int)(BiggestSide*_squareWidth/_squareHeight));
        return ((int)(BiggestSide * _squareHeight/_squareWidth), BiggestSide);
    }

    protected void DrawTextToImage(Bitmap bitmap, string text, float heightPercent, int fontSize)
    {
        var (height, width) = GetImageSize();

        using (var gfx = System.Drawing.Graphics.FromImage(bitmap))
        using (var blackBrush = new SolidBrush(MonopolyClassicTheme.Black))
        {
            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            var textBoxMargin = (int)(width*0.02);
            var textBoxWidth = (int)(width-textBoxMargin*2);
            var textBox = new RectangleF(textBoxMargin, height*heightPercent, textBoxWidth, height*.3f);
            gfx.DrawString(text, new System.Drawing.Font(MonopolyClassicTheme.DeedNameFontFamily, fontSize), blackBrush, textBox, stringFormat);
        }
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