using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using UnityEngine;

namespace Helpers
{
    public static class TextureHelper
    {
        public static Texture2D TextureFromBitmap(Image bmp)
        {
            var units = GraphicsUnit.Point;
            var bounds = bmp.GetBounds(ref units);
            var texture = new Texture2D((int)bounds.X, (int)bounds.Y);

            using (var ms = new MemoryStream())
            {
                bmp.Save(ms,ImageFormat.Png);

                ms.Position = 0;
                var buffer = new byte[ms.Length];
                // ReSharper disable once MustUseReturnValue
                ms.Read(buffer,0,buffer.Length);
        
                texture.LoadImage(buffer);
            }
            return texture;
        }
    }
}