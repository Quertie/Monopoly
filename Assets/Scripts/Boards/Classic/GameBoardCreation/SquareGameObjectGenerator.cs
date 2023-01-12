using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Boards.Classic.GameBoardCreation.ImageGenerators;
using Boards.Classic.GameBoardCreation.MeshGenerators;
using Squares;
using UnityEngine;

namespace Boards.Classic.GameBoardCreation
{
    public class SquareGameObjectGenerator
    {
        private readonly ISquareImageGenerator _imageGenerator;
        private readonly ISquareMeshGenerator _meshGenerator;


        public SquareGameObjectGenerator(ISquareImageGenerator imageGenerator, ISquareMeshGenerator meshGenerator)
        {
            _imageGenerator = imageGenerator;
            _meshGenerator = meshGenerator;
        }


        public GameObject CreateGameObject(Square square)
        {
            var squareBitmap = _imageGenerator.GetImage(square);
            var texture = TextureFromBitmap(squareBitmap);

            var squareObject = CreateSquareGameObject();
            squareObject.GetComponent<Renderer>().material.mainTexture = texture;
            return squareObject;
        }

        private static Texture2D TextureFromBitmap(Image bmp)
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

        private GameObject CreateSquareGameObject()
        {
            var mesh = _meshGenerator.GetMesh();
            var gameObject = new GameObject("Square", typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider));
            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            return gameObject;
        }

    }
}
