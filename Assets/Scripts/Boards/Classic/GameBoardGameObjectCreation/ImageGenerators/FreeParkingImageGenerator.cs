using System.Drawing;
using System.IO;
using System.Linq;
using Squares;

namespace Boards.Classic.GameBoardGameObjectCreation.ImageGenerators
{
    public class FreeParkingImageGenerator : CornerSquareImageGenerator
    {

        public FreeParkingImageGenerator(float squareHeight):base(squareHeight)
        {
        }

        public override Bitmap GetImage(Square square)
        {
            var bitmap = InitImageWithBackground();

            const float imageMarginPct = .25f;
            DrawImageToImage(bitmap, Path.Combine("Classic", "Images", "free-parking.png"), imageMarginPct, imageMarginPct);

            var topText = square.Name.ToUpper().Split(' ')[0];
            var bottomText = string.Join(" ", square.Name.ToUpper().Split(' ').Skip(1));

            DrawDiagonalTextToImage(bitmap, topText, .2f);
            DrawDiagonalTextToImage(bitmap, bottomText, .65f);
        
            return bitmap;
        }
    }
}