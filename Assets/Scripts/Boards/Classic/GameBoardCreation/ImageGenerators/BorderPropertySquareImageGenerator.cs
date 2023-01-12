using System.Drawing;
using Boards.Classic.Style;
using Squares;

namespace Boards.Classic.GameBoardCreation.ImageGenerators
{
    public class BorderPropertySquareImageGenerator : BorderDeedSquareImageGenerator
    {
        public BorderPropertySquareImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
        {
        }

        public override Bitmap GetImage(Square square)
        {
            var property = (Property)square;
            var bitmap = InitImageWithBackground();

            DrawTextToImage(bitmap, property.Name.ToUpper(), .3f, MonopolyClassicTheme.PropertyNameFontSize);
            DrawColorGroupToImage(bitmap, property);
            DrawPriceToImage(bitmap, property);
            return bitmap;
        }

        private void DrawColorGroupToImage(Bitmap bitmap, Property property)
        {
            var (height, width) = GetImageSize();

            using (var gfx = Graphics.FromImage(bitmap))
            using (var propertyColorBrush = new SolidBrush(property.ColorGroup.Color))
            {
                var blackBorderPen = new Pen(MonopolyClassicTheme.Black, BorderThickness);

                gfx.FillRectangle(propertyColorBrush, 0, 0, width, height / 5);
                gfx.DrawRectangle(blackBorderPen, 0, 0, width, height / 5);
            }
        }
    }
}
