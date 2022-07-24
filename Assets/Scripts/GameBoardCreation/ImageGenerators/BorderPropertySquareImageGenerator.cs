using System.Drawing;
using System.Drawing.Text;
using UnityEngine;

public class BorderPropertySquareImageGenerator : BorderSquareImageGeneratorBase
{
    private const int SmallestSide = 500;
    private const int PropertyNameFontSize = 40;

    public BorderPropertySquareImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
    {
    }

    public override Bitmap GetImage(Square square)
    {
        var imageSize = GetImageSize();
        var height = imageSize.height;
        var width = imageSize.width;
        var bitmap = new Bitmap(width, height);
        
        var property = (Property)square;

        using (var gfx = System.Drawing.Graphics.FromImage(bitmap))
        using (var greenBrush = new SolidBrush(MonopolyClassicTheme.LightGreen))
        using (var blackBrush = new SolidBrush(MonopolyClassicTheme.Black))
        using (var propertyColorBrush = new SolidBrush(property.ColorGroup.Color))
        {
            gfx.FillRectangle(greenBrush, 0, 0, width, height);
            Pen blackBorderPen = new Pen(MonopolyClassicTheme.Black, 5);
            gfx.DrawRectangle(blackBorderPen, 0, 0, width, height);

            gfx.FillRectangle(propertyColorBrush, 0, 0, width, height/5);
            gfx.DrawRectangle(blackBorderPen, 0, 0, width, height/5);

            var nameBox = new RectangleF(.02f*width, height*.3f, width*.96f, height*.2f);
            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            gfx.DrawString(square.Name.ToUpper(), new System.Drawing.Font(MonopolyClassicTheme.DeedNameFontFamily, PropertyNameFontSize), blackBrush, nameBox, stringFormat);
        }
        bitmap.Save("square.bmp");
        return bitmap;
    }

    public (int height, int width) GetImageSize()
    {
        if (_squareHeight>_squareWidth) return ((int)(SmallestSide*_squareHeight/_squareWidth), SmallestSide);
        return (SmallestSide, (int)(SmallestSide * _squareWidth/_squareHeight));
    }
}
