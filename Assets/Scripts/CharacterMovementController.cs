using System.Linq;
using System.Threading.Tasks;
using Squares;
using UnityEngine;

internal class CharacterMovementController : ICharacterMovementController
{
    private readonly IGameBoard _gameBoard;

    private static readonly TaskCompletionSource<bool> TaskCompletionSource = new TaskCompletionSource<bool>();
    
    public CharacterMovementController(IGameBoard gameBoard)
    {
        _gameBoard = gameBoard;
    }
    
    public Task MoveToSquare(Square square)
    {
        Debug.Log("Move to square " + square.Name);
        var squareIndex = _gameBoard.GetSquareIndex(square);
        var squareGameObjectName = string.Format(Constants.GameObjectNames.Square, squareIndex);
        var tokenPositionMarkerGameObjectName = Constants.GameObjectNames.TokenPosition10;
        void MoveAction()
        {
            var destinationSquareTransform = GetGameBoardGameObject().GetComponentsInChildren<Transform>().Single(c => c.gameObject.name == squareGameObjectName);
            var destinationPosition = destinationSquareTransform
                .GetComponentsInChildren<Transform>().Single(c => c.gameObject.name == tokenPositionMarkerGameObjectName).position;
            GetPlayerTokenGameObject().transform.position = destinationPosition;
        }
        UnityMainThreadDispatcher.Instance().Enqueue(MoveAction);
        _gameBoard.CurrentSquare = square;
        
        return Task.CompletedTask;
    }

    private GameObject GetPlayerTokenGameObject()
    {
        return GameObject.Find("Player");
    }
    
    private GameObject GetGameBoardGameObject()
    {
        return GameObject.Find(Constants.GameObjectNames.Board);
    }
}