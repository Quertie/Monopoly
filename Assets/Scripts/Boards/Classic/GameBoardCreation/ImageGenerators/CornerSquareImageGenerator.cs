using System.Drawing;
using System.Drawing.Text;

public class CornerSquareImageGenerator : ISquareImageGenerator
{

    private const int SquareSize = 500;
    protected const int borderThickness = 16;

    public Bitmap GetImage(Square square)
    {
        var bitmap = InitImageWithBackground();
        return bitmap;
    }

    protected Bitmap InitImageWithBackground()
    {
        var bitmap = new Bitmap(SquareSize, SquareSize);
        
        using (var gfx = System.Drawing.Graphics.FromImage(bitmap))
        using (var greenBrush = new SolidBrush(MonopolyClassicTheme.LightGreen))
        using (var blackBrush = new SolidBrush(MonopolyClassicTheme.Black))
        {
            gfx.FillRectangle(greenBrush, 0, 0, SquareSize, SquareSize);
            Pen blackBorderPen = new Pen(MonopolyClassicTheme.Black, borderThickness);
            //Offset is necessary otherwise to correct some border problems
            gfx.DrawRectangle(blackBorderPen, -borderThickness/4, -borderThickness/4, SquareSize, SquareSize);
        }
        return bitmap;
    }
}