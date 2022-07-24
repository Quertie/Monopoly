using System.Drawing;
using System.Drawing.Text;

public class BorderPropertySquareImageGenerator : BorderSquareImageGeneratorBase
{
    private const int SmallestSide = 500;
    public BorderPropertySquareImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
    {
    }

    public override Bitmap GetImage(Square square)
    {
        var imageSize = GetImageSize();
        var height = imageSize.height;
        var width = imageSize.width;
        var bitmap = new Bitmap(height, width);
        
        var property = (Property)square;

        using (var gfx = Graphics.FromImage(bitmap))
        using (var greenBrush = new SolidBrush(MonopolyClassicColorTheme.LightGreen))
        using (var blackBrush = new SolidBrush(MonopolyClassicColorTheme.Black))
        using (var propertyColorBrush = new SolidBrush(property.ColorGroup.Color))
        {
            gfx.FillRectangle(greenBrush, 0, 0, width, height);
            Pen blackBorderPen = new Pen(MonopolyClassicColorTheme.Black, 5);
            gfx.DrawRectangle(blackBorderPen, 0, 0, width, height);

            gfx.FillRectangle(propertyColorBrush, 0, 0, width, height/5);
            gfx.DrawRectangle(blackBorderPen, 0, 0, width, height/5);

            var nameBox = new RectangleF(.2f*width, height*.3f, width*.96f, height*.2f);
            gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            gfx.DrawString(square.Name, new Font("Futura", 30), blackBrush, nameBox);
        }
        return bitmap;
    }

    public (int height, int width) GetImageSize()
    {
        if (_squareHeight>_squareWidth) return (SmallestSide*(int)(_squareHeight/_squareWidth), SmallestSide);
        return (SmallestSide, SmallestSide * (int)(_squareWidth/_squareHeight));
    }
}
