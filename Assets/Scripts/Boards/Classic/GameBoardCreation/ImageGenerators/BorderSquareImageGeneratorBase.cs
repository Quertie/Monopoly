using System.Drawing;
using UnityEngine;

public abstract class BorderSquareImageGeneratorBase : SquareImageGeneratorBase
{
    protected BorderSquareImageGeneratorBase(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
    {
    }

    protected Bitmap InitImageWithBackground()
    {
        var (height, width) = GetImageSize();
        var bitmap = new Bitmap(width, height);
        
        using (var gfx = System.Drawing.Graphics.FromImage(bitmap))
        using (var greenBrush = new SolidBrush(MonopolyClassicTheme.LightGreen))
        using (var blackBrush = new SolidBrush(MonopolyClassicTheme.Black))
        {
            gfx.FillRectangle(greenBrush, 0, 0, width, height);
            Pen blackBorderPen = new Pen(MonopolyClassicTheme.Black, borderThickness);
            //Offset is necessary otherwise to correct some border problems
            gfx.DrawRectangle(blackBorderPen, 0, 0, width, height-borderThickness/2);
        }
        return bitmap;
    }

    public void DrawTextToBottomOfImage(Bitmap bitmap, string text)
    {
        DrawTextToImage(bitmap, text, .85f, MonopolyClassicTheme.SmallFontSize);
    }
}
