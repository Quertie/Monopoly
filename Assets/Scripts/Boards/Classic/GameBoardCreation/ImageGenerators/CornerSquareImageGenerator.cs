using System.Drawing;

public class CornerSquareImageGenerator : ISquareImageGenerator
{

    public Bitmap GetImage(Square square)
    {
        var bitmap = new Bitmap(100, 100);
        using (Graphics gfx = Graphics.FromImage(bitmap))
        using (SolidBrush brush = new SolidBrush(MonopolyClassicTheme.LightGreen))
        {
            gfx.FillRectangle(brush, 0, 0, 100, 100);
            Pen blackBorderPen = new Pen(MonopolyClassicTheme.Black, 1);
            gfx.DrawRectangle(blackBorderPen, 0, 0, 100, 100);
        }
        return bitmap;
    }
}