using System.Drawing;
using System.IO;
using Boards.Classic.Style;
using Squares;

namespace Boards.Classic.GameBoardCreation.ImageGenerators
{
    public class BorderTrainStationImageGenerator : BorderDeedSquareImageGenerator
    {
        public BorderTrainStationImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
        {
        }

        public override Bitmap GetImage(Square square)
        {
            var trainStation = (TrainStation)square;

            var bitmap = InitImageWithBackground();
            DrawTextToImage(bitmap, trainStation.Name.ToUpper(), .08f, MonopolyClassicTheme.PropertyNameFontSize);
            DrawImageToImage(bitmap, Path.Combine("Classic", "Images", "train.png"), .1f, .37f);
            DrawPriceToImage(bitmap, trainStation);
            return bitmap;
        }
    }
}
