using System.Drawing;
using System.Drawing.Text;
using System.IO;
using UnityEngine;

public static class MonopolyClassicTheme
{
    public static System.Drawing.Color LightGreen => System.Drawing.Color.FromArgb(191, 219, 174);
    public static System.Drawing.Color Black => System.Drawing.Color.Black;
    public static System.Drawing.Color BrownGroupColor => System.Drawing.Color.FromArgb(132, 77, 36);
    public static System.Drawing.Color LightBlueGroupColor => System.Drawing.Color.FromArgb(154, 207, 239);
    public static System.Drawing.Color VioletGroupColor => System.Drawing.Color.FromArgb(206, 37, 128);
    public static System.Drawing.Color OrangeGroupColor => System.Drawing.Color.FromArgb(236, 154, 19);
    public static System.Drawing.Color RedGroupColor => System.Drawing.Color.FromArgb(218, 10, 44);
    public static System.Drawing.Color YellowGroupColor => System.Drawing.Color.FromArgb(230, 226, 65);
    public static System.Drawing.Color GreenGroupColor => System.Drawing.Color.FromArgb(41, 170, 90);
    public static System.Drawing.Color DarkBlueGroupColor => System.Drawing.Color.FromArgb(58, 90, 151);

    public static FontFamily DeedNameFontFamily{
        get
        {
            var fontPath = Path.Combine(Application.streamingAssetsPath, "Fonts", "futura medium bt.ttf").Replace('/', '\\');
            var collection = new PrivateFontCollection();
            collection.AddFontFile(fontPath);
            return new FontFamily(collection.Families[0].Name, collection);
        }
    }
}