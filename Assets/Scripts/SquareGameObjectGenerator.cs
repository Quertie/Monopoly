using UnityEngine;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

public class SquareGameObjectGenerator
{
    private ISquareImageGenerator _imageGenerator;
    private ISquareMeshGenerator _meshGenerator;


    public SquareGameObjectGenerator(ISquareImageGenerator imageGenerator, ISquareMeshGenerator meshGenerator)
    {
        _imageGenerator = imageGenerator;
        _meshGenerator = meshGenerator;
    }


    public GameObject CreateGameObject(Square square)
    {
        var squareBitmap = _imageGenerator.GetImage(square);
        var texture = TextureFromBitmap(squareBitmap);

        var squareObject = CreateSquareGameObject(square);
        squareObject.GetComponent<Renderer>().material.mainTexture = texture;
        return squareObject;
    }

    private Texture2D TextureFromBitmap(Bitmap bmp)
    {
        var units = GraphicsUnit.Point;
        var bounds = bmp.GetBounds(ref units);
        var texture = new Texture2D((int)bounds.X, (int)bounds.Y);

        using (MemoryStream ms = new MemoryStream())
        {
            bmp.Save(ms,ImageFormat.Png);

            ms.Position = 0;
            var buffer = new byte[ms.Length];
            ms.Read(buffer,0,buffer.Length);
        
            texture.LoadImage(buffer);
        }
        return texture;
    }

    private GameObject CreateSquareGameObject(Square square)
    {
        var mesh = _meshGenerator.GetMesh();
        var gameObject = new GameObject("Square", typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider));
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        return gameObject;
    }

}
