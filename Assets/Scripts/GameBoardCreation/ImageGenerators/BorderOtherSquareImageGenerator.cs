using System.Drawing;

public class BorderOtherSquareImageGenerator : BorderSquareImageGeneratorBase
{
    public BorderOtherSquareImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
    {
    }
    public override Bitmap GetImage(Square square)
    {
        var bitmap = new Bitmap(100, 100);
        using (Graphics gfx = Graphics.FromImage(bitmap))
        using (SolidBrush brush = new SolidBrush(MonopolyClassicColorTheme.LightGreen))
        {
            gfx.FillRectangle(brush, 0, 0, 100, 100);
            Pen blackBorderPen = new Pen(MonopolyClassicColorTheme.Black, 1);
            gfx.DrawRectangle(blackBorderPen, 0, 0, 100, 100);
        }
        return bitmap;
    }
}
