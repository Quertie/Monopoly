using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Boards.Classic.Style;
using Squares;

namespace Boards.Classic.GameBoardCreation.ImageGenerators
{
    public class BorderChanceImageGenerator:BorderSquareImageGeneratorBase
    {
        private int _chanceSquareOccurence = 0;

        private readonly Dictionary<int, string> _occurenceToFileName = new Dictionary<int, string> ()
        {
            {0, "chance-violet.png"},
            {1, "chance-blue.png"},
            {2, "chance-orange.png"}
        };
    
        public BorderChanceImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
        {
        }

        public override Bitmap GetImage(Square square)
        {
            var bitmap = InitImageWithBackground();

            DrawTextToImage(bitmap, square.Name.ToUpper(), .08f, MonopolyClassicTheme.PropertyNameFontSize);
            DrawImageToImage(bitmap, Path.Combine("Classic", "Images", _occurenceToFileName[_chanceSquareOccurence++%3]), .22f, .2f);
            return bitmap;
        }
    }
}