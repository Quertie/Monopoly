using System.Drawing;
using System.IO;
using System.Linq;
using Squares;

namespace Boards.Classic.GameBoardGameObjectCreation.ImageGenerators
{
    public class GoToJailImageGenerator : CornerSquareImageGenerator
    {
        public GoToJailImageGenerator(float squareHeight):base(squareHeight)
        {
        }

        public override Bitmap GetImage(Square square)
        {
            var bitmap = InitImageWithBackground();

            const float imageMarginPct = .15f;
            DrawImageToImage(bitmap, Path.Combine("Classic", "Images", "go-to-jail.png"), imageMarginPct, imageMarginPct);

            var topText = string.Join(" ", square.Name.ToUpper().Split(' ').Take(2));
            var bottomText = string.Join(" ", square.Name.ToUpper().Split(' ').Skip(2));

            DrawDiagonalTextToImage(bitmap, topText, .18f);
            DrawDiagonalTextToImage(bitmap, bottomText, .72f);
        
            return bitmap;
        }
    }
}