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
        private readonly IGeometryGenerator _geometryGenerator;

        public SquareGameObjectGenerator(ISquareImageGenerator imageGenerator, IGeometryGenerator geometryGenerator)
        {
            _imageGenerator = imageGenerator;
            _geometryGenerator = geometryGenerator;
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
            var mesh = _geometryGenerator.GetMesh();
            var gameObject = new GameObject(Constants.GameObjectNames.Square, typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider));
            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            
            var tokenPositionMarker = _geometryGenerator.GetTokenPositionMarker();
            var tokenPositionMarkerGameObject = new GameObject(Constants.GameObjectNames.TokenPosition10);
            tokenPositionMarkerGameObject.transform.parent = gameObject.transform;
            
            return gameObject;
        }

    }
}
