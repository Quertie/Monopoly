using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using Boards.Classic.Style;
using Squares;
using UnityEngine;

namespace Boards.Classic.GameBoardCreation.ImageGenerators
{
    public class JailImageGenerator : CornerSquareImageGenerator
    {
        public JailImageGenerator(float squareHeight):base(squareHeight)
        {
        }

        public override Bitmap GetImage(Square square)
        {
            var (height, width) = GetImageSize();
            var bitmap = InitImageWithBackground();

            using (var gfx = System.Drawing.Graphics.FromImage(bitmap))
            using (var blackBorderPen = new Pen(MonopolyClassicTheme.Black, BorderThickness))
            {
                var jailImage = new Bitmap(Path.Combine(Application.streamingAssetsPath, "Classic", "Images", "jail.png"));
                var imageWidth = width *3/4;
                var imageHeight = (int)((float)jailImage.Height / jailImage.Width * imageWidth);
                gfx.DrawImage(jailImage, 0, 0, imageWidth, imageHeight);
                gfx.DrawRectangle(blackBorderPen, 0, 0, imageWidth, imageHeight);
            }

            DrawInJailTextToImage(square.Name, bitmap);
            DrawJustVisitingTextToImage(bitmap, height);

            return bitmap;
        }

        private void DrawInJailTextToImage(string text, Bitmap bitmap)
        {
            var splitText = text.ToUpper().Split(' ');
            var topText = string.Join(" ", splitText.Take(1));
            var bottomText = string.Join(" ", splitText.Skip(1));
            DrawDiagonalTextToImage(bitmap, topText, .12f, MonopolyClassicTheme.PropertyNameFontSize);
            DrawDiagonalTextToImage(bitmap, bottomText, .58f, MonopolyClassicTheme.PropertyNameFontSize);
        }

        private static void DrawJustVisitingTextToImage(Image image, float height)
        {
            DrawTextToSideOfImage(image, "SIMPLE", height*3f/4/2, height*.8f, 0);
            DrawTextToSideOfImage(image, "VISITE", .8f*height, height*3f/4/2, -90);
        }

        private static void DrawTextToSideOfImage(Image image, string text, float xPosition, float yPosition, float angle)
        {
            using (var gfx = System.Drawing.Graphics.FromImage(image))
            using (var blackBrush = new SolidBrush(MonopolyClassicTheme.Black))
            {
                var stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                gfx.TranslateTransform(xPosition, yPosition);
                gfx.RotateTransform(angle);
                gfx.DrawString(text, new System.Drawing.Font(MonopolyClassicTheme.DeedNameFontFamily, MonopolyClassicTheme.CornerNameFontSize), blackBrush, new PointF(0f, 0f), stringFormat);
            }
        }
    }
}