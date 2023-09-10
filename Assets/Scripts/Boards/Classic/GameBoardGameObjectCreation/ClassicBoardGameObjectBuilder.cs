using System.Linq;
using Boards.Classic.GameBoardGameObjectCreation.GeometryGenerators;
using Boards.Classic.GameBoardGameObjectCreation.ImageGenerators;
using Helpers;
using UnityEngine;

namespace Boards.Classic.GameBoardGameObjectCreation
{
    public class ClassicBoardGameObjectBuilder
    {

        private readonly float _squareWidth;
        private readonly float _squareHeight;

        private readonly IGameBoard _gameBoard;
        private readonly SquareGameObjectGeneratorFactory _squareGameObjectGeneratorFactory;

        public ClassicBoardGameObjectBuilder(IGameBoard gameBoard, SquareGameObjectGeneratorFactory squareGameObjectGeneratorFactory, float squareWidth, float squareHeight)
        {
            _gameBoard = gameBoard;
            _squareGameObjectGeneratorFactory = squareGameObjectGeneratorFactory;
            _squareWidth = squareWidth;
            _squareHeight = squareHeight;
        }

        public GameObject BuildBoard()
        {
            var boardGameObject = new GameObject(Constants.GameObjectNames.Board);
            
            var totalSquares = GetTotalSquares();
            for (var i = 0; i < totalSquares; i++)
            {
                var square = _gameBoard.Squares[i];
                var gameObjectGenerator = _squareGameObjectGeneratorFactory.GetGameObjectGenerator(square);
                var squareGameObject = gameObjectGenerator.CreateGameObject(square);
                MoveSquareToPosition(squareGameObject, i);
                squareGameObject.transform.parent = boardGameObject.transform;
            }

            var centerSquareGameObject = GetCenterSquareGameObject();
            MoveCenterSquareToPosition(centerSquareGameObject);
            centerSquareGameObject.transform.parent = boardGameObject.transform;
            return boardGameObject;
        }

        private GameObject GetCenterSquareGameObject()
        {
            var centerSquareWidth = _squareWidth * (GetTotalSquares()-4)/4;
            
            var geometryGenerator = new SquareGeometryGenerator(centerSquareWidth);
            var mesh = geometryGenerator.GetMesh();
            
            var imageGenerator = new CenterSquareImageGenerator(centerSquareWidth);
            var image = imageGenerator.GetImage(null);
            var texture = TextureHelper.TextureFromBitmap(image);
            
            var gameObject = new GameObject("Center Square", typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider));
            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            gameObject.GetComponent<Renderer>().material.mainTexture = texture;
            
            return gameObject;
        }

        private int GetTotalSquares()
        {
            return _gameBoard.Squares.Count();
        }

        private void MoveSquareToPosition(GameObject squareGameObject, int i)
        {
            var totalSquares = _gameBoard.Squares.Count();
            float x,z,r;
            if (i == 0)
            {
                //For some reason, setting x and z to zero offsets the square. This works.
                return;
            }
            if (i < totalSquares/4)
            {
                x = -_squareHeight/2 - _squareWidth * (0.5f + i-1);
                z = 0f;
                r = 0f;
            }
            else if (i == totalSquares/4)
            {
                x = -_squareWidth * (totalSquares/4 - 1) - _squareHeight;
                z = 0f;
                r = 90f;
            }
            else if (i < totalSquares/2)
            {
                x = -_squareWidth * (totalSquares/4 - 1) - _squareHeight;
                z = _squareHeight/2 + _squareWidth * (0.5f + i%(totalSquares/4)-1);
                r = 90f;
            }
            else if (i == totalSquares/2)
            {
                x = -_squareWidth * (totalSquares/4 - 1) - _squareHeight;
                z = _squareWidth * (totalSquares/4 - 1) + _squareHeight;
                r = 180f;
            }
            else if (i < 3*totalSquares/4)
            {
                x = -_squareWidth * (totalSquares/4 - 1) - _squareHeight + _squareHeight/2 + _squareWidth * (0.5f + i%(totalSquares/4)-1);
                z = _squareWidth * (totalSquares/4 - 1) + _squareHeight;
                r = 180f;
            }
            else if (i == 3*totalSquares/4)
            {
                x = 0f;
                z = _squareWidth * (totalSquares/4 - 1) + _squareHeight;
                r = 270f;
            }
            else
            {
                x = 0f;
                z = _squareWidth * (totalSquares/4 - 1) + _squareHeight - _squareHeight/2 - _squareWidth * (0.5f +i%(totalSquares/4)-1);
                r = 270f;
            }
            squareGameObject.transform.Translate(x, 0f, z);
            squareGameObject.transform.Rotate(0f, r, 0f);
        }
        
        private void MoveCenterSquareToPosition(GameObject centerSquareGameObject)
        {
            var offset = _squareHeight / 2 + (float)(GetTotalSquares()-4)/4 * _squareWidth / 2;
            centerSquareGameObject.transform.Translate(-offset, 0f, offset);
        }
    }
}
