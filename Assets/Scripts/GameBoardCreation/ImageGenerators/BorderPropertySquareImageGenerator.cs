using System.Drawing;
using System.Drawing.Text;
using UnityEngine;

public class BorderPropertySquareImageGenerator : BorderSquareImageGeneratorBase
{
    private const int SmallestSide = 500;
    private const int PropertyNameFontSize = 40;
    private const int PropertyPriceFontSize = 38;
    private const int borderThickness = 16;

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
            Pen blackBorderPen = new Pen(MonopolyClassicTheme.Black, borderThickness);
            //Offset is necessary otherwise to correct some border problems
            gfx.DrawRectangle(blackBorderPen, 0, borderThickness/2, width, height-borderThickness/2);

            gfx.FillRectangle(propertyColorBrush, 0, 0, width, height/5);
            gfx.DrawRectangle(blackBorderPen, 0, borderThickness/2, width, height/5);

            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            var textBoxMargin = (int)(width*0.02);
            var textBoxWidth = (int)(width-textBoxMargin*2);

            var nameBox = new RectangleF(textBoxMargin, height*.3f, textBoxWidth, height*.2f);
            gfx.DrawString(property.Name.ToUpper(), new System.Drawing.Font(MonopolyClassicTheme.DeedNameFontFamily, PropertyNameFontSize), blackBrush, nameBox, stringFormat);

            var priceBox = new RectangleF(textBoxMargin, height*.85f, textBoxWidth, height*.2f);
            gfx.DrawString($"M{property.Price.ToString()}", new System.Drawing.Font(MonopolyClassicTheme.DeedNameFontFamily, PropertyPriceFontSize), blackBrush, priceBox, stringFormat);
        }
        return bitmap;
    }

    public (int height, int width) GetImageSize()
    {
        if (_squareHeight>_squareWidth) return ((int)(SmallestSide*_squareHeight/_squareWidth), SmallestSide);
        return (SmallestSide, (int)(SmallestSide * _squareWidth/_squareHeight));
    }
}
