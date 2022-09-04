using System.IO;
using System.Drawing;
using System.Drawing.Text;

public class GoImageGenerator : CornerSquareImageGenerator
{
    public GoImageGenerator(float squareHeight):base(squareHeight)
    {
    }

    public override Bitmap GetImage(Square square)
    {
        var bitmap = InitImageWithBackground();

        var imageMarginPct = .10f;
        DrawImageToImage(bitmap, Path.Combine("Classic", "Images", "go.png"), imageMarginPct, .75f);

        DrawDiagonalTextToImage(bitmap, "Recevez\nM200 chaque fois\nque vous passez ici".ToUpper(), .18f, MonopolyClassicTheme.PropertyNameFontSize);
        DrawBoldDiagonalTextToImage(bitmap, square.Name.ToUpper(), .38f);
        
        return bitmap;
    }

    public void DrawBoldDiagonalTextToImage(Bitmap bitmap, string text, float distancePctFromTop)
    {
        var (height, width) = GetImageSize();
        
        using (var gfx = System.Drawing.Graphics.FromImage(bitmap))
        using (var blackBrush = new SolidBrush(MonopolyClassicTheme.Black))
        {
            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;


            gfx.TranslateTransform(distancePctFromTop*height, distancePctFromTop*height);
            gfx.RotateTransform(-45);
            gfx.DrawString(text, new System.Drawing.Font(MonopolyClassicTheme.DeedNameBoldFontFamily, MonopolyClassicTheme.GoFontSize), blackBrush, new PointF(0f, 0f), stringFormat);
        }
    }
}