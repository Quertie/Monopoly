using Boards.Classic.GameBoardGameObjectCreation.ImageGenerators;
using Boards.Classic.GameBoardGameObjectCreation.MeshGenerators;
using Helpers;
using Squares;
using UnityEngine;

namespace Boards.Classic.GameBoardGameObjectCreation
{
    public class SquareGameObjectGenerator
    {
        private readonly ISquareImageGenerator _imageGenerator;
        private readonly IGeometryGenerator _geometryGenerator;
        private readonly IGameBoard _gameBoard;

        public SquareGameObjectGenerator(ISquareImageGenerator imageGenerator, IGeometryGenerator geometryGenerator, IGameBoard gameBoard)
        {
            _imageGenerator = imageGenerator;
            _geometryGenerator = geometryGenerator;
            _gameBoard = gameBoard;
        }
        
        public GameObject CreateGameObject(Square square)
        {
            var squareBitmap = _imageGenerator.GetImage(square);
            var texture = TextureHelper.TextureFromBitmap(squareBitmap);

            var squareObject = CreateSquareGameObject(square);
            squareObject.GetComponent<Renderer>().material.mainTexture = texture;
            return squareObject;
        }
        
        private GameObject CreateSquareGameObject(Square square)
        {
            var mesh = _geometryGenerator.GetMesh();
            var gameObject = new GameObject(string.Format(Constants.GameObjectNames.Square, _gameBoard.GetSquareIndex(square)), typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider));
            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            
            var tokenPositionMarker = _geometryGenerator.GetTokenPositionMarker();
            var tokenPositionMarkerGameObject = new GameObject(Constants.GameObjectNames.TokenPosition10);
            tokenPositionMarkerGameObject.transform.position = tokenPositionMarker;
            tokenPositionMarkerGameObject.transform.parent = gameObject.transform;
            
            return gameObject;
        }

    }
}
