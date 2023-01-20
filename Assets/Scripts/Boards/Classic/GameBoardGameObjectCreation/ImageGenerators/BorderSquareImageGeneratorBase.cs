using System.Drawing;
using Boards.Classic.Style;

namespace Boards.Classic.GameBoardGameObjectCreation.ImageGenerators
{
    public abstract class BorderSquareImageGeneratorBase : SquareImageGeneratorBase
    {
        protected BorderSquareImageGeneratorBase(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
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
                gfx.DrawRectangle(blackBorderPen, 0, 0, width, height-BorderThickness/2);
            }
            return bitmap;
        }

        protected void DrawTextToBottomOfImage(Bitmap bitmap, string text)
        {
            DrawTextToImage(bitmap, text, .85f, MonopolyClassicTheme.SmallFontSize);
        }
    }
}
