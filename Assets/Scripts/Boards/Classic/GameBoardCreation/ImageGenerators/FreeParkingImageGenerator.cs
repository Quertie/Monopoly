using System;
using System.Drawing;
using System.Drawing.Text;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;

public class FreeParkingImageGenerator : CornerSquareImageGenerator
{

    public FreeParkingImageGenerator(float squareHeight):base(squareHeight)
    {
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