using System.Drawing;
using System.Drawing.Text;
using System.IO;
using Boards.Classic.Style;
using Squares;
using UnityEngine;

namespace Boards.Classic.GameBoardGameObjectCreation.ImageGenerators
{
    public abstract class SquareImageGeneratorBase:ISquareImageGenerator
    {
        protected const int BorderThickness = 8;
        private const int ScaleFactor = 77;

        private readonly float _squareHeight;
        private readonly float _squareWidth;

        protected SquareImageGeneratorBase(float squareHeight, float squareWidth)
        {
            _squareHeight = squareHeight;
            _squareWidth = squareWidth;
        }

        public abstract Bitmap GetImage(Square square);

        protected (int height, int width) GetImageSize()
        {
            return ((int)_squareHeight * ScaleFactor, (int)_squareWidth * ScaleFactor);
        }

        protected void DrawTextToImage(Bitmap bitmap, string text, float heightPercent, int fontSize)
        {
            var (height, width) = GetImageSize();

            using (var gfx = System.Drawing.Graphics.FromImage(bitmap))
            using (var blackBrush = new SolidBrush(MonopolyClassicTheme.Black))
            {
                var stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                var textBoxMargin = (int)(width*0.02);
                var textBoxWidth = width-textBoxMargin*2;
                var textBox = new RectangleF(textBoxMargin, height*heightPercent, textBoxWidth, height*.3f);
                gfx.DrawString(text, new System.Drawing.Font(MonopolyClassicTheme.DeedNameFontFamily, fontSize), blackBrush, textBox, stringFormat);
            }
        }

        protected void DrawImageToImage(Bitmap bitmap, string imageAssetPath, float imageMarginPct, float imageVerticalPositionPct)
        {
            var (height, width) = GetImageSize();
        
            using (var gfx = System.Drawing.Graphics.FromImage(bitmap))
            {
                var trainImageFilename = Path.Combine(Application.streamingAssetsPath, imageAssetPath).Replace("\\", "/");
                var trainImage = new Bitmap(trainImageFilename);
                var imageMargin = (int)(width * imageMarginPct);
                var imageWidth = width - imageMargin * 2;
                var imageHeight = (int)((float)trainImage.Height / trainImage.Width * imageWidth);
                gfx.DrawImage(trainImage, imageMargin, height * imageVerticalPositionPct, imageWidth, imageHeight);
            }
        }
    }
}