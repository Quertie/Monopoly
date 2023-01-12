using System.Linq;
using UnityEngine;

namespace Boards.Classic.GameBoardCreation
{
    public class BoardBuilder
    {

        private const float SquareWidth = 4f;
        private const float SquareHeight = 6.5f;

        private readonly GameBoard _gameBoard;
        private readonly SquareGameObjectGeneratorFactory _squareGameObjectGeneratorFactory;

        public BoardBuilder(GameBoard gameBoard, SquareGameObjectGeneratorFactory squareGameObjectGeneratorFactory)
        {
            _gameBoard = gameBoard;
            _squareGameObjectGeneratorFactory = squareGameObjectGeneratorFactory;
        }

        public void BuildBoard()
        {
            var totalSquares = _gameBoard.Squares.Count();
            for (int i = 0; i < totalSquares; i++)
            {
                var square = _gameBoard.Squares[i];
                var gameObjectGenerator = _squareGameObjectGeneratorFactory.GetGameObjectGenerator(square);
                var squareGameObject = gameObjectGenerator.CreateGameObject(square);
                MoveSquareToPosition(squareGameObject, i);
            }
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
                x = -SquareHeight/2 - SquareWidth * (0.5f + i-1);
                z = 0f;
                r = 0f;
            }
            else if (i == totalSquares/4)
            {
                x = -SquareWidth * (totalSquares/4 - 1) - SquareHeight;
                z = 0f;
                r = 90f;
            }
            else if (i < totalSquares/2)
            {
                x = -SquareWidth * (totalSquares/4 - 1) - SquareHeight;
                z = SquareHeight/2 + SquareWidth * (0.5f + i%(totalSquares/4)-1);
                r = 90f;
            }
            else if (i == totalSquares/2)
            {
                x = -SquareWidth * (totalSquares/4 - 1) - SquareHeight;
                z = SquareWidth * (totalSquares/4 - 1) + SquareHeight;
                r = 180f;
            }
            else if (i < 3*totalSquares/4)
            {
                x = -SquareWidth * (totalSquares/4 - 1) - SquareHeight + SquareHeight/2 + SquareWidth * (0.5f + i%(totalSquares/4)-1);
                z = SquareWidth * (totalSquares/4 - 1) + SquareHeight;
                r = 180f;
            }
            else if (i == 3*totalSquares/4)
            {
                x = 0f;
                z = SquareWidth * (totalSquares/4 - 1) + SquareHeight;
                r = 270f;
            }
            else
            {
                x = 0f;
                z = SquareWidth * (totalSquares/4 - 1) + SquareHeight - SquareHeight/2 - SquareWidth * (0.5f +i%(totalSquares/4)-1);
                r = 270f;
            }
            squareGameObject.transform.Translate(x, 0f, z);
            squareGameObject.transform.Rotate(0f, r, 0f);
        }
    }
}
