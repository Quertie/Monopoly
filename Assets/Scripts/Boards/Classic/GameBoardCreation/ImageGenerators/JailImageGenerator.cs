using UnityEngine;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;

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
        using (var blackBrush = new SolidBrush(MonopolyClassicTheme.Black))
        {
            var jailImage = new Bitmap(Path.Combine(Application.streamingAssetsPath, "Classic", "Images", "jail.png").Replace("/", "\\"));
            var imageMargin = (int)(width * 1/4);
            var imageWidth = (int)(width *3/4);
            var imageHeight = (int)((float)jailImage.Height / jailImage.Width * imageWidth);
            gfx.DrawImage(jailImage, 0, 0, imageWidth, imageHeight);

            Pen blackBorderPen = new Pen(MonopolyClassicTheme.Black, borderThickness);
            gfx.DrawRectangle(blackBorderPen, 0, 0, imageWidth, imageHeight);
        }

        var topText = String.Join(" ", square.Name.ToUpper().Split(' ').Take(1));
        var bottomText = String.Join(" ", square.Name.ToUpper().Split(' ').Skip(1));
        DrawDiagonalTextToImage(bitmap, topText, .12f, MonopolyClassicTheme.PropertyNameFontSize);
        DrawDiagonalTextToImage(bitmap, bottomText, .58f, MonopolyClassicTheme.PropertyNameFontSize);

        // TODO REFACTOR

        using (var gfx = System.Drawing.Graphics.FromImage(bitmap))
        using (var blackBrush = new SolidBrush(MonopolyClassicTheme.Black))
        {
            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            gfx.TranslateTransform(height*3/4/2, height*.8f);
            gfx.DrawString("SIMPLE", new System.Drawing.Font(MonopolyClassicTheme.DeedNameFontFamily, MonopolyClassicTheme.CornerNameFontSize), blackBrush, new PointF(0f, 0f), stringFormat);
        }

        using (var gfx = System.Drawing.Graphics.FromImage(bitmap))
        using (var blackBrush = new SolidBrush(MonopolyClassicTheme.Black))
        {
            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            gfx.TranslateTransform(.8f*height, height*3/4/2);
            gfx.RotateTransform(-90);
            gfx.DrawString("VISITE", new System.Drawing.Font(MonopolyClassicTheme.DeedNameFontFamily, MonopolyClassicTheme.CornerNameFontSize), blackBrush, new PointF(0f, 0f), stringFormat);
        }

        return bitmap;
    }
}