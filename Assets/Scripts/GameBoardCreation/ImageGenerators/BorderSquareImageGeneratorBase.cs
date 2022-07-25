using System.Drawing;
using System.Drawing.Text;

public abstract class BorderSquareImageGeneratorBase : ISquareImageGenerator
{
    protected float _squareHeight;
    protected float _squareWidth;

    protected const int borderThickness = 16;
    private const int SmallestSide = 500;

    protected BorderSquareImageGeneratorBase(float squareHeight, float squareWidth)
    {
        _squareHeight = squareHeight;
        _squareWidth = squareWidth;
    }

    public abstract Bitmap GetImage(Square square);

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
            gfx.DrawRectangle(blackBorderPen, 0, borderThickness/2, width, height-borderThickness/2);
        }
        return bitmap;
    }

    protected (int height, int width) GetImageSize()
    {
        if (_squareHeight>_squareWidth) return ((int)(SmallestSide*_squareHeight/_squareWidth), SmallestSide);
        return (SmallestSide, (int)(SmallestSide * _squareWidth/_squareHeight));
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
            var textBox = new RectangleF(textBoxMargin, height*heightPercent, textBoxWidth, height*.2f);
            gfx.DrawString(text, new System.Drawing.Font(MonopolyClassicTheme.DeedNameFontFamily, fontSize), blackBrush, textBox, stringFormat);
        }
    }
}