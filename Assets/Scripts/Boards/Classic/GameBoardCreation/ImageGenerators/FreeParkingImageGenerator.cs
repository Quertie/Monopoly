using System;
using System.Drawing;
using System.Drawing.Text;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;

public class FreeParkingImageGenerator : CornerSquareImageGenerator
{
    const int cornerNameFontSize = 40;

    public FreeParkingImageGenerator(float squareHeight):base(squareHeight)
    {
    }

    private void DrawDiagonalTextToImage(Bitmap bitmap, string text, float distancePctFromTop)
    {
        var (height, width) = GetImageSize();
        
        using (var gfx = System.Drawing.Graphics.FromImage(bitmap))
        using (var blackBrush = new SolidBrush(MonopolyClassicTheme.Black))
        {
            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;


            gfx.TranslateTransform(distancePctFromTop*height, distancePctFromTop*height);
            gfx.RotateTransform(-45);
            gfx.DrawString(text, new System.Drawing.Font(MonopolyClassicTheme.DeedNameFontFamily, cornerNameFontSize), blackBrush, new PointF(0f, 0f), stringFormat);
        }
    }

    public override Bitmap GetImage(Square square)
    {
        var bitmap = InitImageWithBackground();

        var imageMarginPct = .25f;
        DrawImageToImage(bitmap, Path.Combine("Classic", "Images", "free-parking.png"), imageMarginPct, imageMarginPct);

        var topText = square.Name.ToUpper().Split(' ')[0];
        var bottomText = String.Join(" ", square.Name.ToUpper().Split(' ').Skip(1));

        DrawDiagonalTextToImage(bitmap, topText, .2f);
        DrawDiagonalTextToImage(bitmap, bottomText, .65f);
        
        return bitmap;
    }
}