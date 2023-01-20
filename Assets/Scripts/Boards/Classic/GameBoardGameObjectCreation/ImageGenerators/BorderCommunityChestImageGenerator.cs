using System.Drawing;
using System.IO;
using Boards.Classic.Style;
using Squares;

namespace Boards.Classic.GameBoardGameObjectCreation.ImageGenerators
{
    public class BorderCommunityChestImageGenerator:BorderSquareImageGeneratorBase
    {
        public BorderCommunityChestImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
        {
        }

        public override Bitmap GetImage(Square square)
        {
            var bitmap = InitImageWithBackground();

            DrawTextToImage(bitmap, square.Name.ToUpper(), .08f, MonopolyClassicTheme.PropertyNameFontSize);
            DrawImageToImage(bitmap, Path.Combine("Classic", "Images", "community-chest.png"), .1f, .45f);
            return bitmap;
        }
    }
}