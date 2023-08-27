using System.Collections.Generic;
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
            
            var squareGameObjectName = string.Format(Constants.GameObjectNames.Square, _gameBoard.GetSquareIndex(square));
            var gameObject = new GameObject(squareGameObjectName, typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider));
            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            
            PlaceTokenPositionMarkers(gameObject);

            return gameObject;
        }

        private void PlaceTokenPositionMarkers(GameObject squareGameObject)
        {
            for (var numberOfPlayersOnSquare = 1; numberOfPlayersOnSquare <= 4; numberOfPlayersOnSquare++)
            {
                var tokenPositionMarkers = _geometryGenerator.GetTokenPositionMarkers(numberOfPlayersOnSquare);
                for (var positionIndex = 0; positionIndex < tokenPositionMarkers.Count; positionIndex ++)
                {
                    PlaceTokenPositionMarker(squareGameObject, numberOfPlayersOnSquare, positionIndex, tokenPositionMarkers);
                }
            }
        }

        private static void PlaceTokenPositionMarker(GameObject squareGameObject,
                                                     int numberOfPlayersOnSquare,
                                                     int positionIndex,
                                                     List<Vector3> tokenPositionMarkers)
        {
            var tokenPositionMarkerName = string.Format(Constants.GameObjectNames.TokenPosition, numberOfPlayersOnSquare, positionIndex);
            var tokenPositionMarkerGameObject = new GameObject(tokenPositionMarkerName);
            tokenPositionMarkerGameObject.transform.position = tokenPositionMarkers[positionIndex];
            tokenPositionMarkerGameObject.transform.parent = squareGameObject.transform;
        }
    }
}
