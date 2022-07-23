using System.Drawing;

public class SquareImageGenerator
{
    public Bitmap GetImage(Square square)
    {
        var bitmap = new Bitmap(100,100);
        using (Graphics gfx = Graphics.FromImage(bitmap))
        using (SolidBrush brush = new SolidBrush(Color.FromArgb(255,255,0)))
        {
            gfx.FillRectangle(brush, 0,0, 100, 100);
        }
        return bitmap;
    }
}