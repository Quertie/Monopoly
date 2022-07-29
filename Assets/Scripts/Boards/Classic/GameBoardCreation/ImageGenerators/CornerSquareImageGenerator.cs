using System.Drawing;
using System.Drawing.Text;

public abstract class CornerSquareImageGenerator : SquareImageGeneratorBase
{

    public CornerSquareImageGenerator(float squareHeight):base(squareHeight, squareHeight)
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
            gfx.DrawRectangle(blackBorderPen, -borderThickness/4, -borderThickness/4, width, height);
        }
        return bitmap;
    }
}
