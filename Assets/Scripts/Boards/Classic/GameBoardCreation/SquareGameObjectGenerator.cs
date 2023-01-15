using Boards.Classic.GameBoardCreation.ImageGenerators;
using Boards.Classic.GameBoardCreation.MeshGenerators;
using Helpers;
using Squares;
using UnityEngine;

namespace Boards.Classic.GameBoardCreation
{
    public class SquareGameObjectGenerator
    {
        private readonly ISquareImageGenerator _imageGenerator;
        private readonly IMeshGenerator _meshGenerator;

        public SquareGameObjectGenerator(ISquareImageGenerator imageGenerator, IMeshGenerator meshGenerator)
        {
            _imageGenerator = imageGenerator;
            _meshGenerator = meshGenerator;
        }


        public GameObject CreateGameObject(Square square)
        {
            var squareBitmap = _imageGenerator.GetImage(square);
            var texture = TextureHelper.TextureFromBitmap(squareBitmap);

            var squareObject = CreateSquareGameObject();
            squareObject.GetComponent<Renderer>().material.mainTexture = texture;
            return squareObject;
        }

        

        private GameObject CreateSquareGameObject()
        {
            var mesh = _meshGenerator.GetMesh();
            var gameObject = new GameObject(Constants.GameObjectNames.Square, typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider));
            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            return gameObject;
        }

    }
}
