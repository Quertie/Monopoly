using System.Drawing;
using System.Drawing.Text;
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

    public void DrawTextToBottomOfImage(Bitmap bitmap, string text)
    {
        DrawTextToImage(bitmap, text, .85f, MonopolyClassicTheme.SmallFontSize);
    }
}
