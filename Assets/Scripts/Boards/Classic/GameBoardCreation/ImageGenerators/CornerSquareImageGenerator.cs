using System.Drawing;
using System.Drawing.Text;
using Boards.Classic.Style;

namespace Boards.Classic.GameBoardCreation.ImageGenerators
{
    public abstract class CornerSquareImageGenerator : SquareImageGeneratorBase
    {
        protected CornerSquareImageGenerator(float squareHeight):base(squareHeight, squareHeight)
        {
        }

        protected Bitmap InitImageWithBackground()
        {

            var (height, width) = GetImageSize();
            var bitmap = new Bitmap(width, height);
        
            using (var gfx = Graphics.FromImage(bitmap))
            using (var greenBrush = new SolidBrush(MonopolyClassicTheme.LightGreen))
            {
                gfx.FillRectangle(greenBrush, 0, 0, width, height);
                var blackBorderPen = new Pen(MonopolyClassicTheme.Black, BorderThickness);
                //Offset is necessary otherwise to correct some border problems
                gfx.DrawRectangle(blackBorderPen, -BorderThickness/4, -BorderThickness/4, width, height);
            }
            return bitmap;
        }

        protected void DrawDiagonalTextToImage(Bitmap bitmap, string text, float distancePctFromTop)
        {
            DrawDiagonalTextToImage(bitmap, text, distancePctFromTop, MonopolyClassicTheme.CornerNameFontSize);
        }

        protected void DrawDiagonalTextToImage(Bitmap bitmap, string text, float distancePctFromTop, float fontSize)
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
                gfx.DrawString(text, new Font(MonopolyClassicTheme.DeedNameFontFamily, fontSize), blackBrush, new PointF(0f, 0f), stringFormat);
            }
        }

    }
}
