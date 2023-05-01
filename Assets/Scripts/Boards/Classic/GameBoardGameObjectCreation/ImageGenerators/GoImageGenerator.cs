using System.Drawing;
using System.Drawing.Text;
using System.IO;
using Boards.Classic.Style;
using Squares;

namespace Boards.Classic.GameBoardGameObjectCreation.ImageGenerators
{
    public class GoImageGenerator : CornerSquareImageGenerator
    {
        public GoImageGenerator(float squareHeight):base(squareHeight)
        {
        }

        public override Bitmap GetImage(Square square)
        {
            var bitmap = InitImageWithBackground();

            const float imageMarginPct = .10f;
            DrawImageToImage(bitmap, Path.Combine("Classic", "Images", "go.png"), imageMarginPct, .75f);

            DrawDiagonalTextToImage(bitmap, "Recevez\nM200 chaque fois\nque vous passez ici".ToUpper(), .18f, MonopolyClassicTheme.PropertyNameFontSize);
            DrawBoldDiagonalTextToImage(bitmap, square.Name.ToUpper(), .38f);
        
            return bitmap;
        }

        private void DrawBoldDiagonalTextToImage(Bitmap bitmap, string text, float distancePctFromTop)
        {
            // ReSharper disable once UnusedVariable
            var (height, width) = GetImageSize();
        
            using (var gfx = Graphics.FromImage(bitmap))
            using (var blackBrush = new SolidBrush(MonopolyClassicTheme.Black))
            {
                var stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;


                gfx.TranslateTransform(distancePctFromTop*height, distancePctFromTop*height);
                gfx.RotateTransform(-45);
                gfx.DrawString(text, new Font(MonopolyClassicTheme.DeedNameBoldFontFamily, MonopolyClassicTheme.GoFontSize), blackBrush, new PointF(0f, 0f), stringFormat);
            }
        }
    }
}