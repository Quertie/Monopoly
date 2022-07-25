using System.Drawing;
using System.Drawing.Text;
using System.IO;
using UnityEngine;
public class BorderTrainStationImageGenerator : BorderDeedSquareImageGenerator
{

    private const int TrainStationNameFontSize = 40;

    public BorderTrainStationImageGenerator(float squareHeight, float squareWidth):base(squareHeight, squareWidth)
    {
    }

    public override Bitmap GetImage(Square square)
    {
        var trainStation = (TrainStation)square;
        var (height, width) = GetImageSize();

        var bitmap = InitImageWithBackground();
        DrawTextToImage(bitmap, trainStation.Name.ToUpper(), .1f, TrainStationNameFontSize);
        DrawTrainToImage(bitmap);
        DrawPriceToImage(bitmap, trainStation);
        return bitmap;
    }

    private void DrawTrainToImage(Bitmap bitmap)
    {
        var (height, width) = GetImageSize();

        using (var gfx = System.Drawing.Graphics.FromImage(bitmap))
        using (var blackBrush = new SolidBrush(MonopolyClassicTheme.Black))
        {
            var trainImage = new Bitmap(Path.Combine(Application.streamingAssetsPath, "train.png").Replace("/", "\\"));
            var imageMargin = (int)(width * 0.1);
            var imageWidth = (int)(width - imageMargin * 2);
            var imageHeight = (int)((float)trainImage.Height / trainImage.Width * imageWidth);
            gfx.DrawImage(trainImage, imageMargin, height * 0.37f, imageWidth, imageHeight);
        }
    }
}